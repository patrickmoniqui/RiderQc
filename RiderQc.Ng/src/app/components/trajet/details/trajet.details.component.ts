import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, NgForm } from '@angular/forms';

//Services
import { TrajetService } from '../../../services/trajet.service';
import { UserService } from "../../../services/user.service";

//Models
import { Trajet } from "../../../model/trajet";
import { User } from "../../../model/user";

@Component({
    selector: 'app-trajet',
    templateUrl: './trajet.details.component.html',
    styleUrls: ['./trajet.details.component.css'],
    providers: [TrajetService]
})
/** trajet component*/
export class TrajetDetailsComponent {
    @Input() trajetId;
    public trajet: Trajet;
    public user: User;
    public isLogged: Boolean;
    public userService: UserService;
    sub: any;
    response: any;

    constructor(public trajetService: TrajetService,
      private _userService: UserService,
      private route: ActivatedRoute,
      private router: Router) {
        this.userService = _userService;
        this.isLogged = this.userService.isLogged;

        if (this.isLogged) {
            this.userService.getLoggedUser().subscribe(x => this.user = x);
            console.log(this.user);
        }
        else {
            this.user = null;
        }
    }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            if (id) {
                console.log('getting trajet with id: ', id);
                this.trajetService
                    .details(id)
                    .subscribe(trajet => {
                        this.trajet = trajet;
                    });
            }
        });
    }
}
