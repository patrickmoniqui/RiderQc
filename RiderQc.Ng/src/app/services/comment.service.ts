import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

//Import Model
import { CommentReply } from '../model/commentReply';

@Injectable()
export class CommentService {

    //baseUrl: string = "http://riderqc-api.azurewebsites.net"; //dev
    baseUrl: string = "http://localhost:50800/"; //local

    constructor(public http: Http) {

    }

    replyToComment(comment: CommentReply) {
        var commentId = this.http.post('${this.baseUrl}/comment/reply', comment);
        return commentId;
    }
}
