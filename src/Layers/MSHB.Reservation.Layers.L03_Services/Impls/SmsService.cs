using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MSHB.Reservation.Layers.L00_BaseModels.Settings;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using Newtonsoft.Json;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class SmsService:ISmsService
    {
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;
        private readonly HttpClient _httpClient;

        public SmsService(IOptionsSnapshot<SiteSettings> siteSettings, HttpClient httpclient)
        {
            _siteSettings = siteSettings;
            _siteSettings.CheckArgumentIsNull(nameof(_siteSettings));
            _httpClient = httpclient;
            _httpClient.CheckArgumentIsNull(nameof(_httpClient));
        }

        public async Task<bool> SendSmsAsync( SendSmsModel smsModel)
        {
            try
            {
                var response = await _httpClient.PostAsync(_siteSettings.Value.SmsUrl, CreateHttpContent<SendSmsModel>(smsModel));
               
                return true;
            }
            catch 
            {
                return false;
            }
        }
        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
    }
}