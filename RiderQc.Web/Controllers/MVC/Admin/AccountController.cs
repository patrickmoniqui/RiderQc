using RiderQc.Web.Entities;
using RiderQc.Web.Helpers;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Admin;
using RiderQc.Web.ViewModels.User;
using System.Web.Mvc;

namespace RiderQc.Web.Controllers.MVC.Admin
{
    [RoutePrefix("admin/account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository repo;
        private readonly IUserRoleRepository repo2;

        public AccountController(IUserRepository _repo, IUserRoleRepository _repo2)
        {
            repo = _repo;
            repo2 = _repo2;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public ActionResult Login(AdminLoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                //Gets user from DB
                User userFromDB = repo.GetUserByNameAdmin(user.Username);
                //See if it matches
                if (userFromDB != null)
                    if (userFromDB.Username == user.Username && userFromDB.Password == EncryptionHelper.HashToSHA256(user.Password))
                    {
                        if (repo2.GetUserRole(userFromDB.UserID).RoleId == Constant.UserRole_Admin)
                        {
                            Session["Username"] = userFromDB.Username;
                            return RedirectToAction("Index", "UserAdmin");
                        }
                    }
            }
            ModelState.AddModelError("LoginError", "Invalid login attempt");
            return View(user);
            //jai besoin de chier
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult LogOut()
        {
            Session["Username"] = null;
            return RedirectToAction("Login");
        }
    }
}