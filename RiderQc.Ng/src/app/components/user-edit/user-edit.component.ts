import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FormControl } from '@angular/forms';
import { FormGroup, Validators } from '@angular/forms';

//Model
import { User } from '../../model/user';

//Services
import { UserService } from '../../services/user.service';



@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
  providers:[UserService],
})
export class UserEditComponent implements OnInit {
    
    private errorlbl: string;
    currdate: any = "";
    private user: User;
    private user1: User;
    private showPass: boolean;
    private pass: string;
    private confirmPass: string;

    user: User;
    showPass: boolean;
    InfoGroup: FormGroup;
    pass: string = "hello";
>>>>>>> develop
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
    ChangePass()
    {
        console.log(this.pass);
        if (this.pass.length < 6)
        {
            this.errorlbl = "This password is not long enough";
        }
        else
        {
            if (this.pass !== this.confirmPass) {
                this.errorlbl = "The Passwords don't match";
            }
            else
            {
                this.userService.ChangePass(this.pass).subscribe();
            }
        }
        
            
        
    }
    hidePass()
    {
        this.showPass = false;
        
    }
    clearError()
    {
        this.errorlbl = "";
    }
}
