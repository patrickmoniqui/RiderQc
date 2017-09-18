using RiderQc.Web.DAL;
using System.Collections.Generic;

namespace RiderQc.Web.Repository
{
    public interface IRideRepository
    {
        List<Ride> GetAllRides();
    }
}
