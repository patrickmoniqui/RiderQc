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
    textValue: string;
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

  getData()
  {
    var commentId = this.Comment.CommentId;
    this.commentService.getById(commentId).subscribe(x => this.Comment = x);
  }

  sendMessage(event)
  {
    var newComment = new CommentReply();
      newComment.CommentText = this.textValue;
      newComment.ParentId = this.Comment.CommentId;

      if (newComment.CommentText != "") {
          //Send message to WebApi with service.
          console.log('sending comment ' + newComment.CommentText);
          var commentId: number;
          this.commentService.replyToComment(newComment).subscribe((x) => {
            commentId = x;
            this.textValue = "";
            this.getData();
          });
<<<<<<< HEAD
=======
      }
  }

  deleteMessage()
  {
    var result: Boolean;
    this.commentService.deleteById(this.Comment.CommentId).subscribe((x) => {
      result = x;
      if (result)
      {
        this.Comment = null;
        this.getData();
>>>>>>> 7ebd39bee64d905e0df40270f230ee620c4b9dee
      }
    });
  }

  deleteMessage()
  {
    var result: Boolean;
    this.commentService.deleteById(this.Comment.CommentId).subscribe((x) => {
      result = x;
      if (result)
      {
        this.Comment = null;
        this.getData();
      }
    });
  }
}
