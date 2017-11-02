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

@Injectable()
export class UserService {
    public isLogged: Boolean;

    baseUrl: string = environment.BaseUrl;
    token = "";

    constructor(public http: Http) {
        this.token = this.getAuthCookie();
        console.log("auth_token: " + this.token);

        this.isLogged = this.token != null && this.token != "" ? true : false;

        console.log("isLogged: " + this.isLogged);
    }

    //Web Services


    getUser(username: string): Observable<User> {
        return this.http.get(`${this.baseUrl}/user?username=${username}`)
            .map(res => res.json());

    }

    getUserByAuthToken(authToken: string): Observable<User> {
        return this.http.get(this.baseUrl + '/user/bytoken?auth_token=' + authToken, { headers: this.getBearerAuthHeader() })
            .map(res => res.json());
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
                    console.log("token for " + username + " is: " + token);
                }
            });
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
        localStorage.removeItem("username");
        localStorage.removeItem("token");
    }


    //Fonctions autres

    private getHeaders() {
        let headers = new Headers();
        headers.append('Accept', 'application/json');
        return headers;
    }

    private getBearerAuthHeader()
    {
        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + this.token);
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
    }

    public getAuthCookie()
    {
        return localStorage.getItem("auth_token");
    }
}
