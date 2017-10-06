using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;

namespace RiderQc.Web.Controllers
{
    public class RideAdminController : Controller
    {
        // GET: RideAdmin
        private readonly IRideRepository repo;

        public RideAdminController(IRideRepository _repo)
        {
            repo = _repo;
        }

        public ActionResult Index()
        {
            List<Ride> rides = repo.GetAllRides();
            return View(rides);
        }

        public ActionResult EditRide(int rideid)
        {
            Ride ride = repo.Get(rideid);
            return View(ride);
        }

        public ActionResult DeleteRide(int rideid)
        {
            repo.Delete(rideid);
            return View();
        }

        public ActionResult CreateNewRide()
        {
            return View();
        }

        public ActionResult DetailRide(int rideid)
        {
            Ride ride = repo.Get(rideid);
            return View(ride);
        }


    }
}