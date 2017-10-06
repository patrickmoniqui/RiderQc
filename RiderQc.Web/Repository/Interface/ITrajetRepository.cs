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
        Trajet Get(int trajetId);
        List<Trajet> GetAllTrajets();
    }
}
