using AutoMapper;
using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Comment;
using RiderQc.Web.ViewModels.Level;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace RiderQc.Web.DAL
{
    public class RideDao : IRideDao
    {
        public bool Create(Ride ride)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Rides.Add(ride);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public bool Delete(int rideId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Ride ride = ctx.Rides.Find(rideId);

                if(ride == null)
                {
                    return false;
                }

                ctx.Rides.Remove(ride);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public RideViewModel Get(int rideId)
        {
            Ride ride = null;

            using (RiderQcContext ctx = new RiderQcContext())
            {
               ride = ctx.Rides
                    .Include(x => x.User)
                    .Include(x => x.Level)
                    .Include(x => x.Trajet)
                    .Include(x => x.Comments)
                    // include user information (creator)
                    .Include("Comments.User")
                    // include child comments of comment
                    .Include("Comments.Comment1")
                    .Include("Comments.Comment1.User")
                    .AsNoTracking()
                    .SingleOrDefault(x => x.RideId == rideId);
            }

            RideViewModel rideViewModel = RideToRideViewMdodel(ride);
            return rideViewModel;
        }

        public List<RideViewModel> GetAllRides()
        {
            List<Ride> rides = new List<Ride>();

            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                ctx.Configuration.LazyLoadingEnabled = false;

                rides = ctx.Rides
                    .Include(x => x.User)
                    .Include(x => x.Level)
                    .Include(x => x.Trajet)
                    .Include(x => x.Comments)
                    // include user information (creator)
                    .Include("Comments.User")
                    // include child comments of comment
                    .Include("Comments.Comment1")
                    .Include("Comments.Comment1.User")
                    .AsNoTracking()
                    .ToList();
            }

            List<RideViewModel> ridesViewModel = new List<RideViewModel>();

            foreach(Ride ride in rides)
            {
                RideViewModel rideViewModel = RideToRideViewMdodel(ride);
                ridesViewModel.Add(rideViewModel);
            }
            
            return ridesViewModel;
        }

        private RideViewModel RideToRideViewMdodel(Ride ride)
        {
            RideViewModel rideViewModel = new RideViewModel();
            rideViewModel.RideId = ride.RideId;
            rideViewModel.Title = ride.Title;
            rideViewModel.Description = ride.Description;
            rideViewModel.CreatorId = ride.CreatorId;
            rideViewModel.TrajetId = ride.TrajetId;
            rideViewModel.DateDepart = ride.DateDepart;
            rideViewModel.DateFin = ride.DateFin;

            LevelViewModel levelViewModel = new LevelViewModel();
            levelViewModel.LevelId = ride.Level.LevelId;
            levelViewModel.Name = ride.Level.Name;
            rideViewModel.Level = levelViewModel;

            TrajetViewModel trajetViewModel = new TrajetViewModel();
            trajetViewModel.TrajetId = ride.Trajet.TrajetId;
            trajetViewModel.CreatorId = ride.Trajet.CreatorId;
            trajetViewModel.GpsPoints = ride.Trajet.GoogleCo.Split(';').ToList();
            rideViewModel.Trajet = trajetViewModel;

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserID = ride.User.UserID;
            userViewModel.Username = ride.User.Username;
            rideViewModel.User = userViewModel;

            rideViewModel.Comments = new List<CommentViewModel>();

            foreach (Comment comment in ride.Comments.Where(x => x.ParentId == null))
            {
                rideViewModel.Comments.Add(CommentToCommentViewModel(comment));
            }

            return rideViewModel;
        }

        private CommentViewModel CommentToCommentViewModel(Comment comment)
        {
            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.CommentId = comment.CommentId;
            commentViewModel.ParentId = comment.ParentId;
            commentViewModel.SenderId = comment.SenderId;
            commentViewModel.TimeStamp = comment.TimeStamp;
            commentViewModel.Vote = comment.Vote;
            commentViewModel.Blocked = comment.Blocked;
            commentViewModel.RideId = comment.RideId;

            commentViewModel.User = new UserViewModel() { UserID = comment.User.UserID, Username = comment.User.Username };
            commentViewModel.CommentText = comment.CommentText;
            commentViewModel.ChildComments = new List<CommentViewModel>();

            if(comment.Comment1 != null)
            {
                foreach(Comment comment2 in comment.Comment1)
                {
                    commentViewModel.ChildComments.Add(CommentToCommentViewModel(comment2));
                }
            }

            return commentViewModel;
        }
        
        public bool UserIsCreator(int rideId, string username)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Ride ride = ctx.Rides.FirstOrDefault(x => x.RideId == rideId);
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                if(ride == null || user == null)
                {
                    return false;
                }
                else
                {
                    if(ride.CreatorId == user.UserID)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Exist(int rideId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Rides.Any(x => x.RideId == rideId);
            }
        }
    }
}