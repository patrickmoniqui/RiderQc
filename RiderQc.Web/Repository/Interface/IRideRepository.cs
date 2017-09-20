using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IRideRepository
    {
        List<Ride> GetAllRides();
    }
}
