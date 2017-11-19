/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { MessagingComponent } from './messaging.component';

let component: MessagingComponent;
let fixture: ComponentFixture<MessagingComponent>;

describe('messaging component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ MessagingComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(MessagingComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});