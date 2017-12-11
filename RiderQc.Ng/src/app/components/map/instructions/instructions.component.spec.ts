/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { InstructionsComponent } from './instructions.component';

let component: InstructionsComponent;
let fixture: ComponentFixture<InstructionsComponent>;

describe('instructions component', () =>
{
    beforeEach(async(() =>
    {
        TestBed.configureTestingModule({
            declarations: [InstructionsComponent],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(InstructionsComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() =>
    {
        expect(true).toEqual(true);
    }));
});