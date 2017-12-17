import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FormControl } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { FormGroup, Validators } from '@angular/forms';

//Model
import { User } from '../../model/user';
import { UserEdit } from '../../model/user-edit';
import { Moto } from '../../model/moto';

//Services
import { UserService } from '../../services/user.service';
import { MotoService } from '../../services/moto.service';



@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css'],
  providers:[UserService,MotoService],
})
export class UserEditComponent implements OnInit {
    errorlbl: string;
    currdate: any = "";
    dateOfBirth: string;
    user: User;
    user1: User;
    showPass: boolean;
    pass: string;
    confirmPass: string;
    hasVilleChanged: boolean = false;
    hasRegionChanged: boolean = false;
    hasDescriptionChanged: boolean = false;
    body: string = "";
    moto:Moto= new Moto();
    
    regions = [
        "Bas-Saint-Laurent", "Saguenay–Lac-Saint-Jean", "Capitale-Nationale", "Mauricie", "Estrie", "Montréal",
        "Outaouais", "Abitibi-Témiscamingue", "Côte-Nord", "Nord-du-Québec", "Gaspésie–Îles-de-la-Madeleine",
        "Chaudière-Appalaches", "Laval", "Lanaudière", "Laurentides", "Montérégie", "Centre-du-Québec"
    ];

    constructor(private userService: UserService, private motoService: MotoService) { }

    ngOnInit() {
   
      this.userService.getLoggedUser().subscribe((user) => {
        this.user = user;
      });
        
      this.showPass = false;
      this.moto.Brand = "";
      this.moto.Model = "";
      this.moto.Year = 2010;
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
                this.errorlbl = "Password Changed";
            }
        } 
    }

    UpdateUser()
    {
      var userEdit: UserEdit = new UserEdit();
      userEdit.Region = this.user.Region;
      userEdit.Ville = this.user.Ville;
      userEdit.DateOfBirth = this.user.DateOfBirth;
      userEdit.Description = this.user.Description;
      userEdit.DpUrl = this.user.DpUrl;
      
      console.log(this.user.DateOfBirth);
      this.userService.editUser(userEdit).subscribe((x) => {

        if (x) {
          alert("User successfully saved.");
        }
        else {
          alert("An error ocured while saving user.");
        }
      });
    }

    CreateMoto()
    {
        this.motoService.createMoto({
            "Brand": this.moto.Brand,
            "Model": this.moto.Model,
            "Year": this.moto.Year,
            "Type": 4,
            "UserId": this.user.UserID
        }
        , this.userService.getAuthCookie()).subscribe();
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
