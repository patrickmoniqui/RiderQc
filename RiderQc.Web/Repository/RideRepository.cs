using RiderQc.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly IRideDao dao;

        public RideRepository(/*IRideDao _dao*/)
        {
            dao = new RideDao();
        }

        public List<Ride> GetAllRides()
        {
            return dao.GetAllRides();
        }
    }
}