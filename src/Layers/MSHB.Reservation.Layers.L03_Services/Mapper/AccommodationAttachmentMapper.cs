using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Mapper
{
    public static class AccommodationAttachmentMapper
    {
        public static IQueryable<AccommodationAttachmentViewModel> Mapper(this IQueryable<AccommodationAttachment> model)
        {
            return model.Select(x => new AccommodationAttachmentViewModel
            {
                AccommodationId = x.AccommodationId,
                FileId = x.FileId,
                FileSize = x.FileSize,
                FileType = x.FileType,
                Id = x.Id
            });
        }
    }
}
