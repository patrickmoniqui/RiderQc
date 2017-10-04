using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiderQc.Web.Controllers
{
    [RoutePrefix("admin/user")]
    public class UserAdminController : Controller
    {
        // GET: UserAdmin
        private readonly IUserRepository repo;

        public UserAdminController(IUserRepository _repo)
        {
            repo = _repo;
        }

        [Route("liste")]
        public ActionResult Index()
        {
            List<User> users = repo.GetAllUsers();
            return View(users);
        }

        public ActionResult EditUser(int userid)
        {
            return View();
        }

        public ActionResult DeleteUser(string username)
        {
            repo.DeleteUser(username);
            return View();
        }

        public ActionResult CreateNewUser()
        {
            return View();
        }

        public ActionResult DetailUser(int userid)
        {
            return View();
        }
        

        public ActionResult ManageTrajet()
        {
            List<Trajet> trajets = repo.GetAllTrajets();
            return View(trajets);
        }

        public ActionResult ManageRide()
        {
            List<Ride> rides = repo.GetAllRides();
            return View(rides);
        }


    }
}