import { Component, Input, OnInit } from '@angular/core';

//Models
import { CommentReply } from '../../model/commentReply';
import { Comment } from '../../model/comment';

//Services
import { CommentService } from '../../services/comment.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {

    @Input() Comment: Comment;
    textValue: any;
    public commentService: CommentService;
    public userService: UserService;
    public isLogged: Boolean;


    constructor(public _commentService: CommentService, public _userService: UserService) {
        this.commentService = _commentService;
        this.userService = _userService;
        this.isLogged = this.userService.isLogged;
    }

  ngOnInit() {

  }

  sendMessage(event, text)
  {
      var newComment = new CommentReply();
      newComment.CommentText = text;
      newComment.ParentId = this.Comment.CommentId;

      if (text != "") {
          //Send message to WebApi with service.
          console.log('sending comment ' + newComment.CommentText);
          var commentId = this.commentService.replyToComment(newComment);
      }
  }
}