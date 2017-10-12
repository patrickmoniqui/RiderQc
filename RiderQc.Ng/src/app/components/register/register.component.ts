import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormControl, FormBuilder, Validators, ReactiveFormsModule  } from '@angular/forms';
import { RiderqcService } from '../../services/riderqc.service';
import { UserService } from '../../services/user.service';
import { RouterModule, Routes, Router } from '@angular/router';
import { EmailValidator } from '@angular/forms';
import { CustomValidation } from './customValidation';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css'],
    providers: [UserService, FormBuilder]
})
/** register component*/
export class RegisterComponent implements OnInit
{
    registerForm: FormGroup;
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
    constructor(private riderQcService: RiderqcService,
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
                Validators.minLength(4),
                Validators.email
            ]),
            'password': new FormControl(this.user.Password, [
                Validators.required,
                Validators.minLength(4)
            ]),
            'passwordConf': new FormControl(this.user.PasswordConf, [
                Validators.required,
                Validators.minLength(4)
            ]),
            'ville': new FormControl(this.user.Ville, [
                Validators.required,
                Validators.minLength(2)
            ]),
            'region': new FormControl(this.user.Password, [Validators.required]),
            'dateOfBirth': new FormControl(this.user.DateOfBirth, [Validators.required]),
            'description': new FormControl(this.user.Description)
        }, {
                validator: CustomValidation.MatchPassword
        });
    }

    lookIfValid() {
        return this.registerForm.valid
    }

    register() {
        var registerResponse;
      
            /*this.userService.register({
                "Username": btoa("trumpanonyme@baraclou.siphon"),
                "Password": btoa("asdasdasd"),
                "Region": "Mauricie",
                "Ville": "Pincourt",
                "DateOfBirth": "2050-01-01T00:00:00.000Z",
                "Description": "Un tit test"
            }).subscribe(
                (response) => {
                    //Here you can map the response to a type.
                    console.log("regRes: " + response);
                },
                (err) => {
                    //Here you can catch the error
                   // this.errorLabel.setText(err);
                    console.log("regErr: " + err);
                },
                () => {
                  //this.router.navigate(['rides'])
                }
            );*/
       
        this.userService.register({
            "Username": btoa(this.registerForm.value.name),
            "Password": btoa(this.registerForm.value.password),
            "Region": this.registerForm.value.region,
            "Ville": this.registerForm.value.ville,
            "DateOfBirth": this.registerForm.value.dateOfBirth + "T00:00:00.000Z",
            "Description": this.registerForm.value.description
        }).subscribe(
            (response) => {
                //Here you can map the response to a type.
                this.errorlbl = response;
                console.log("regRes: " + response);
            },
            (err) => {
                //Here you can catch the error
                this.errorlbl = err;
                console.log("regErr: " + err);
            },
            () => { /*this.router.navigate(['rides'])*/ }
        );

        

        //console.log(registerResponse.status);
        /*var cache = [];
        console.log("okok:" + JSON.stringify(test, function (key, value) {
            if (typeof value === 'object' && value !== null) {
                if (cache.indexOf(value) !== -1) {
                    // Circular reference found, discard key
                    return;
                }
                // Store value in our collection
                cache.push(value);
            }
            return value;
        }));*/
    }
}
