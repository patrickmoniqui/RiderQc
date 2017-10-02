import { Component, OnInit } from '@angular/core';
import { RiderqcService } from '../../services/riderqc.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})

export class UserComponent implements OnInit {


    constructor(private riderQcService: RiderqcService)
    {
         
    }

    ngOnInit() {
        this.riderQcService.getRides().subscribe((rides) => {
            console.log(rides);
        });
    }
}
