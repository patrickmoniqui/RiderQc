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
        [Route("list")]
        public ActionResult Index()
        {
            List<TrajetViewModel> trajets = repo.GetAllTrajets();
            return View(trajets);
        }
        [Route("edit/{trajetid}")]
        public ActionResult Edittrajet(int trajetid)
        {
            TrajetViewModel trajet = repo.Get(trajetid);
            return View(trajet);
        }
        [Route("delete/{trajetid}")]
        public ActionResult DeleteTrajet(int trajetid)
        {
            repo.Delete(trajetid);
            return View();
        }
        [Route("create")]
        public ActionResult CreateNewTrajet()
        {
            return View();
        }
        [Route("detail/{trajetid}")]
        public ActionResult DetailTrajet(int trajetid)
        {
            TrajetViewModel trajet = repo.Get(trajetid);
            return View(trajet);
        }


    }
}