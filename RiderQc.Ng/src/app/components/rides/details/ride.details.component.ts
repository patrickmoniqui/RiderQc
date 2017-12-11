import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RiderqcService } from '../../../services/riderqc.service';
import { Ride } from '../../../model/ride';

@Component({
  selector: 'app-ride-details',
  templateUrl: './ride.details.component.html',
  styleUrls: ['./ride.details.component.css'],
  providers:[RiderqcService]
})
export class RideDetailsComponent implements OnInit {
    public ride: Ride;
    public rides: Ride[];
    participants: string[] = [];
    sub: any;

    constructor(public riderqcSerice: RiderqcService,
                private route: ActivatedRoute,
                private router: Router) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            console.log('getting ride with id: ', id);
            this.riderqcSerice.getRides().subscribe((rides) => {
                console.log(rides);
                this.ride = rides[0];
            });
            this.riderqcSerice.getRidesParticipants(id).subscribe((participants) => {
                console.log("particpants: " + participants);
                this.participants = participants;
            });
            /*this.riderqcSerice
                .details(id)
                .subscribe(
                p => {
                    this.ride = p;
                });*/
        });
    }

    participate() {
        this.riderqcSerice.participate(this.ride.RideId).subscribe((participant) => {
        });
    }

    getTimes = function (n) {
        return new Array(n);
    };
}
