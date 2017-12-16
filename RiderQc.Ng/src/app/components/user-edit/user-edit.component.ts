import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Observable";

//Model
import { User } from '../../model/user';

//Services
import { UserService } from '../../services/user.service';
import { FormGroup } from "@angular/forms/forms";


@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
  providers:[UserService],
})
export class UserEditComponent implements OnInit {

    currdate: any = "";
    user: User;
    showPass: boolean;
    InfoGroup: FormGroup;
    pass: string = "hello";
    regions = [
        "Bas-Saint-Laurent", "Saguenay–Lac-Saint-Jean", "Capitale-Nationale", "Mauricie", "Estrie", "Montréal",
        "Outaouais", "Abitibi-Témiscamingue", "Côte-Nord", "Nord-du-Québec", "Gaspésie–Îles-de-la-Madeleine",
        "Chaudière-Appalaches", "Laval", "Lanaudière", "Laurentides", "Montérégie", "Centre-du-Québec"
    ];
    constructor(private userService: UserService) { }

    ngOnInit() {
        this.userService.getLoggedUser().subscribe(
            user => this.user = user);
        console.log(this.user);
        this.showPass = false;
        /*this.InfoGroup = new FormGroup({

        });*/
        
        var date = new Date(this.user.DateOfBirth);
        var day = date.getDay();
        var month = date.getMonth();
        var year = date.getFullYear();
        this.currdate = year + "-" + month + "-" + day;
    }
    ShowPass():void 
    {
        this.showPass = true;
    }
    ChangePass(pass)
    {
        console.log(pass);
    }
    hidePass()
    {
        this.showPass = false;
        
    }
}
