import { TestBed } from '@angular/core/testing';

import { AntiforgeryInterceptorService } from './antiforgery-interceptor.service';

describe('AntiforgeryInterceptorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AntiforgeryInterceptorService = TestBed.get(AntiforgeryInterceptorService);
    expect(service).toBeTruthy();
  });
});
