using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Trajet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public TrajetViewModel Get(int trajetId)
        {
            TrajetViewModel trajetViewModel = new TrajetViewModel();
            Trajet trajet = dao.Get(trajetId);
            trajetViewModel.TrajetId = trajet.TrajetId;
            trajetViewModel.GpsPoints = new List<string>();
            var gpsPoints = trajet.GoogleCo.Split(';');
            trajetViewModel.GpsPoints.AddRange(gpsPoints);
            return trajetViewModel;

        }

        public List<TrajetViewModel> GetAllTrajets()
        {
            List<TrajetViewModel> list = new List<TrajetViewModel>();

            List<Trajet> trajets = dao.GetTrajetList();

            foreach (Trajet trajet in trajets)
            {
                TrajetViewModel trajetViewModel = new TrajetViewModel();
                trajetViewModel.TrajetId = trajet.TrajetId;
                trajetViewModel.GpsPoints = new List<string>();

                var gpsPoints = trajet.GoogleCo.Split(';');

                trajetViewModel.GpsPoints.AddRange(gpsPoints);

                list.Add(trajetViewModel);
            }

            return list;
        }
    }
}