import { Component, OnInit } from '@angular/core';

//Model
import { User } from '../../model/user';

//Service
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  providers:[UserService]
})
export class NavbarComponent implements OnInit {
    user: User;
    username: string;
    token: string;
    isLogged: Boolean;

    constructor(public userService: UserService) { }

    ngOnInit() {
        this.isLogged = this.userService.isLogged;
        console.log("navbar:userlogged: " + this.isLogged);

        if (this.isLogged)
        {
            this.token = this.userService.getAuthCookie();
            this.userService.getUserByAuthToken(this.token).subscribe(x => this.user =  x);
        }
  }
}
