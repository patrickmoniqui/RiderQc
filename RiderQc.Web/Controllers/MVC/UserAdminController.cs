using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.ViewModels.User;
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

        [Route("list")]
        public ActionResult Index()
        {
            List<UserViewModel> users = repo.GetAllUsers();
            return View(users);
        }

        [Route("edit/{userid}")]
        public ActionResult EditUser(int userid)
        {
            UserViewModel user = repo.GetUserById(userid);
            return View(user);
        }

        [Route("delete/{username}")]
        public ActionResult DeleteUser(string username)
        {
            repo.DeleteUser(username);
            return View();
        }

        [Route("CreateUser")]
        public ActionResult CreateNewUser()
        {
            return View();
        }

        [Route("detail/{userid}")]
        public ActionResult DetailUser(int userid)
        {
            UserViewModel user = repo.GetUserById(userid);
            return View(user);
        }
    }
}