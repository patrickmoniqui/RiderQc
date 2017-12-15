using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Admin;
using RiderQc.Web.ViewModels.Comment;
using RiderQc.Web.ViewModels.Level;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.ViewModels.User;
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
        private readonly IUserRepository repo2;
        public TrajetAdminController(ITrajetRepository _repo, IUserRepository _repo2)
        {
            repo = _repo;
            repo2 = _repo2;
        }

        [Route("list")]
        public ActionResult Index()
        {
            if (Authenticate())
            {
                List<TrajetViewModel> trajets = repo.GetAllTrajets();
                return View(trajets);
            }
            return Redirect("/admin/account/login");
        }
        [Route("edit/{trajetid}")]
        public ActionResult Edittrajet(int trajetid)
        {
            if (Authenticate())
            {
                TrajetViewModel trajet = repo.Get(trajetid);
                List<UserViewModel> users = repo2.GetAllUsers();
                ViewBag.Edittrajet = users;
                return View(trajet);
            }
            return Redirect("/admin/account/login");
        }
        [Route("delete/{trajetid}")]
        public ActionResult DeleteTrajet(int trajetid)
        {
            if (Authenticate())
            {
                repo.Delete(trajetid);
                return View();
            }
            return Redirect("/admin/account/login");
        }
        [Route("create")]
        public ActionResult CreateNewTrajet()
        {
            if (Authenticate())
            {
                
                return View();
            }
            return Redirect("/admin/account/login");
        }
        [Route("detail/{trajetid}")]
        public ActionResult DetailTrajet(int trajetid)
        {
            if (Authenticate())
            {
                TrajetViewModel trajet = repo.Get(trajetid);
                return View(trajet);
            }
            return Redirect("/admin/account/login");
        }
        public bool Authenticate()
        {
            if (Session != null)
            {
                if (Session["Username"] != null)
                    return true; ;
            }
            return false;
        }
    }
}