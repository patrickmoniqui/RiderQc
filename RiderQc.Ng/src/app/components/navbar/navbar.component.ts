import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

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
      console.log("navbar component ngOnInit");
      this.isLogged = this.userService.isLogged;
      console.log("navbar:userlogged: " + this.isLogged);

        if (this.isLogged)
        {
            this.token = this.userService.getAuthCookie();
            this.userService.getUserByAuthToken(this.token).subscribe(x => this.user =  x);
        }
    }

    toggle(): void {
      var x = document.getElementById("myTopnav");
      if (x.className === "topnav") {
        x.className += " responsive";
      } else {
        x.className = "topnav";
      }
    }

    LoginToggle(): void {
      var element = document.getElementById("login-dp");
      var topnav = document.getElementById("myTopnav");

      console.log("login visible b4: " + element.style.display);

      if (element.style.display == "none")
      {
        element.style.display = "visible";
        topnav.style.overflow = "visible";
      }
      else
      {
        element.style.display = "none";
        topnav.style.overflow = "hidden";
      }

      console.log("login visible after: " + element.style.display);
    }
}
