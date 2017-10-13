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
    sub: any;

    constructor(public riderqcSerice: RiderqcService,
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
