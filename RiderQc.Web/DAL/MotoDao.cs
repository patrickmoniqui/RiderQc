using System.Collections.Generic;
using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Data.Entity;
using System.Linq;
using System;

namespace RiderQc.Web.DAL
{
    public class MotoDao : IMotoDao
    {
        public bool CreateMoto(Moto moto)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Motoes.Add(moto);
                int result = ctx.SaveChanges();

                return result >= 1 ? true : false;
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

                return result >= 1 ? true : false;
            }
        }

        public bool DeleteMoto(int motoId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Moto moto = ctx.Motoes.Find(motoId);

                if (moto == null)
                    return false;

                ctx.Motoes.Remove(moto);
                int result = ctx.SaveChanges();

                return result >= 1 ? true : false;
            }
        }

        public Moto GetMoto(int motoId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
                return ctx.Motoes.Find(motoId);
        }

        public List<Moto> GetAllMotos()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var motos = ctx.Motoes
                    .Include(x => x.User);

                if (motos != null)
                    return motos.ToList();
                else
                    return new List<Moto>();
            }
        }

        public bool UserHasAccess(int motoId, string username)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Moto moto = ctx.Motoes.FirstOrDefault(x => x.MotoId == motoId);
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                if (moto == null || user == null)
                {
                    return false;
                }
                else
                {
                    if (moto.UserId == user.UserID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}