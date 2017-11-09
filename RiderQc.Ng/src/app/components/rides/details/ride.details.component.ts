import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RideService } from '../../../services/ride.service';
import { Ride } from '../../../model/ride';

@Component({
  selector: 'app-ride-details',
  templateUrl: './ride.details.component.html',
  styleUrls: ['./ride.details.component.css'],
  providers:[RideService]
})
export class RideDetailsComponent implements OnInit {
    public ride: Ride;
    sub: any;

    constructor(public riderqcSerice: RideService,
                private route: ActivatedRoute,
                private router: Router) {}

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            console.log('getting ride with id: ', id);
            this.riderqcSerice
                .details(id)
                .subscribe(
                p => {
                    this.ride = p;
                });
        });
  }
}
