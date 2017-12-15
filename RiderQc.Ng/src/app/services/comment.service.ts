import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../../environments/environment';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

//Models
import { CommentReply } from '../model/commentReply';
import { Comment } from '../model/comment';

//Service
import { UserService } from '../services/user.service';

@Injectable()
export class CommentService {

  baseUrl: string = environment.BaseUrl;

    constructor(public http: Http, public userService: UserService) {

    }

    replyToComment(comment: CommentReply): Observable<number> {
      var commentId = this.http.post(this.baseUrl + '/comment/reply', comment, { headers: this.userService.getBearerAuthHeader() })
        .map(x => x.json())
        return commentId;
    }

    replyToRide(comment: CommentReply): Observable<number> {
      var commentId = this.http.post(this.baseUrl + '/comment/reply', comment, { headers: this.userService.getBearerAuthHeader() })
        .map(x => x.json());
      return commentId;
    }

    getById(commentId: number): Observable<Comment> {
      var comment = this.http.get(this.baseUrl + '/comment/' + commentId, { headers: this.userService.getBearerAuthHeader() })
        .map(x => x.json());
      return comment;
    }

    deleteById(commentId: number): Observable<Boolean> {
      var result = this.http.delete(this.baseUrl + '/comment/' + commentId, { headers: this.userService.getBearerAuthHeader() })
        .map(x => x.json());
      return result;
    }
}
