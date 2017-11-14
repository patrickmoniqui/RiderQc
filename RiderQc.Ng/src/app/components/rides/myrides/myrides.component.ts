import { Component, OnInit, Input } from '@angular/core';

//Models
import { CommentReply } from '../../../model/commentReply';
import { Comment } from '../../../model/comment';
import { User } from '../../../model/user';
import { Ride } from '../../../model/ride';

//Services
import { CommentService } from '../../../services/comment.service';
import { UserService } from '../../../services/user.service';
import { RideService } from '../../../services/ride.service';

@Component({
  selector: 'app-myrides',
  templateUrl: './myrides.component.html',
  styleUrls: ['./myrides.component.css']
})
export class MyridesComponent implements OnInit {

  public isLogged: Boolean;
  user: User;
  public rides: Ride[];

  public commentService: CommentService;
  public userService: UserService;

  constructor(public rideService: RideService, public _commentService: CommentService, public _userService: UserService) {
    this.commentService = _commentService;
    this.userService = _userService;
    this.isLogged = this.userService.isLogged;

    if (this.isLogged) {
      var token = this.userService.getAuthCookie();
      this.userService.getUserByAuthToken(token).subscribe(x => this.user = x);
      console.log("user: " + this.user);

    }
    else {
      this.user = null;
    }
  }

  ngOnInit() {
    this.GetMyRidesForLoggedUser();
  }

  GetMyRidesForLoggedUser() {
    if (this.isLogged)
    {
      let username = this.user.Username;
      this.userService.getMyRides(username).subscribe((rides) => {
        this.rides = rides;
      });
      console.log(this.rides);
    }
  }
}
