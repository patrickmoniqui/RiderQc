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
    templateUrl: './trajet.edit.component.html',
    styleUrls: ['./trajet.edit.component.css'],
    providers: [
      TrajetService,
      UserService
  ]
})
/** trajet component*/
export class TrajetEditComponent {
    @Input() trajetId;
    public trajet: Trajet;
    public user: User;
    public isLogged: Boolean;
    public userService: UserService;
    isModification: boolean = false;
    sub: any;
    response: any;
    sendValues: boolean;
    trajetForm: FormGroup;

    constructor(public trajetService: TrajetService,
      private _userService: UserService,
      private route: ActivatedRoute,
      private router: Router,
      private formBuilder: FormBuilder) {
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
                this.isModification = true;
                console.log('getting trajet with id: ', id);
                /*this.trajetService
                    .getTrajets()
                    .subscribe(trajets => {
                        this.trajet = trajets[0];
                    });*/
                this.trajetService
                    .details(id)
                    .subscribe(trajet => {
                        this.trajet = trajet;
                    });
            } else {
                this.trajet = new Trajet();
            }
            this.trajetForm = this.formBuilder.group({
                trajetid: [''],
                title: ['', Validators.required],
                description: [''],
                creatorid: ['', Validators.required],
                GpsPoints: ['', Validators.required]
            });

        });
    }

    handleTrajetUpdated(trajet) {
        this.trajetForm.patchValue({ GpsPoints: trajet });
        this.trajet.GpsPoints = trajet;
    }

    submitForm() {
        if (this.user) {
            if (this.trajetForm.valid) {
              this.trajet.Creator = this.user;
              this.trajetForm.patchValue({ creatorid: this.user.UserID });
              if (this.isModification) {
                this.trajetService
                    .update(this.trajetForm.value)
                    .subscribe(p => {
                        this.response = p;
                        alert(this.response);
                    });
              } else {
                  console.log("Add trajet");
                  this.trajetService
                      .add(this.trajetForm.value)
                      .subscribe(p => {
                          this.response = p;
                          alert(this.response);
                      });
              }
            }
        } else {
        }
    }
}
