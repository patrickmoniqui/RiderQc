using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using System.Collections.Generic;

namespace RiderQc.Web.Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly IRideDao dao;

        public RideRepository(IRideDao _dao)
        {
            dao = _dao;
        }

        public List<Ride> GetAllRides()
        {
            return dao.GetAllRides();
        }
    }
}