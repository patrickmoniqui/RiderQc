using RiderQc.Web.Helpers;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Admin;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;
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
            if (Authenticate())
            {
                List<UserViewModel> users = repo.GetAllUsers();
                return View(users);
            }

            return Redirect("/admin/account/login");
        }

        [Route("edit/{userid}")]
        public ActionResult EditUser(int userid)
        {
            if (Authenticate())
            {
                UserAdminViewModel user = repo.GetUserAdminById(userid);
                return View(user);
            }

            return Redirect("/admin/account/login");
        }

        [Route("delete/{username}")]
        public ActionResult DeleteUser(string username)
        {
            if (Authenticate())
            {
                repo.DeleteUser(username);
                return View();
            }

            return Redirect("/admin/account/login");
        }

        [Route("create")]
        public ActionResult CreateNewUser()
        {
            if (Authenticate())
                return View();

            return Redirect("/admin/account/login");
        }

        [Route("detail/{userid}")]
        public ActionResult DetailUser(int userid)
        {
            if (Authenticate())
            {
                UserAdminViewModel user = repo.GetUserAdminById(userid);
                return View(user);
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