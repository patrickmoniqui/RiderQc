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

        public bool Create(LevelCreateViewModel levelViewModel)
        {
            Level level  = new Level();
            level.Name = levelViewModel.Name;

            return dao.Create(level);
        }

        public bool Exist(int levelId)
        {
            return dao.Exist(levelId);
        }

        public LevelViewModel Get(int levelId)
        {
            LevelViewModel levelViewModel = new LevelViewModel();
            Level level = dao.Get(levelId);

            levelViewModel.LevelId = level.LevelId;
            levelViewModel.Name = level.Name;

            return levelViewModel;
        }

        public List<LevelViewModel> GetLevelList()
        {
            List<LevelViewModel> levels = new List<LevelViewModel>();

            foreach(Level level in dao.GetLevelList())
            {
                LevelViewModel l = new LevelViewModel();
                l.LevelId = level.LevelId;
                l.Name = level.Name;
                levels.Add(l);
            }

            return levels;
        }
    }
}