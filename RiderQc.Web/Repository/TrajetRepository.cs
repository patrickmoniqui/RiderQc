using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RiderQc.Web.Repository
{
    public class TrajetRepository : ITrajetRepository
    {
        private readonly ITrajetDao dao;

        public TrajetRepository(ITrajetDao _dao)
        {
            dao = _dao;
        }

        public bool Create(TrajetCreateViewModel trajetViewModel)
        {
            Trajet trajet = new Trajet();
            trajet.CreatorId = trajetViewModel.CreatorId;
            trajet.Title = trajetViewModel.Title;
            trajet.Description = trajetViewModel.Description;
            string googleCo = string.Join(";", trajetViewModel.GpsPoints);
            trajet.GoogleCo = googleCo;
            
            return dao.Create(trajet);
        }

        public bool Delete(int trajetId)
        {
            return dao.Delete(trajetId);
        }

        public bool Exist(int trajetId)
        {
            return dao.Exist(trajetId);
        }

        public TrajetViewModel Get(int trajetId)
        {
            TrajetViewModel trajetViewModel = new TrajetViewModel();
            Trajet trajet = dao.Get(trajetId);

            if(trajet == null)
            {
                return null;
            }
            
            return ToTrajetViewModel(trajet);

        }

        public List<TrajetViewModel> GetAllTrajets()
        {
            List<TrajetViewModel> list = new List<TrajetViewModel>();
            List<Trajet> trajets = dao.GetTrajetList();

            foreach (Trajet trajet in trajets)
            {
                list.Add(ToTrajetViewModel(trajet));
            }

            return list;
        }

        public bool Update(TrajetCreateViewModel trajetViewModel)
        {
            Trajet trajet = new Trajet();
            trajet.TrajetId = trajetViewModel.TrajetId;
            trajet.CreatorId = trajetViewModel.CreatorId;
            trajet.Title = trajetViewModel.Title;
            trajet.Description = trajetViewModel.Description;
            string googleCo = string.Join(";", trajetViewModel.GpsPoints);
            trajet.GoogleCo = googleCo;

            return dao.Update(trajet);
        }

        private TrajetViewModel ToTrajetViewModel(Trajet trajet)
        {
            TrajetViewModel trajetViewModel = new TrajetViewModel();
            trajetViewModel.TrajetId = trajet.TrajetId;
            trajetViewModel.Title = trajet.Title;
            trajetViewModel.Description = trajet.Description;
            trajetViewModel.GpsPoints = trajet.GoogleCo.Split(';').ToList();

            UserSimpleViewModel userSimpleViewModel = new UserSimpleViewModel();
            userSimpleViewModel.UserID = trajet.User.UserID;
            userSimpleViewModel.Username = trajet.User.Username;
            trajetViewModel.Creator = userSimpleViewModel;

            return trajetViewModel;
        }
    }
}