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