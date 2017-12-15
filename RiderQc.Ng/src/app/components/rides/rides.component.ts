import { Component, OnInit } from '@angular/core';

//Models
import { CommentReply } from '../../model/commentReply';
import { Comment } from '../../model/comment';
import { User } from '../../model/user';
import { Ride } from '../../model/ride';

//Services
import { CommentService } from '../../services/comment.service';
import { UserService } from '../../services/user.service';
import { RideService } from '../../services/ride.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css'],
  providers:[RideService]
})

export class RidesComponent implements OnInit {
  textValue: any;
  public commentService: CommentService;
  public userService: UserService;
  public isLogged: Boolean;
  public user: User;
  public rides: Ride[] = [];

  constructor(public rideService: RideService, public _commentService: CommentService, public _userService: UserService) {
    this.commentService = _commentService;
    this.userService = _userService;
    this.isLogged = this.userService.isLogged;

    if (this.isLogged)
    {
      this.userService.getLoggedUser().subscribe(x => this.user = x);
    }
    else
    {
      this.user = null;
    }
  }

    ngOnInit() {
      this.fetchAllRide();
    }
    
    toggleRideContainer(element) {

    }

    fetchAllRide()
    {
        this.rideService.getRides().subscribe((rides) => {
            this.rides = rides;
        });
    }

    searchBar_OnChange(element)
    {
        var value = element.srcElement.value;

        if (value != "")
        {
            var tmpRides: Ride[] = new Array();

            for (var i = 0, len = this.rides.length; i < len; i++) {
                if (this.rides[i].Title.includes(value)) {
                    tmpRides.push(this.rides[i]);
                }
            }

            this.rides = tmpRides;

        }
        else
        {
            this.fetchAllRide();
        }
    }

    getTimes = function (n) {
        return new Array(n);
    };
}
