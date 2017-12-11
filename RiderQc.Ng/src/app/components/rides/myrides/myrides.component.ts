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

  public isLogged: Boolean = true;
  user: User;
  public rides: Ride[];
  isLoading: Boolean;
  
  constructor(public rideService: RideService, public commentService: CommentService, public userService: UserService) {
  }

  ngOnInit() {
    this.isLoading = true;

    this.user = new User();
    this.user.Username = "Loading user..";

    this.isLogged = this.userService.isLogged;

    if (this.isLogged) {
      this.userService.getLoggedUser().subscribe((user) => {
        this.user = user;
        this.GetMyRidesForLoggedUser();
        this.isLoading = false;
      });
    }
  }

  ngAfterViewInit() {

  }

  GetMyRidesForLoggedUser() {
    if (this.isLogged)
    {
      this.rideService.getMyRides(this.user.Username).subscribe((rides) => {
        this.rides = rides;
        console.log(this.rides);
      });
    }
  }
}
