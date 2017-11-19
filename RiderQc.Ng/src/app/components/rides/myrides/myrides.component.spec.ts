import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyridesComponent } from './myrides.component';

describe('MyridesComponent', () => {
  let component: MyridesComponent;
  let fixture: ComponentFixture<MyridesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyridesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyridesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
