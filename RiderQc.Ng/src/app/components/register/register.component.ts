import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormControl, FormBuilder, Validators, ReactiveFormsModule  } from '@angular/forms';
import { RideService } from '../../services/ride.service';
import { UserService } from '../../services/user.service';
import { RouterModule, Routes, Router } from '@angular/router';
import { EmailValidator } from '@angular/forms';
import { CustomValidation } from './customValidation';


@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
	  styleUrls: ['./register.component.css'],
	  providers: [UserService, FormBuilder, RideService]
})
/** register component*/
export class RegisterComponent implements OnInit
{
    registerForm: FormGroup;

    currDate: any = "";
    errorlbl: any = "";
    response: any = {};

    user: any = {
        "Username": "",
        "Password": "",
        "Region": "",
        "Ville": "",
        "DateOfBirth": "2050-01-01T00:00:00.000Z",
        "Description": "",
        "DpUrl": ""
    };
    regions = [
        "Bas-Saint-Laurent", "Saguenay–Lac-Saint-Jean", "Capitale-Nationale", "Mauricie", "Estrie", "Montréal",
        "Outaouais", "Abitibi-Témiscamingue", "Côte-Nord", "Nord-du-Québec", "Gaspésie–Îles-de-la-Madeleine",
        "Chaudière-Appalaches", "Laval", "Lanaudière", "Laurentides", "Montérégie", "Centre-du-Québec"
    ];
    
    /** register constructor` */
    constructor(private riderQcService: RideService,
        private userService: UserService,
        private fb: FormBuilder,
        private router: Router)
    {
        this.createForm();
    }

    /** Called by Angular after register component initialized */
    ngOnInit(): void {
        this.createForm();
    }

    createForm() {
        this.registerForm = this.fb.group({
            'name': new FormControl(this.user.Name, [
                Validators.required,
                Validators.email,
                Validators.minLength(4),
                Validators.maxLength(37)
            ]),
            'password': new FormControl(this.user.Password, [
                Validators.required,
                Validators.minLength(6),
                Validators.maxLength(75)
            ]),
            'passwordConf': new FormControl(this.user.PasswordConf, [
                Validators.required,
                Validators.minLength(6)
            ]),
            'ville': new FormControl(this.user.Ville, [
                Validators.minLength(2)
            ]),
            'region': new FormControl(this.user.Password, [Validators.required]),
            'dateOfBirth': new FormControl(this.user.DateOfBirth, [Validators.required]),
            'description': new FormControl(this.user.Description)
        }, {
            validators: [CustomValidation.MatchPassword, CustomValidation.CheckDateFormat(this.currDate)]
        });
    }

    lookIfValid() {
        return this.registerForm.valid
    }

    register() {
        var registerResponse;
        this.errorlbl = "";
        
        this.userService.register({
            "Username": this.registerForm.value.name,
            "Password": this.registerForm.value.password,
            "Region": this.registerForm.value.region,
            "Ville": this.registerForm.value.ville,
            "DateOfBirth": this.registerForm.value.dateOfBirth + "T00:00:00.000Z",
            "Description": this.registerForm.value.description
        }).subscribe(
            (response) => {
                //Here you can map the response to a type.
                alert(response);
                this.router.navigate(['login']); 
            },
            (err) => {
                //Here you can catch the error
                this.errorlbl = err;
            }
        );

    }
}
