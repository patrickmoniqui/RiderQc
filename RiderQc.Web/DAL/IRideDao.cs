using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiderQc.Web.DAL
{
    public interface IRideDao
    {
        List<Ride> GetAllRides();
    }
}
