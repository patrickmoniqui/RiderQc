import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-logoff',
  templateUrl: './logoff.component.html',
  styleUrls: ['./logoff.component.css']
})
export class LogoffComponent implements OnInit {

    constructor(public userService: UserService) {
        this.userService.removeAuthCookie();
    }

    ngOnInit() {
        window.location.href = "/";
    }
}