using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Level;
using System.Collections.Generic;

namespace RiderQc.Web.Repository
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ILevelDao dao;
        
        public LevelRepository(ILevelDao _dao)
        {
            dao = _dao;
        }

        public List<LevelViewModel> GetLevelList()
        {
            List<LevelViewModel> levels = new List<LevelViewModel>();

            foreach(Level level in dao.GetLevelList())
            {
                LevelViewModel l = new LevelViewModel();
                l.Id = level.LevelId;
                l.Name = level.Name;
                levels.Add(l);
            }

            return levels;
        }
    }
}