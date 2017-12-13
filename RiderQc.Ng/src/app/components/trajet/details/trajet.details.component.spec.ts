/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { TrajetDetailsComponent } from './trajet.details.component';

let component: TrajetDetailsComponent;
let fixture: ComponentFixture<TrajetDetailsComponent>;

describe('TrajetDetailsComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TrajetDetailsComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(TrajetDetailsComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
