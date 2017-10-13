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
            List<RideViewModel> rides = repo.GetAllRides();
            return View(rides);
        }
        [Route("edit/{rideid}")]
        public ActionResult EditRide(int rideid)
        {
            RideViewModel ride = repo.Get(rideid);
            return View(ride);
        }
        [Route("delete/{rideid}")]
        public ActionResult DeleteRide(int rideid)
        {
            repo.Delete(rideid);
            return View();
        }
        [Route("create")]
        public ActionResult CreateNewRide()
        {
            return View();
        }
        [Route("detail/{rideid}")]
        public ActionResult DetailRide(int rideid)
        {
            RideViewModel ride = repo.Get(rideid);
            return View(ride);
        }


    }
}