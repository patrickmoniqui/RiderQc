import { Component, OnInit } from '@angular/core';

import { AuthService } from "angular4-social-login";
import { UserService } from '../../services/user.service';
import { FacebookLoginProvider, GoogleLoginProvider } from "angular4-social-login";

import { SocialUser } from "angular4-social-login";
import { User } from '../../model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [AuthService, UserService]
})


export class LoginComponent implements OnInit {

  public socialUser: SocialUser;
  public loggedIn: boolean;

  user: User;
  token: string;
  err: string;

  constructor(private authService: AuthService, public userService: UserService) { }

  ngOnInit() {
    this.user = new User();
    this.authService.authState.subscribe((socialUser) => {
      this.socialUser = socialUser;
      this.loggedIn = (socialUser != null);
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

  signOut(): void {
    this.authService.signOut();
  }

  convertUser(): void {
    this.user.Username = this.socialUser.email;
    this.user.Password = this.socialUser.id;
  }

  /*Comment Utiliser le login :
      Commencer par valider les cookies :
       nous avons 2 cookies :
        -"token" sert pour mettre dans le header avec le tag Authorization
        -"username" sert pour récupérer les donneés autres du User via la fonction getUser(username:string) du UserService
     1- Ajouter un user dans votre composant
     2- aller récupérer ses données via UserService
     3- récupérer le token dans le cookie


    */

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
                }else{
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


    //fonction pour Onchange sur le champs de message d'erreur'
    change(): void {
        this.err = "";
    }
}
