import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, NgForm } from '@angular/forms';
import { TrajetService } from '../../../services/trajet.service';
import { Trajet } from "../../../model/trajet";

@Component({
    selector: 'app-trajet',
    templateUrl: './trajet.details.component.html',
    styleUrls: ['./trajet.details.component.css'],
    providers: [TrajetService]
})
/** trajet component*/
export class TrajetDetailsComponent {
    @Input() trajetId;
    public trajet: Trajet;
    sub: any;
    response: any;

    constructor(public trajetService: TrajetService,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            if (id) {
                console.log('getting trajet with id: ', id);
                this.trajetService
                    .details(id)
                    .subscribe(trajet => {
                        this.trajet = trajet;
                    });
            }
        });
    }
}
