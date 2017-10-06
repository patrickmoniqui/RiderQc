using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Trajet;

namespace RiderQc.Web.Controllers
{

    [RoutePrefix("admin/trajet")]
    public class TrajetAdminController : Controller
    {
        // GET: TrajetAdmin
        private readonly ITrajetRepository repo;

        public TrajetAdminController(ITrajetRepository _repo)
        {
            repo = _repo;
        }

        public ActionResult Index()
        {
            List<TrajetViewModel> trajets = repo.GetAllTrajets();
            return View(trajets);
        }

        public ActionResult Edittrajet(int trajetid)
        {
            TrajetViewModel trajet = repo.Get(trajetid);
            return View(trajet);
        }

        public ActionResult DeleteTrajet(int trajetid)
        {
            repo.Delete(trajetid);
            return View();
        }

        public ActionResult CreateNewTrajet()
        {
            return View();
        }

        public ActionResult DetailTrajet(int trajetid)
        {
            TrajetViewModel trajet = repo.Get(trajetid);
            return View(trajet);
        }


    }
}