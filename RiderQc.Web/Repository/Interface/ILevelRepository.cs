using RiderQc.Web.ViewModels.Level;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface ILevelRepository
    {
        bool Create(LevelCreateViewModel levelViewModel);
        LevelViewModel Get(int levelId);
        bool Exist(int levelId);
        List<LevelViewModel> GetLevelList();
    }
}
