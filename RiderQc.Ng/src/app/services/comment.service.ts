import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../../environments/environment';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

//Import Model
import { CommentReply } from '../model/commentReply';

@Injectable()
export class CommentService {

  baseUrl: string = environment.BaseUrl;

    constructor(public http: Http) {

    }

    replyToComment(comment: CommentReply) {
        var commentId = this.http.post('${this.baseUrl}/comment/reply', comment);
        return commentId;
    }

    replyToRide(comment: CommentReply) {
      var commentId = this.http.post('${this.baseUrl}/comment/replytoride', comment);
      return commentId;
    }
}
