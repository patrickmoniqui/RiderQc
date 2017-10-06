using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace RiderQc.Web.DAL
{
    public class TrajetDao : ITrajetDao
    {
        public bool Create(Trajet trajet)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Trajets.Add(trajet);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public bool Delete(int trajetId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Trajet trajet = ctx.Trajets.Find(trajetId);

                if (trajet == null)
                {
                    return false;
                }

                ctx.Trajets.Remove(trajet);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public Trajet Get(int trajetId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Trajets.Find(trajetId);
            }
        }

        public List<Trajet> GetAllTrajets()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var trajets = ctx.Trajets
                    .Include(x => x.Rides)
                    .Include(x => x.Comments);

                if (trajets != null)
                {
                    return trajets.ToList();
                }
                else
                {
                    return new List<Trajet>();
                }
            }
        }
    }
}