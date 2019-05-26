import { TestBed } from '@angular/core/testing';

import { SomkerStatService } from './somker-stat.service';

describe('SomkerStatService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SomkerStatService = TestBed.get(SomkerStatService);
    expect(service).toBeTruthy();
  });
});
