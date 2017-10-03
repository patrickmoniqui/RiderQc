using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Moto;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IMotoRepository
    {
        bool CreateMoto(MotoViewModel motoViewModel);
        bool EditMoto(MotoViewModel motoViewModel);
        bool DeleteMoto(int motoId);
        bool UserHasAccess(int motoId, string username);
        Moto GetMoto(int motoId);
        List<Moto> GetAllMotos();
    }
}