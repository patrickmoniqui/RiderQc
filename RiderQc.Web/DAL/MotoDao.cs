using System.Collections.Generic;
using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Data.Entity;
using System.Linq;

namespace RiderQc.Web.DAL
{
    public class MotoDao : IMotoDao
    {
        public bool CreateMoto(Moto moto)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Motos.Add(moto);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public bool EditMoto(Moto moto)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                if (moto == null)
                    return false;

                ctx.Entry(moto).State = EntityState.Modified;

                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public bool DeleteMoto(int motoId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Moto moto = ctx.Motos.Find(motoId);

                if (moto == null)
                    return false;

                ctx.Motos.Remove(moto);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public Moto GetMoto(int motoId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
                return ctx.Motos.Find(motoId);
        }

        public List<Moto> GetAllMotos()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var motos = ctx.Motos
                    .Include(x => x.User);

                if (motos != null)
                    return motos.ToList();
                else
                    return new List<Moto>();
            }
        }
    }
}