using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MSHB.Reservation.Shared.Common.Captcha.Contracts;
using MSHB.Reservation.Shared.Common.Captcha.Providers;

namespace MSHB.Reservation.Shared.Common.Captcha
{
    
    [HtmlTargetElement("ze-captcha")]
    public class CaptchaTagHelper : CaptchaTagHelperHtmlAttributes, ITagHelper
    {
        /// <summary>
        /// The default hidden input name of the captcha.
        /// </summary>
        public static readonly string CaptchaHiddenInputName = "CaptchaText";

        /// <summary>
        /// The default hidden input name of the captcha's cookie token.
        /// </summary>
        public static readonly string CaptchaHiddenTokenName = "CaptchaToken";

        /// <summary>
        /// The default input name of the captcha.
        /// </summary>
        public static readonly string CaptchaInputName = "CaptchaInputText";

        private readonly ICaptchaProtectionProvider _captchaProtectionProvider;
        private readonly ICaptchaStorageProvider _captchaStorageProvider;
        private readonly IHumanReadableIntegerProvider _humanReadableIntegerProvider;
        private readonly IRandomNumberProvider _randomNumberProvider;

        public CaptchaTagHelper(
            ICaptchaProtectionProvider captchaProtectionProvider,
            IRandomNumberProvider randomNumberProvider,
            IHumanReadableIntegerProvider humanReadableIntegerProvider,
            ICaptchaStorageProvider captchaStorageProvider)
        {
            captchaProtectionProvider.CheckArgumentNull(nameof(captchaProtectionProvider));
            randomNumberProvider.CheckArgumentNull(nameof(randomNumberProvider));
            humanReadableIntegerProvider.CheckArgumentNull(nameof(humanReadableIntegerProvider));
            captchaStorageProvider.CheckArgumentNull(nameof(captchaStorageProvider));

            _captchaProtectionProvider = captchaProtectionProvider;
            _randomNumberProvider = randomNumberProvider;
            _humanReadableIntegerProvider = humanReadableIntegerProvider;
            _captchaStorageProvider = captchaStorageProvider;
        }

        /// <summary>
        /// Default order is <c>0</c>.
        /// </summary>
        public int Order { get; } = 0;

        /// <summary>
        /// The current ViewContext.
        /// </summary>
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        /// <inheritdoc />
        public void Init(TagHelperContext context)
        {
        }

        /// <summary>
        /// Process the taghelper and generate the output.
        /// </summary>
        public void Process(TagHelperContext context, TagHelperOutput output)
        {
            context.CheckArgumentNull(nameof(context));
            output.CheckArgumentNull(nameof(output));

            output.TagName = "div";
            output.Attributes.Add("class", "captcha");
            var captchaDivId = $"captcha{context.UniqueId}{_randomNumberProvider.Next(Min, Max)}";
            output.Attributes.Add("id", captchaDivId);
            output.TagMode = TagMode.StartTagAndEndTag;

            var number = _randomNumberProvider.Next(Min, Max);
            var randomText = "";
            randomText = OutputType == OutputType.InDigits ? DNTPersianUtils.Core.PersianNumbersUtils.ToPersianNumbers(number) : _humanReadableIntegerProvider.NumberToText(number, Language);
            
            var encryptedText = _captchaProtectionProvider.Encrypt(randomText);

            var captchaImage = GetCaptchaImageTagBuilder(encryptedText);
            output.Content.AppendHtml(captchaImage);

            var cookieToken = $".{captchaDivId}";
            var refreshButton = GetRefreshButtonTagBuilder(captchaDivId, cookieToken);
            output.Content.AppendHtml(refreshButton);

            var hiddenInput = GetHiddenInputTagBuilder(encryptedText);
            output.Content.AppendHtml(hiddenInput);
            if (!ImageOnly)
            {
                var textInput = GetTextInputTagBuilder();
                output.Content.AppendHtml($"{string.Format(TextBoxTemplate, textInput.GetString())}");
            }

            var validationMessage = GetValidationMessageTagBuilder();
            output.Content.AppendHtml(validationMessage);

            var hiddenInputToken = GetHiddenInputTokenTagBuilder(_captchaProtectionProvider.Encrypt(cookieToken));
            output.Content.AppendHtml(hiddenInputToken);

            _captchaStorageProvider.Add(ViewContext.HttpContext, cookieToken, randomText);
        }

        /// <summary>
        /// Asynchronously executes the <see cref="TagHelper"/> with the given <paramref name="context"/> and
        /// <paramref name="output"/>.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>A <see cref="Task"/> that on completion updates the <paramref name="output"/>.</returns>
        /// <remarks>By default this calls into <see cref="Process"/>.</remarks>.
        public Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            return Task.CompletedTask;
        }

        private static TagBuilder GetHiddenInputTagBuilder(string encryptedText)
        {
            var hiddenInput = new TagBuilder("input");
            hiddenInput.Attributes.Add("id", CaptchaHiddenInputName);
            hiddenInput.Attributes.Add("name", CaptchaHiddenInputName);
            hiddenInput.Attributes.Add("type", "hidden");
            hiddenInput.Attributes.Add("value", encryptedText);
            return hiddenInput;
        }

        private static TagBuilder GetHiddenInputTokenTagBuilder(string token)
        {
            var hiddenInput = new TagBuilder("input");
            hiddenInput.Attributes.Add("id", CaptchaHiddenTokenName);
            hiddenInput.Attributes.Add("name", CaptchaHiddenTokenName);
            hiddenInput.Attributes.Add("type", "hidden");
            hiddenInput.Attributes.Add("value", token);
            return hiddenInput;
        }

        private TagBuilder GetCaptchaImageTagBuilder(string encryptedText)
        {
            ViewContext.CheckArgumentNull(nameof(ViewContext));

            IUrlHelper urlHelper = new UrlHelper(ViewContext);
            var actionUrl = urlHelper.Action(action: nameof(CaptchaImageController.Show),
                controller: nameof(CaptchaImageController).Replace("Controller", string.Empty),
                values:
                new
                {
                    text = encryptedText,
                    rndDate = DateTime.Now.Ticks,
                    foreColor = ForeColor,
                    backColor = BackColor,
                    fontSize = FontSize,
                    fontName = FontName,
                    area = ""
                });

            var captchaImage = new TagBuilder("img");
            var dntCaptchaImg = "captchaImg";
            captchaImage.Attributes.Add("id", dntCaptchaImg);
            captchaImage.Attributes.Add("name", dntCaptchaImg);
            captchaImage.Attributes.Add("alt", "captcha");
            captchaImage.Attributes.Add("src", actionUrl);
            captchaImage.Attributes.Add("style", "margin-bottom: 4px;");
            return captchaImage;
        }

        private TagBuilder GetRefreshButtonTagBuilder(string captchaDivId, string captchaToken)
        {
            IUrlHelper urlHelper = new UrlHelper(ViewContext);
            var actionUrl = urlHelper.Action(action: nameof(CaptchaImageController.Refresh),
                controller: nameof(CaptchaImageController).Replace("Controller", string.Empty),
                values:
                new
                {
                    rndDate = DateTime.Now.Ticks,
                    area = "",
                    BackColor = BackColor,
                    FontName = FontName,
                    FontSize = FontSize,
                    ForeColor = ForeColor,
                    Language = Language,
                    Max = Max,
                    Min = Min,
                    Placeholder = Placeholder,
                    TextBoxClass = TextBoxClass,
                    TextBoxTemplate = TextBoxTemplate,
                    ValidationErrorMessage = ValidationErrorMessage,
                    ValidationMessageClass = ValidationMessageClass,
                    CaptchaToken = captchaToken,
                    RefreshButtonClass = RefreshButtonClass,
                    OutputType = OutputType,
                    ImageOnly = ImageOnly
                });

            var refreshButton = new TagBuilder("a");
            var dntCaptchaRefreshButton = "captchaRefreshButton";
            refreshButton.Attributes.Add("id", dntCaptchaRefreshButton);
            refreshButton.Attributes.Add("name", dntCaptchaRefreshButton);
            refreshButton.Attributes.Add("href", "#refresh");
            refreshButton.Attributes.Add("data-ajax-url", actionUrl);
            refreshButton.Attributes.Add("data-ajax", "true");
            refreshButton.Attributes.Add("data-ajax-method", "POST");
            refreshButton.Attributes.Add("data-ajax-mode", "replace-with");
            refreshButton.Attributes.Add("data-ajax-update", $"#{captchaDivId}");
            refreshButton.Attributes.Add("class", RefreshButtonClass);
            return refreshButton;
        }

        private TagBuilder GetTextInputTagBuilder()
        {
            var textInput = new TagBuilder("input");
            textInput.Attributes.Add("id", CaptchaInputName);
            textInput.Attributes.Add("name", CaptchaInputName);
            textInput.Attributes.Add("autocomplete", "off");
            textInput.Attributes.Add("class", TextBoxClass);
            textInput.Attributes.Add("data-val", "true");
            textInput.Attributes.Add("data-val-required", ValidationErrorMessage);
            textInput.Attributes.Add("placeholder", Placeholder);
            textInput.Attributes.Add("dir", "ltr");
            textInput.Attributes.Add("type", "text");
            textInput.Attributes.Add("value", "");
            return textInput;
        }

        private TagBuilder GetValidationMessageTagBuilder()
        {
            var validationMessage = new TagBuilder("span");
            validationMessage.Attributes.Add("class", ValidationMessageClass);
            validationMessage.Attributes.Add("data-valmsg-for", CaptchaInputName);
            validationMessage.Attributes.Add("data-valmsg-replace", "true");

            if (!ViewContext.ModelState.IsValid)
            {
                ModelStateEntry captchaInputNameValidationState;
                if (ViewContext.ModelState.TryGetValue(CaptchaInputName, out captchaInputNameValidationState))
                {
                    if (captchaInputNameValidationState.ValidationState == ModelValidationState.Invalid)
                    {
                        var error = captchaInputNameValidationState.Errors.FirstOrDefault();
                        if (error != null)
                        {
                            var errorSpan = new TagBuilder("span");
                            errorSpan.InnerHtml.AppendHtml(error.ErrorMessage);
                            validationMessage.InnerHtml.AppendHtml(errorSpan);
                        }
                    }
                }
            }

            return validationMessage;
        }
    }
}