using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface ITrajetDao
    {
        bool Create(Trajet trajet);
        bool Update(Trajet trajet);
        bool Delete(int trajetId);
        bool Exist(int trajetId);
        Trajet Get(int trajetId);
        List<Trajet> GetTrajetList();
    }
}
