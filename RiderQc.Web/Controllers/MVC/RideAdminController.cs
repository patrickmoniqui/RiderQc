using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Ride;

namespace RiderQc.Web.Controllers
{
    [RoutePrefix("admin/ride")]

    public class RideAdminController : Controller
    {
        // GET: RideAdmin
        private readonly IRideRepository repo;
        public RideAdminController(IRideRepository _repo)
        {
            repo = _repo;
        }

        [Route("list")]
        public ActionResult Index()
        {
            if (Authenticate())
            {
                List<RideViewModel> rides = repo.GetAllRides();
                return View(rides);
            }
            return Redirect("/admin/account/login");
        }
        [Route("pimp/{rideid}")]
        public ActionResult EditRide(int rideid)
        {
            if (Authenticate())
            {
                RideViewModel ride = repo.Get(rideid);
                return View(ride);
            }
            return Redirect("/admin/account/login");
        }
        [Route("delete/{rideid}")]
        public ActionResult DeleteRide(int rideid)
        {
            if (Authenticate())
            {
                repo.Delete(rideid);
                return View();
            }
            return Redirect("/admin/account/login");
        }
        [Route("create")]
        public ActionResult CreateNewRide()
        {
            if (Authenticate())
            {
                return View();
            }
            return Redirect("/admin/account/login");
        }
        [Route("detail/{rideid}")]
        public ActionResult DetailRide(int rideid)
        {
            if (Authenticate())
            {
                RideViewModel ride = repo.Get(rideid);
                return View(ride);
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