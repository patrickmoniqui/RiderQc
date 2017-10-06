using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface ITrajetRepository
    {
        bool Create(TrajetViewModel trajetViewModel);
        bool Delete(int trajetId);

        Trajet Get(int trajetId);
        List<Trajet> GetAllTrajets();
    }
}
