import { Component, OnInit } from '@angular/core';

//Model
import { User } from '../../model/user';

//Service
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  providers:[UserService]
})
export class NavbarComponent implements OnInit {
    user: User;
    username: string;
    token: string;

    constructor(private userService: UserService) { }

    ngOnInit() {
        this.username = localStorage.getItem("username");
        this.token = localStorage.getItem("token");
        this.userService.getUser(this.username).subscribe(
            user => {
                this.user = user
            }
        );
  }

}
