import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, NgForm } from '@angular/forms';
import { TrajetService } from '../../../services/trajet.service';
import { Trajet } from "../../../model/trajet";

@Component({
    selector: 'app-trajet',
    templateUrl: './trajet.edit.component.html',
    styleUrls: ['./trajet.edit.component.css'],
    providers: [TrajetService]
})
/** trajet component*/
export class TrajetEditComponent {
    @Input() trajetId;
    public trajet: Trajet;
    isModification: boolean = false;
    sub: any;
    response: any;
    sendValues: boolean;
    trajetForm: FormGroup;

    constructor(public trajetService: TrajetService,
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.sub = this.route.params.subscribe(params => {
            let id = Number.parseInt(params['id']);
            if (id) {
                this.isModification = true;
                console.log('getting trajet with id: ', id);
                /*this.trajetService
                    .getTrajets()
                    .subscribe(trajets => {
                        this.trajet = trajets[0];
                    });*/
                this.trajetService
                    .details(id)
                    .subscribe(trajet => {
                        this.trajet = trajet;
                    });
            } else {
                this.trajet = new Trajet();
            }
            this.trajetForm = this.formBuilder.group({
                trajetid: [''],
                title: ['', Validators.required],
                description: ['', Validators.required],
                creatorid: [''],
                gpsPoints: ['', Validators.required]
            });

        });
    }

    handleTrajetUpdated(trajet) {
        this.trajetForm.patchValue({ gpsPoints: trajet });
        this.trajet.GpsPoints = trajet;
        console.log("gps points: ", this.trajet.GpsPoints);
        console.log("updated value: ", this.trajetForm.value);
    }

    submitForm() {
        console.log("you submitted value: ", this.trajetForm.value);
    }
}
