/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { TrajetEditComponent } from './trajet.edit.component';

let component: TrajetEditComponent;
let fixture: ComponentFixture<TrajetEditComponent>;

describe('TrajetEditComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [TrajetEditComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(TrajetEditComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
