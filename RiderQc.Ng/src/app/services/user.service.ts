import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from '../../environments/environment';

//Models
import { Authentification } from '../model/Authentification';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


//Import Model
import { User } from '../model/user';
import { UserEdit } from '../model/user-edit';

@Injectable()
export class UserService {
    public isLogged: Boolean;

    baseUrl: string = environment.BaseUrl;
    token = "";

    constructor(public http: Http) {
        this.token = this.getAuthCookie();
        this.isLogged = this.token != null && this.token != "" ? true : false;
    }

    //Web Services

    getMyRides(username: string) {
      return this.http.get(`${this.baseUrl}/user/myrides?username=` + username)
        .map(res => res.json());
    }

    getUser(username: string): Observable<User> {
        return this.http.get(`${this.baseUrl}/user?username=${username}`)
          .map((response: Response) => {
            if (response.status == 200) {
              return response.json();
            }
            else if (response.status == 401) {
              this.removeAuthCookie();
            }
            else
            {
              return null;
            }
          });
    }

    getUserByAuthToken(authToken: string): Observable<User> {
      return this.http.get(this.baseUrl + '/user/bytoken', { headers: this.getBearerAuthHeader() })
        .map((response: Response) => {
          if (response.status == 200) {
            return response.json();
          }
          else if (response.status == 401) {
            this.removeAuthCookie();
          }
        });
    }

    getLoggedUser(): Observable<User> {
      return this.http.get(this.baseUrl + '/user/bytoken', { headers: this.getBearerAuthHeader() })
        .map((response: Response) => {
          if (response.status == 200) {
            return response.json();
          }
          else if (response.status == 401) {
            this.removeAuthCookie();
          }
        });
    }
  
    getUsers(): Observable<User[]> {
        return this.http.get(`${this.baseUrl}/user/list`, { headers: this.getHeaders() })
            .map(res => res.json());
    }

    Login(username: string, password: string) {
        return this.http.get(`${this.baseUrl}/login`, { headers: this.getHeadersAUTH("Basic " + btoa(username + ":" + password)) })
            .map((response: Response) => {
                if (response.status == 200) {
                    var token: Authentification = response.json();
                    this.setAuthCookie(token.Token);
                }
              else if (response.status == 401)
                {
                  this.removeAuthCookie();
                }
            });
    }

    ChangePass(password: string) {
        let options = new RequestOptions({ headers: this.getBearerAuthHeader() });
        let body = JSON.stringify("{}");
        return this.http.put(this.baseUrl + "/user/password?pwd=" + password, body, options)
            .map((response: Response) => {
                console.log(response);
                return response;
            }).catch(this.handleError);

    }

    editUser(user: UserEdit) : Observable<boolean>
    {
      let header = new RequestOptions({ headers: this.getBearerAuthHeader() });

      return this.http.put(this.baseUrl + "/user", user, header)
        .map(res => res.json());
    }

    UpdateUser(jsonUser)
    {
        let body = JSON.stringify(jsonUser);
        console.log(body);
        let options = new RequestOptions({ headers: this.getBearerAuthHeader() });
        return this.http.put(this.baseUrl + "/user", body, options)
            .map((response: Response) => {
                console.log(response);
            return response;
        }).catch(this.handleError);
    }


    register(jsonUser) {
        let body = JSON.stringify(jsonUser);
        let headers = new Headers({
            'Content-Type': 'application/json',
            'username': btoa(jsonUser.Username),
            'password': jsonUser.Password
        });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(`${this.baseUrl}/user/register`, body, options)
            .map((response: Response) => {
                return response;
            }).catch(this.handleError);

    }

    logoff() {
      this.removeAuthCookie();
    }


    //Fonctions autres

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    public getBearerAuthHeader()
    {
        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + this.token);
        headers.append('Accept', 'application/json');
        headers.append('Content-Type', 'application/json');
        return headers;
    }


    private handleError(error: Response) {
        console.error(error.json());
        return Observable.throw(error.json().Message || 'Server error');
    }

    private getHeadersAUTH(userBtoa: string) {
        let headers = new Headers();
        headers.append("Authorization", userBtoa);
        return headers;
    }

    public setAuthCookie(token: string) {
        localStorage.setItem("auth_token", token);
    }

    public removeAuthCookie() {
      localStorage.removeItem("auth_token");
      this.isLogged = false;
      console.log("removing cookie");
    }

    public getAuthCookie()
    {
        return localStorage.getItem("auth_token");
    }
}
