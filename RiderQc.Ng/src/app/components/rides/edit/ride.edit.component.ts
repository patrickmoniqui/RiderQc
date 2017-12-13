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
    public userService: UserService;
    isModification: boolean = false;
    sub: any;
    response: any;
    rideForm: FormGroup;

    constructor(public rideService: RideService,
                private trajetService: TrajetService,
                private route: ActivatedRoute,
                private router: Router,
                private formBuilder: FormBuilder) {}

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
                rideid: ['', Validators.required],
                title: ['', Validators.required],
                description: [''],
                creatorid: ['', Validators.required],
                trajetid: [''],
                levelid: ['', Validators.required],
                datedepart: ['', Validators.required],
                datefin: ['', Validators.required]
            });

        });
    }

    submitForm() {
        if (this.user) {
            this.ride.Creator = this.user;
            this.rideForm.patchValue({ creatorid: this.user.UserID });
            this.rideService.add(this.ride);
            console.log("you submitted value: ", this.rideForm.value);
        } else {
            console.log("no user");
        }
    }
}
