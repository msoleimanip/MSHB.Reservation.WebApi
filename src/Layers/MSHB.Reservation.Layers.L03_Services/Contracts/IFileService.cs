using Microsoft.AspNetCore.Http;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IFileService
    {
        Task<Guid> UploadAsync(User user, IFormFile file);
        Task<DownloadViewModel> DownloadAsync(User user, Guid fileId);
    }
}
