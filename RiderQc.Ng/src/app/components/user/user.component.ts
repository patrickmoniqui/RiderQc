import { Component, OnInit } from '@angular/core';
//service
import { UserService } from '../../services/user.service';
//class
import { User } from '../../model/user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  providers: [UserService]
  
})

export class UserComponent implements OnInit {

	userList: User[];

	constructor(private userservice: UserService)
	{
		    
    }

	ngOnInit() {
		this.userservice.getUsers().subscribe(
			u => {
				this.userList = u;
				console.log(u);
			});

    }
}
