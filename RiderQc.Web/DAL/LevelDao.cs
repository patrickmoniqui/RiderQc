using RiderQc.Web.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiderQc.Web.Entities;

namespace RiderQc.Web.DAL
{
    public class LevelDao : ILevelDao
    {
        public bool Create(Level level)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
               ctx.Levels.Add(level);
               result = ctx.SaveChanges();
            }

            return result >= 1 ? true : false;
        }

        public bool Exist(int levelId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Levels.Any(x => x.LevelId == levelId);
            }
        }

        public Level Get(int levelId)
        {
            Level level = null;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                level = ctx.Levels.FirstOrDefault(x => x.LevelId == levelId);
            }

            return level;
        }

        public List<Level> GetLevelList()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var list = ctx.Levels;

                if (list != null)
                {
                    return list.ToList();
                }
                else
                {
                    return new List<Level>();
                }
            }
        }
    }
}