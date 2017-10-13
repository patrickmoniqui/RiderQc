using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Trajet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiderQc.Web.Repository.Interface
{
    public interface ITrajetRepository
    {
        bool Create(TrajetViewModel trajetViewModel);
        bool Delete(int trajetId);
        TrajetViewModel Get(int trajetId);
        List<TrajetViewModel> GetAllTrajets();
    }
}
