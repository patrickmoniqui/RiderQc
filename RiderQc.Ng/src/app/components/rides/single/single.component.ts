import { Component, OnInit, Input } from '@angular/core';
import { OnClickEvent, OnRatingChangeEven, OnHoverRatingChangeEvent } from 'angular-star-rating/star-rating-struct'


//Models
import { CommentReply } from '../../../model/commentReply';
import { Comment } from '../../../model/comment';
import { Ride } from '../../../model/ride';
import { User } from '../../../model/user';

//Services
import { CommentService } from '../../../services/comment.service';
import { UserService } from '../../../services/user.service';
import { RideService } from '../../../services/ride.service';

@Component({
  selector: 'app-ride-single',
  templateUrl: './single.component.html',
  styleUrls: ['./single.component.css']
})

export class SingleComponent implements OnInit {

  @Input() ride;
  textValue: any;
  public user: User;
  public isLogged: Boolean;
  public Comment: Comment;

  constructor(public rideService: RideService, public _commentService: CommentService, public _userService: UserService) {
    this.isLogged = _userService.isLogged;

    if (this.isLogged) {
      this._userService.getLoggedUser().subscribe(x => this.user = x);
    }
    else {
      this.user = null;
    }
  }

  ngOnInit() {

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
      //Send message to WebApi with service.
      console.log('sending comment ' + newComment.CommentText);
      var commentId: number;

      this._commentService.replyToRide(newComment).subscribe(x => commentId = x);

      if (commentId != null)
      {
        var comment: Comment;
        comment.CommentId = commentId;
        comment.RideId = newComment.RideId;
        comment.CommentText = newComment.CommentText;

        this.ride.Comments.push(comment);
      }
    }
  }

  getTimes = function (n) {
    return new Array(n);
  };

  refreshRide()
  {
    this.ride = this.rideService.getRideById(this.ride.RideId);
  }

}
