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

@Component({
  selector: 'app-ride-details',
  templateUrl: './ride.details.component.html',
  styleUrls: ['./ride.details.component.css'],
  providers:[RideService]
})
export class RideDetailsComponent implements OnInit {
  public ride: Ride;
  textValue: any;
  public user: User;
  public isLogged: Boolean;
  public Comment: Comment;
    sub: any;

    constructor (
      private route: ActivatedRoute,
      private router: Router,
      public rideService: RideService,
      public _commentService: CommentService,
      public _userService: UserService
    )
    {
      this.isLogged = _userService.isLogged;

      if (this.isLogged) {
        this._userService.getLoggedUser().subscribe(x => this.user = x);
      }
      else {
        this.user = null;
      }
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            console.log('getting ride with id: ', id);
            this.rideService
                .details(id)
                .subscribe(
                p => {
                    this.ride = p;
                });
        });
    }


    attendRide(ride: Ride) {
      this.rideService.participate(ride.RideId).subscribe();
      this.refreshRide()
    }

    cancelAttendRide(ride: Ride) {
      this.rideService.removeParticipate(ride.RideId).subscribe();
      this.refreshRide()
    }

    sendMessage(event) {
      var newComment = new CommentReply();
      newComment.CommentText = this.textValue;
      newComment.RideId = this.ride.RideId;

      if (newComment.CommentText != "") {
        //Send message to WebApi with service.
        console.log('sending comment ' + newComment.CommentText);
        var commentId: number;

        this._commentService.replyToRide(newComment).subscribe(x => commentId = x);

        if (commentId != null) {
          var comment: Comment;
          comment.CommentId = commentId;
          comment.RideId = newComment.RideId;
          comment.CommentText = newComment.CommentText;

          this.ride.Comments.push(comment);
        }
      }
    }

    refreshRide() {

    }

    getTimes = function (n) {
        return new Array(n);
    };

}
