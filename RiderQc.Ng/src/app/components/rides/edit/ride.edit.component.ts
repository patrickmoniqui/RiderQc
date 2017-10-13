import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, NgForm } from '@angular/forms';
import { RiderqcService } from '../../../services/riderqc.service';
import { Ride } from '../../../model/ride';
import { Level } from '../../../model/level';

@Component({
  selector: 'app-ride-edit',
  templateUrl: './ride.edit.component.html',
  styleUrls: ['./ride.edit.component.css'],
  providers:[RiderqcService]
})
export class RideEditComponent implements OnInit {
    ride: Ride;
    levels: Level[];
    isModification: boolean = false;
    sub: any;
    response: any;
    rideForm: FormGroup;

    constructor(public riderqcService: RiderqcService,
                private route: ActivatedRoute,
                private router: Router,
                private formBuilder: FormBuilder) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            this.riderqcService.levelList().subscribe(l => {
                this.levels = l;
                console.log(this.levels[0].Name);
            })
            if (id != null && id != 0) {
                this.isModification = true;
                console.log('getting ride with id: ', id);
                this.riderqcService
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
                description: ['', Validators.required],
                creatorid: ['', Validators.required],
                trajetid: ['', Validators.required],
                levelid: ['', Validators.required],
                datedepart: ['', Validators.required],
                datefin: ['', Validators.required]
            });

        });
    }

    submitForm() {}
}
