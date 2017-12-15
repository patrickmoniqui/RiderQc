import { TestBed, inject } from '@angular/core/testing';

import { MotoService } from './moto.service';

describe('MotoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MotoService]
    });
  });

  it('should be created', inject([MotoService], (service: MotoService) => {
    expect(service).toBeTruthy();
  }));
});
