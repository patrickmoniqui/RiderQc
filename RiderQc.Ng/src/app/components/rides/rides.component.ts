import { Component, OnInit } from '@angular/core';
import { RiderqcService } from '../../services/riderqc.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css'],
  providers:[RiderqcService]
})
export class RidesComponent implements OnInit {
    public rides: Ride[];
    constructor(public riderqcSerice: RiderqcService)
    {

    }

    ngOnInit() {
        this.riderqcSerice.getRides().subscribe((rides) => {
            //console.log(rides);
            this.rides = rides;
      });
  }
}

interface Ride {
    Arrive: Date;
    Descript: string;
}
