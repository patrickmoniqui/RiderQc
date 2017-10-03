using RiderQc.Web.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.Entities;

namespace RiderQc.Web.DAL
{
    public class TrajetDao : ITrajetDao
    {
        public List<Trajet> GetTrajetList()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var trajets = ctx.Trajets;

                if(trajets != null)
                {
                    return ctx.Trajets.ToList();
                }
            }
            return new List<Trajet>();
        }
    }
}