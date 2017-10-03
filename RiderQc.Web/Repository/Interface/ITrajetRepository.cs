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
        List<TrajetViewModel> GetTrajetList();
    }
}
