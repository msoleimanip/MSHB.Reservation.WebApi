using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IBookinationService
    {
        Task<bool> AddAsync(AddBookinationFormModel AddFomrModel);
    }
}
