import { TestBed } from '@angular/core/testing';

import { RegisterGuardService } from './register-guard.service';

describe('RegisterGuardService', () => {
  let service: RegisterGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegisterGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
