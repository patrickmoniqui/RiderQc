using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface ITrajetDao
    {
        List<Trajet> GetTrajetList();
    }
}
