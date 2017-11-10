using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Comment;
using RiderQc.Web.ViewModels.Level;
using RiderQc.Web.ViewModels.Moto;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.Helpers
{
    public static class EntityExtensions
    {
        public static RideViewModel ToViewModel(this Ride ride)
        {
            RideViewModel rideViewModel = new RideViewModel();
            rideViewModel.CreatorId = ride.CreatorId;
            rideViewModel.DateDepart = ride.DateDepart;
            rideViewModel.DateFin = ride.DateFin;
            rideViewModel.Description = ride.Description;
            rideViewModel.LevelId = ride.LevelId;
            rideViewModel.Title = ride.Title;
            rideViewModel.TrajetId = ride.TrajetId;

            rideViewModel.Comments = new List<CommentViewModel>();

            if(ride.Comments != null)
            {
                foreach (Comment comment in ride.Comments)
                {
                    rideViewModel.Comments.Add(comment.ToViewModel());
                }
            }
            
            rideViewModel.Level = ride.Level != null ? ride.Level.ToViewModel() : null;
            rideViewModel.User = ride.User != null ? ride.User.ToViewModel() : null;
            rideViewModel.Trajet = ride.Trajet != null ? ride.Trajet.ToViewModel() : null;

            return rideViewModel;
        }

        public static LevelViewModel ToViewModel(this Level level)
        {
            LevelViewModel levelViewModel = new LevelViewModel();
            levelViewModel.Id = level.LevelId;
            levelViewModel.Name = level.Name;

            return levelViewModel;
        }

        public static UserViewModel ToViewModel(this User user)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserID = user.UserID;
            userViewModel.Username = user.Username;
            userViewModel.Region = user.Region;
            userViewModel.DateOfBirth = user.DateOfBirth;
            userViewModel.Description = user.Description;
            userViewModel.DpUrl = user.DpUrl;
            userViewModel.Ville = user.Ville;

            userViewModel.Motos = new List<MotoViewModel>();
            userViewModel.Rides = new List<RideViewModel>();

            if(user.Motoes != null)
            {
                foreach (Moto moto in user.Motoes)
                {
                    userViewModel.Motos.Add(moto.ToViewModel());
                }
            }

            if(user.Rides != null)
            {
                foreach (Ride ride in user.Rides)
                {
                    userViewModel.Rides.Add(ride.ToViewModel());
                }
            }
            
            return userViewModel;
        }

        public static TrajetViewModel ToViewModel(this Trajet trajet)
        {
            TrajetViewModel trajetViewModel = new TrajetViewModel();
            trajetViewModel.TrajetId = trajet.TrajetId;
            trajetViewModel.GpsPoints = new List<string>();
            var gpsPoints = trajet.GoogleCo.Split(';');
            trajetViewModel.GpsPoints.AddRange(gpsPoints);
            return trajetViewModel;
        }

        public static CommentViewModel ToViewModel(this Comment comment)
        {
            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.CommentId = comment.CommentId;
            commentViewModel.ParentId = comment.ParentId;
            commentViewModel.SenderId = comment.SenderId;
            commentViewModel.RideId = comment.RideId;
            commentViewModel.TrajetId = comment.TrajetId;
            commentViewModel.CommentText = comment.CommentText;
            commentViewModel.Blocked = comment.Blocked;
            commentViewModel.Vote = comment.Vote;
            commentViewModel.TimeStamp = comment.TimeStamp;
            commentViewModel.ChildComments = new List<CommentViewModel>();

            var comments = comment.Comment1;

            foreach(Comment c in comments)
            {
                commentViewModel.ChildComments.Add(c.ToViewModel());
            }

            commentViewModel.User = comment.User.ToViewModel();

            return commentViewModel;
        }

        public static MotoViewModel ToViewModel(this Moto moto)
        {
            MotoViewModel motoViewModel = new MotoViewModel();
            motoViewModel.MotoId = moto.MotoId;
            motoViewModel.Brand = moto.Brand;
            motoViewModel.Model = moto.Model;
            motoViewModel.Year = moto.Year;
            motoViewModel.Type = moto.Type;

            return motoViewModel;
        }
    }
}