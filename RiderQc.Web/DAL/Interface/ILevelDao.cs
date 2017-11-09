using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface ILevelDao
    {
        bool Create(Level level);
        Level Get(int levelId);
        bool Exist(int levelId);
        List<Level> GetLevelList();
    }
}
