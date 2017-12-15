import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { UserService } from '../services/user.service';
import { environment } from '../../environments/environment';

@Injectable()
export class MessagerieService {

  baseUrl: string = environment.BaseUrl;

  constructor(public http: Http, public userService: UserService) { }

  getMessages() {
    return this.http.get(`${this.baseUrl}/message/conversation`)
      .map(res => res.json());
  }

  getInbox() {
    let options = new RequestOptions({ headers: this.getSpecialHeaders() });
    return this.http.get(`${this.baseUrl}/message/inbox`, options)
      .map(res => res.json());
  }

  getOutbox() {
    let options = new RequestOptions({ headers: this.getSpecialHeaders() });
    return this.http.get(`${this.baseUrl}/message/outbox`, options)
      .map(res => res.json());
  }

  sendMessage(jsonMessage) {
    //let body = JSON.stringify(jsonMessage);
    let options = new RequestOptions({ headers: this.getSpecialHeaders() });
    options.body = jsonMessage;
    console.log(JSON.stringify(jsonMessage) + JSON.stringify(options));
    return this.http.post(`${this.baseUrl}/message/send`, jsonMessage, options)
      .subscribe(
      res => {
        console.log("gucci");
        return res;
      },
      err => {
        console.log("fail xd");
      }
      );
  }

  private getHeaders() {
    let headers = new Headers();
    headers.append('Accept', 'application/json');
    return headers;
  }

  private getSpecialHeaders() {
    let headers = this.userService.getBearerAuthHeader();
    headers.append('Accept', 'application/json');
    return headers;
  }

  private handleError(error: Response) {
    console.error(error.json());
    return Observable.throw(error.json().Message || 'Server error');
  }

}
