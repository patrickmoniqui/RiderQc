using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

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

                return result >= 1 ? true : false;
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

                return result >= 1 ? true : false;
            }
        }

        public bool Exist(int trajetId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Trajets.Any(x => x.TrajetId == trajetId);
            }
        }

        public Trajet Get(int trajetId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Trajets
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.TrajetId == trajetId);
            }
        }

        public List<Trajet> GetTrajetList()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                List<Trajet> trajets = ctx.Trajets.Include(x => x.User).ToList();

                if(trajets == null)
                {
                    return null;
                }
                return trajets;
            }
        }

        public bool Update(Trajet trajet)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Trajet _trajet = ctx.Trajets.FirstOrDefault(x => x.TrajetId == trajet.TrajetId);
                _trajet.Title = trajet.Title;
                _trajet.Description = trajet.Description;
                _trajet.GoogleCo = trajet.GoogleCo;

                ctx.Entry<Trajet>(_trajet).State = System.Data.Entity.EntityState.Modified;
                
                int result = ctx.SaveChanges();

                return result >= 1 ? true : false;
            }
        }
    }
}