import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, NgForm } from '@angular/forms';

//Models
import { Ride } from '../../../model/ride';
import { Level } from '../../../model/level';
import { Trajet } from '../../../model/trajet';
import { User } from '../../../model/user';

//Services
import { RideService } from '../../../services/ride.service';
import { UserService } from '../../../services/user.service';
import { TrajetService } from '../../../services/trajet.service';

@Component({
  selector: 'app-ride-edit',
  templateUrl: './ride.edit.component.html',
  styleUrls: ['./ride.edit.component.css'],
  providers: [
      RideService,
      TrajetService,
      UserService
  ]
})
export class RideEditComponent implements OnInit {
    ride: Ride;
    levels: Level[];
    trajets: Trajet[];
    public user: User;
    public isLogged: Boolean;
    isModification: boolean = false;
    sub: any;
    response: any;
    rideForm: FormGroup;

    constructor(public rideService: RideService,
                private trajetService: TrajetService,
                private userService: UserService,
                private route: ActivatedRoute,
                private router: Router,
                private formBuilder: FormBuilder) {
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
            this.rideService.levelList().subscribe(l => {
                this.levels = l;
                console.log(this.levels[0].Id);
            });
            this.trajetService.getTrajets().subscribe(t => {
                this.trajets = t;
            });
            if (id) {
                this.isModification = true;
                console.log('getting ride with id: ', id);
                this.rideService
                    .details(id)
                    .subscribe(ride => {
                        this.ride = ride;
                    });
            } else {
                this.ride = new Ride();
            }
            this.rideForm = this.formBuilder.group({
                rideid: [''],
                title: ['', Validators.required],
                description: [''],
                creatorid: ['', Validators.required],
                trajetid: [''],
                levelid: ['', Validators.required],
                datedepart: ['', Validators.required],
                datefin: ['', Validators.required]
            }, { validator: this.endDateAfterOrEqualValidator }
            );

        });
    }

    endDateAfterOrEqualValidator(formGroup): any {
        var startDateTimestamp, endDateTimestamp;
        for (var controlName in formGroup.controls) {
            if (controlName.indexOf("datedepart") !== -1) {
                startDateTimestamp = Date.parse(formGroup.controls[controlName].value);
            }
            if (controlName.indexOf("datefin") !== -1) {
                endDateTimestamp = Date.parse(formGroup.controls[controlName].value);
            }
        }
        return (endDateTimestamp < startDateTimestamp) ? { endDateLessThanStartDate: true } : null;
    }

    submitForm() {
        if (this.user) {
            if (this.rideForm.valid) {
                this.ride.Creator = this.user;
                this.rideForm.patchValue({ creatorid: this.user.UserID });
                if (this.isModification) {
                  console.log("Update put");
                  this.rideService
                      .update(this.rideForm.value)
                      .subscribe(p => {
                          this.response = p;
                          alert(this.response);
                          console.log("ride title", this.ride.Title);
                      });
                } else {
                    console.log("Add ride");
                    this.rideService
                        .add(this.rideForm.value)
                        .subscribe(p => {
                            this.response = p;
                            alert(this.response);
                            console.log("ride title", this.ride.Title);
                        });
                }
                console.log("you submitted value: ", this.rideForm.value);
            }
        } else {
            console.log("no user");
        }
    }
}
