import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { UserService } from '../services/user.service';
import { environment } from '../../environments/environment';

@Injectable()
export class MessageService {
  baseUrl: string = environment.BaseUrl;

  constructor(public http: Http, public userService: UserService) { }

  getMessages() {
    return this.http.get(`${this.baseUrl}/message/conversation`)
      .map(res => res.json());
  }

  sendMessage(jsonMessage) {
    let body = JSON.stringify(jsonMessage);
    let options = new RequestOptions({ headers: this.getSpecialHeaders() });
    return this.http.post(`${this.baseUrl}/message/send`, body, options)
      .map((response: Response) => {
        return response;
      }).catch(this.handleError);

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
