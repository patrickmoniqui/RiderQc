import { Component, OnInit } from '@angular/core';

//Model
import { User } from '../../../model/user';
import { SocialUser } from "angular4-social-login";

//Service
import { UserService } from '../../../services/user.service';
import { AuthService } from "angular4-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angular4-social-login";

@Component({
  selector: 'app-navbar-login',
  templateUrl: './navbar-login.component.html',
  styleUrls: ['./navbar-login.component.css'],
  providers: [AuthService, UserService]
})

export class NavbarLoginComponent implements OnInit {
  public isLogged: Boolean;
  public socialUser: SocialUser;

  user: User;
  token: string;
  err: string;


  constructor(private authService: AuthService, public userService: UserService) {

    this.isLogged = this.userService.isLogged;

    if (this.isLogged) {
      this.userService.getLoggedUser().subscribe(x => this.user = x);
    }
  }

  ngOnInit() {
    if (this.user == null)
    {
      this.user = new User();
    }

    this.authService.authState.subscribe((socialUser) => {
      this.socialUser = socialUser;
    });
  }

  signInWithGoogle(): void {
    var googlePromise = this.authService.signIn(GoogleLoginProvider.PROVIDER_ID);
    googlePromise.then((result) => {
      this.convertUser();
      this.login(true);
    });
  }

  signInWithFB(): void {
    var facebookPromise = this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
    facebookPromise.then((result) => {
      this.convertUser();
      this.login(true);
    });
  }

  convertUser(): void {
    this.user.Username = this.socialUser.email;
    this.user.Password = this.socialUser.id;
  }

  login(isSocial: boolean): void {
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
