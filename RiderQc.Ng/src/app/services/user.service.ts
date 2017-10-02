import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';

//Import Model
import { User } from '../model/user';


@Injectable()
export class UserService {

	baseUrl: string = "http://riderqc-api.azurewebsites.net";
	constructor(public http: Http) { }

	getUsers(): Observable<User[]>{
		return this.http.get(`${this.baseUrl}/user/list`, { headers: this.getHeaders()})
		  .map(res => res.json());
	}

	register(jsonUser) {
		return this.http.post(`${this.baseUrl}/user/register`, JSON.stringify(jsonUser));
	}

  private getHeaders() {
	  let headers = new Headers();
	  headers.append('Accept', 'application/json');
	  return headers;
	}

  private getHeadersAUTH(userBtoa:string) {
	  let headers = new Headers();
	  headers.append("Authorization", userBtoa);
	  return headers;
  }

  
}