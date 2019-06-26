using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L00_BaseModels.ContentType;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L00_BaseModels.Settings;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class FileService: IFileService
    {
        private readonly ReservationDbContext _context;
        private readonly IOptionsSnapshot<SiteSettings> _siteSettings;

        public FileService(ReservationDbContext context, IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
            _siteSettings = siteSettings;
            _siteSettings.CheckArgumentIsNull(nameof(_siteSettings));
        }

        public async  Task<DownloadViewModel> DownloadAsync(User user, Guid fileId)
        {
            try
            {
                var fileAddress=await _context.FileAddresses.FindAsync(fileId);
                var downloadViewModel = new DownloadViewModel();
                
                if (fileAddress!=null)
                {
                    if (File.Exists(fileAddress.FilePath))
                    {
                        var memory = new MemoryStream();
                        using (var stream = new FileStream(fileAddress.FilePath, FileMode.Open))
                        {
                            await stream.CopyToAsync(memory);
                        }
                        memory.Position = 0;
                        downloadViewModel.ContentType = ContentType.GetContentType(fileAddress.FilePath);
                        downloadViewModel.FileName = Path.GetFileName(fileAddress.FilePath);
                        downloadViewModel.Memory = memory;                                            
                    }                 

                }
                return downloadViewModel;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(UploadServiceErrors.FileDownloadError,ex);
            }
           
        }
     

        public async Task<Guid> UploadAsync(User user, IFormFile file)
        {
            try
            {
                if (string.IsNullOrEmpty(file.FileName) || file.Length == 0)
                {
                    throw new ReservationGlobalException(UploadServiceErrors.UploadFileValidError);
                }
                var extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString()+"."+ extension;
                var path = Path.Combine(_siteSettings.Value.UserAttachedFile.PhysicalPath,  fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }
                var uploadFile = new FileAddress()
                {
                    FilePath = path,
                    FileType = extension,
                    UserId = user.Id,
                    FileSize = file.Length,
                    CreationDate = DateTime.Now,
                    
                };
                await _context.FileAddresses.AddAsync(uploadFile);
                await _context.SaveChangesAsync();

                return uploadFile.FileId;

            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(UploadServiceErrors.UploadFileError, ex);
            }
        }

     

    }
}
