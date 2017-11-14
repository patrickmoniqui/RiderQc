import { Component, OnInit } from '@angular/core';

//Model
import { User } from '../../../model/user';

//Service
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-navbar-login',
  templateUrl: './navbar-login.component.html',
  styleUrls: ['./navbar-login.component.css']
})

export class NavbarLoginComponent implements OnInit {
  public isLogged: Boolean;
  user: User;
  token: string;
  err: string;

  constructor(public userService: UserService) {
    console.log("navbar login::constructor");
    this.isLogged = this.userService.isLogged;

    if (this.isLogged) {
      this.token = this.userService.getAuthCookie();
      this.userService.getUserByAuthToken(this.token).subscribe(x => this.user = x);
      console.log("navbar user: " + this.user);
    }
  }

  ngOnInit() {
    console.log("navbar login::ngOnInit");
    if (this.user == null)
    {
      this.user = new User();
    }
  }

  login(isSocial: boolean): void {
    console.log("Logging in..");
    this.userService.Login(this.user.Username, this.user.Password).subscribe(
      (token) => {
        window.location.href = "/";
      },
      (err) => {
        if (isSocial) {
          this.userService.register({
            "Username": this.user.Username,
            "Password": this.user.Password
          }).subscribe(
            (response) => {
              this.login(false);
            },
            (err) => {
              //Here you can catch the error
              //this.errorlbl = err;
              alert(err)
            }
            );
        } else {
          if (err.status == 401) {
            this.err = "Username or Password is incorrect";
          }
          if (err.status == 500) {
            this.err = "Fatal Error";
          }
        }


      }
    );
  }

}
