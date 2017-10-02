import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FormControl, FormBuilder, Validators, ReactiveFormsModule  } from '@angular/forms';
import { RiderqcService } from '../../services/riderqc.service';
import { UserService } from '../../services/user.service';

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
        private fb: FormBuilder)
    {
        this.createForm();
    }

    /** Called by Angular after register component initialized */
    ngOnInit(): void {
        

        this.test();
    }

    createForm() {
        this.registerForm = this.fb.group({
            'name': new FormControl(this.user.name, [
                Validators.required,
                Validators.minLength(4)
            ]),
            'password': new FormControl(this.user.password, [
                Validators.required,
                Validators.minLength(4)
            ]),
            'passwordConf': new FormControl(this.user.passwordConf, [
                Validators.required,
                Validators.minLength(4)
            ]),
            /*'email': new FormControl(this.user.email, [
                Validators.required,
                Validators.minLength(4)
            ]),*/
            'region': new FormControl(this.user.password, [Validators.required])
        });
    }

    test() {
        /*this.userService.register({
            "Username": "test44",
            "Password": "aaa111",
            "Region": "Mauricie",
            "Ville": "Pincourt",
            "DateOfBirth": "2050-01-01T00:00:00.000Z",
            "Description": "Un tit test",
            "DpUrl": ""
        }).subscribe(data => {
            console.log("HERE: " + data);
            console.log("HERE: " + JSON.stringify(data));
        });*/
    }
}
