import { TestBed } from '@angular/core/testing';

import { HelplinesService } from './helplines.service';

describe('HelplinesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HelplinesService = TestBed.get(HelplinesService);
    expect(service).toBeTruthy();
  });
});
