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
            List<UserViewModel> users = repo.GetAllUsers();
            return View(users);
        }

        [Route("edit/{userid}")]
        public ActionResult EditUser(int userid)
        {
            UserAdminViewModel user = repo.GetUserAdminById(userid);
            return View(user);
        }

        [Route("delete/{username}")]
        public ActionResult DeleteUser(string username)
        {
            repo.DeleteUser(username);
            return View();
        }

        [Route("create")]
        public ActionResult CreateNewUser()
        {
            return View();
        }

        [Route("detail/{userid}")]
        public ActionResult DetailUser(int userid)
        {
            UserAdminViewModel user = repo.GetUserAdminById(userid);
            return View(user);
        }
    }
}