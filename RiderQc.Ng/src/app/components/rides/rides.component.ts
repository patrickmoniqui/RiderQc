import { Component, OnInit } from '@angular/core';
import { RiderqcService } from '../../services/riderqc.service';
import { Ride } from '../../model/ride';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css'],
  providers:[RiderqcService]
})
export class RidesComponent implements OnInit {
    public rides: Ride[];

    constructor(public riderqcSerice: RiderqcService) {}

    ngOnInit() {
        this.fetchAllRide();
    }
    
    toggleRideContainer(element) {

    }

    fetchAllRide()
    {
        this.riderqcSerice.getRides().subscribe((rides) => {
            console.log(rides);
            this.rides = rides;
        });
    }

    searchBar_OnChange(element)
    {
        var value = element.srcElement.value;

        if (value != "")
        {
            var tmpRides: Ride[] = new Array();

            for (var i = 0, len = this.rides.length; i < len; i++) {
                if (this.rides[i].Title.includes(value)) {
                    tmpRides.push(this.rides[i]);
                }
            }

            this.rides = tmpRides;

        }
        else
        {
            this.fetchAllRide();
        }
    }

    getTimes = function (n) {
        return new Array(n);
    };
}
