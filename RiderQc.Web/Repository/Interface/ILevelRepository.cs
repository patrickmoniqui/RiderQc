using RiderQc.Web.ViewModels.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiderQc.Web.Repository.Interface
{
    public interface ILevelRepository
    {
        List<LevelViewModel> GetLevelList();
    }
}
