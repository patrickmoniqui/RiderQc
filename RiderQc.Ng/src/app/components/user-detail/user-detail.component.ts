import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from "rxjs/Subscription";

//Model
import { User } from '../../model/user';
//Service
import { UserService } from '../../services/user.service';



@Component({
    selector: 'app-user-detail',
    templateUrl: './user-detail.component.html',
    styleUrls: ['./user-detail.component.css'],
    providers: [UserService],
})
export class UserDetailComponent implements OnInit {
    sub: Subscription;
    formatedDate: Date;
    displayDate:string
    username: string;
    user: User;
    constructor(
        private userService: UserService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
         this.sub = this.route
            .queryParams
            .subscribe(params => {
                // Defaults to 0 if no query param provided.
                this.username = params['username'];
            });
        this.userService.getUser(this.username).subscribe(
            user => {
                this.user = user
                this.formatedDate = new Date(this.user.DateOfBirth);
                this.displayDate = this.formatedDate.getDay() + "/" + this.formatedDate.getMonth() + "/" + this.formatedDate.getFullYear();
            }
        );


        


  }

}
