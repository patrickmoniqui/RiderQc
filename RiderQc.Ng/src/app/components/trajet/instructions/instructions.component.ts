import { Component, OnInit, Input } from '@angular/core';
import { TrajetInfo } from "../../../model/trajetinfo";

@Component({
    selector: 'app-instructions',
    templateUrl: './instructions.component.html',
    styleUrls: ['./instructions.component.css']
})
/** instructions component*/
export class InstructionsComponent implements OnInit {
    constructor() { }
    //@Input() instructionsInfo: any;
    @Input() trajetInfo: TrajetInfo = new TrajetInfo();

    ngOnInit() {
        //this.setTrajetInfo(this.instructionsInfo);
    }

    setTrajetInfo(instructionsInfo: any) {
        let i = 0;
        for (let step of instructionsInfo.steps) {
            this.trajetInfo.steps[i] = {
                instructions: step.instructions,
                duration: step.duration.text,
                distance: step.distance.text
            }
            i++;
        }
        this.trajetInfo.duration = instructionsInfo.duration.text;
        this.trajetInfo.distance = instructionsInfo.distance.text;
    }
}
