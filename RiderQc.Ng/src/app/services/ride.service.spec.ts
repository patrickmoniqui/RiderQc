﻿import { TestBed, inject } from '@angular/core/testing';

import { RideService } from './ride.service';

describe('RiderqcService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RideService]
    });
  });

  it('should be created', inject([RideService], (service: RideService) => {
    expect(service).toBeTruthy();
  }));
});
