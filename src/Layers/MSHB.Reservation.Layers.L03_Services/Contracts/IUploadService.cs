using Microsoft.AspNetCore.Http;
using MSHB.Reservation.Layers.L01_Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IUploadService
    {
        Task<Guid> UploadFileAsync(User user, IFormFile file);
    }
}
