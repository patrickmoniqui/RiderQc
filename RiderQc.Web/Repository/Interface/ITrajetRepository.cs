using RiderQc.Web.ViewModels.Api.Trajet;
using RiderQc.Web.ViewModels.Trajet;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface ITrajetRepository
    {
        bool Create(TrajetCreateViewModel trajetViewModel);
        bool Update(TrajetCreateViewModel trajetViewModel);
        bool Delete(int trajetId);
        bool Exist(int trajetId);
        TrajetViewModel Get(int trajetId);
        List<TrajetViewModel> GetAllTrajets();
        bool CreateAdminTrajet(TrajetAdminCreateViewModel trajetVM);
    }
}
