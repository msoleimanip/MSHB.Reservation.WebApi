using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public  interface ISmsService
    {
         Task<bool> SendSmsAsync( SendSmsModel smsModel);
    }
}