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

                return result >= 1 ? true : false;
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

                return result >= 1 ? true : false;
            }
        }

        public RideViewModel Get(int rideId)
        {
            Ride ride = null;
            RideViewModel rideViewModel = null;

            using (RiderQcContext ctx = new RiderQcContext())
            {
               ride = ctx.Rides
                    .Include(x => x.User)
                    .Include(x => x.Level)
                    .Include(x => x.Trajet)
                    .Include(x => x.Comments)
                    // include user information (creator)
                    .Include(x => x.Comments.Select(y => y.User))
                    // include child comments of comment
                    .Include(x => x.Comments.Select(y => y.ChildComments))
                    .Include(x => x.Comments.Select(y => y.ChildComments.Select(z => z.User)))
                    .Include(x => x.Participants)
                    .Include(x => x.RideRatings)
                    .SingleOrDefault(x => x.RideId == rideId);

                rideViewModel = RideToRideViewMdodel(ride);
            }
            
            return rideViewModel;
        }

        public List<RideViewModel> MyRidesForUser(string username)
        {
            List<Ride> rides = new List<Ride>();
            

            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                ctx.Configuration.LazyLoadingEnabled = false;

                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                rides = ctx.Rides
                    .Include(x => x.User)
                    .Include(x => x.Level)
                    .Include(x => x.Trajet)
                    .Include(x => x.Participants)
                    .Include(x => x.RideRatings)
                    .Where(x => x.Participants.Select(y => y.UserID).Contains(user.UserID))
                    .ToList();
            }

            List<RideViewModel> ridesViewModel = new List<RideViewModel>();

            foreach (Ride ride in rides)
            {
                RideViewModel rideViewModel = RideToRideViewMdodel(ride);
                ridesViewModel.Add(rideViewModel);
            }

            return ridesViewModel;
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
                    .Include(x => x.Comments.Select(y => y.User))
                    // include child comments of comment
                    .Include(x => x.Comments.Select(y => y.ChildComments))
                    .Include(x => x.Comments.Select(y => y.ChildComments.Select(z => z.User)))
                    .Include(x => x.Participants)
                    .Include(x => x.RideRatings)
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
            rideViewModel.LevelId = ride.LevelId;
            rideViewModel.CreatorId = ride.CreatorId;
            rideViewModel.TrajetId = ride.TrajetId;
            rideViewModel.DateDepart = ride.DateDepart;
            rideViewModel.DateFin = ride.DateFin;
            rideViewModel.Participants = ride.Participants.Select(x => x.Username).ToList();
            var listRatting = ride.RideRatings.Select(x => x.Rate);
            rideViewModel.RideRating = listRatting?.Count() > 0 ? listRatting.Average() : 0;

            LevelViewModel levelViewModel = new LevelViewModel();
            levelViewModel.LevelId = ride.Level.LevelId;
            levelViewModel.Name = ride.Level.Name;
            rideViewModel.Level = levelViewModel;

            if(ride.Trajet != null)
            {
                TrajetViewModel trajetViewModel = new TrajetViewModel();
                trajetViewModel.TrajetId = ride.Trajet.TrajetId;
                trajetViewModel.GpsPoints = ride.Trajet.GoogleCo.Split(';').ToList();
                rideViewModel.Trajet = trajetViewModel;
            }

            UserSimpleViewModel userSimpleViewModel = new UserSimpleViewModel();
            userSimpleViewModel.UserID = ride.User.UserID;
            userSimpleViewModel.Username = ride.User.Username;
            rideViewModel.Creator = userSimpleViewModel;



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

            if(comment.ChildComments != null)
            {
                foreach(Comment comment2 in comment.ChildComments)
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

        public bool AddUserToParticipants(int rideId, string username)
        {
            int result = -1;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                Ride ride = ctx.Rides.FirstOrDefault(x => x.RideId == rideId);
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                if (ride != null || user != null)
                {
                    if(!ride.Participants.Any(x => x == user))
                    {
                        ride.Participants.Add(user);
                        result = ctx.SaveChanges();

                        return result >= 1 ? true : false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RemoveUserToParticipants(int rideId, string username)
        {
            int result = -1;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                Ride ride = ctx.Rides.FirstOrDefault(x => x.RideId == rideId);
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                if (ride != null || user != null)
                {
                    if (ride.Participants.Any(x => x == user))
                    {
                        ride.Participants.Remove(user);
                        result = ctx.SaveChanges();

                        return result >= 1 ? true : false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public List<string> GetPartipants(int rideId)
        {
            List<string> participants = new List<string>();

            using (RiderQcContext ctx = new RiderQcContext())
            {
                if(!Exist(rideId))
                {
                    return null;
                }

                Ride ride = ctx.Rides
                    .Include(x => x.Participants)
                    .FirstOrDefault(y => y.RideId == rideId);

                if(ride != null)
                {
                    participants = ride.Participants.Select(x => x.Username).ToList();
                }
            }

            return participants;
        }

        public bool Update(Ride ride)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Ride _ride = ctx.Rides.FirstOrDefault(x => x.RideId == ride.RideId);
                _ride.DateDepart = ride.DateDepart;
                _ride.DateFin = ride.DateFin;
                _ride.Description = ride.Description;
                _ride.LevelId = ride.LevelId;
                _ride.TrajetId = ride.TrajetId;
                _ride.Title = ride.Title;
                result = ctx.SaveChanges();
            }

            return result >= 1 ? true : false;

        }
    }
}