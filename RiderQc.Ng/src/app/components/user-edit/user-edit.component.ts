import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { FormControl } from '@angular/forms';
import { FormGroup, Validators } from '@angular/forms';

//Model
import { User } from '../../model/user';
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
   
        this.userService.getLoggedUser().subscribe(
            user => this.user = user);
        console.log(this.user);
        this.showPass = false;
              
        var date = new Date(this.user.DateOfBirth);
        var day = date.getDay();
        var month = date.getMonth();
        var year = date.getFullYear();
        this.currdate = year + "-" + month + "-" + day;
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
    addValue(body:string)
    {
        if (body == "Ville")
        {
            this.hasVilleChanged = true;
        }
        if (body == "Region") {
            this.hasRegionChanged = true;
        }
        if (body == "Description") {
            this.hasDescriptionChanged = true;
        }
        
    }
    UpdateUser()
    {
        
        console.log("R" + this.user.Region + "V" + this.user.Ville + "D" + this.user.Description);

        if (this.hasDescriptionChanged)
        {
            this.body = this.body.concat(this.body + "{Description:" + this.user.Description + "},");
        }
        if (this.hasRegionChanged)
        {
            this.body = this.body + "{Region:" + this.user.Region + "},";
        }
        if (this.hasVilleChanged) {
            this.body = this.body + "Ville:" + this.user.Ville + "},";
        }
        console.log(this.body);
        this.userService.UpdateUser(this.body).subscribe();
        

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
            
       /* {
            "Username": this.registerForm.value.name,
            "Password": this.registerForm.value.password,
            "Region": this.registerForm.value.region,
            "Ville": this.registerForm.value.ville,
            "DateOfBirth": this.registerForm.value.dateOfBirth + "T00:00:00.000Z",
            "Description": this.registerForm.value.description
        }*/
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
