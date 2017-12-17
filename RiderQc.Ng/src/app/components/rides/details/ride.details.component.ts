import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

//Models
import { CommentReply } from '../../../model/commentReply';
import { Comment } from '../../../model/comment';
import { Ride } from '../../../model/ride';
import { User } from '../../../model/user';

//Services
import { RideService } from '../../../services/ride.service';
import { CommentService } from "../../../services/comment.service";
import { UserService } from "../../../services/user.service";
import { TrajetService } from "../../../services/trajet.service";

@Component({
  selector: 'app-ride-details',
  templateUrl: './ride.details.component.html',
  styleUrls: ['./ride.details.component.css'],
  providers: [
      RideService,
      TrajetService
  ]
})

export class RideDetailsComponent implements OnInit {
  public ride: Ride;
  textValue: string;
  public user: User;
  public isLogged: Boolean;
  public Comment: Comment;
    sub: any;

    constructor (
      private route: ActivatedRoute,
      private router: Router,
      public rideService: RideService,
      public _commentService: CommentService,
      public _userService: UserService,
      public trajetService: TrajetService,
    )
    {
    }

    ngOnInit() {
      this.isLogged = this._userService.isLogged;

      if (this.isLogged) {
        this._userService.getLoggedUser().subscribe((x) => {
          this.user = x;
          this.loadRide()
        });
      }
      else {
        this.user = null;
      }
    }

    loadRide()
    {
      this.sub = this.route.params.subscribe(params => {
        let id = Number.parseInt(params['id']);
        this.rideService
          .details(id)
          .subscribe(
          p => {
            this.ride = p;
            console.log(this.ride);
          });
      });
    }
  
    attendRide(ride: Ride) {
      this.rideService.participate(ride.RideId).subscribe(() => {
        var ride: Ride = this.ride;
        ride.Participants.push(this.user.Username);

        this.ride = ride;
      });
    }

    cancelAttendRide(ride: Ride) {
      this.rideService.removeParticipate(ride.RideId).subscribe(() => {
        var ride: Ride = this.ride;

        const index: number = ride.Participants.indexOf(this.user.Username);

        if (index !== -1) {
          ride.Participants.splice(index, 1);
        }

        this.ride = ride;
      });
    }

    sendMessage(event) {
      var newComment = new CommentReply();
      newComment.CommentText = this.textValue;
      newComment.RideId = this.ride.RideId;

      if (newComment.CommentText != "") {

        var commentId: number;

        this._commentService.replyToRide(newComment).subscribe((x) => {
          commentId = x;

          var comment = new Comment();

          comment.CommentText = newComment.CommentText;
          comment.User = this.user;
          comment.TimeStamp = new Date();
          comment.RideId = newComment.RideId;
          comment.CommentId = commentId;

          var ride: Ride = this.ride;
          ride.Comments.push(comment);
          this.ride = ride;
          
          this.textValue = "";
        });
      }
    }

    getTimes = function (n) {
        return new Array(n);
    };
}
