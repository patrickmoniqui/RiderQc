using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using System.Collections.Generic;
using RiderQc.Web.ViewModels.Ride;
using System;

namespace RiderQc.Web.Repository
{
    public class TrajetRepository : ITrajetRepository
    {
        private readonly ITrajetDao dao;

        public TrajetRepository(ITrajetDao _dao)
        {
            dao = _dao;
        }

        public bool Create(TrajetViewModel trajetViewModel)
        {
            //mapping
            Trajet trajet = new Trajet();

            return dao.Create(trajet);
        }

        public bool Delete(int trajetId)
        {
            return dao.Delete(trajetId);
        }

        public Trajet Get(int trajetId)
        {
            return dao.Get(trajetId);
        }

        public List<Trajet> GetAllTrajets()
        {
            return dao.GetAllTrajets();
        }
    }
}