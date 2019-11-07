import { TestBed } from '@angular/core/testing';

import { OidcService } from './oidc.service';

describe('OidcService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OidcService = TestBed.get(OidcService);
    expect(service).toBeTruthy();
  });
});
