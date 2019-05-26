import { TestBed } from '@angular/core/testing';

import { AgecategoryService } from './agecategory.service';

describe('AgecategoryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AgecategoryService = TestBed.get(AgecategoryService);
    expect(service).toBeTruthy();
  });
});
