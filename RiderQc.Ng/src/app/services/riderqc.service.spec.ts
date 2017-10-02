import { TestBed, inject } from '@angular/core/testing';

import { RiderqcService } from './riderqc.service';

describe('RiderqcService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RiderqcService]
    });
  });

  it('should be created', inject([RiderqcService], (service: RiderqcService) => {
    expect(service).toBeTruthy();
  }));
});
