import { TestBed } from '@angular/core/testing';

import { ProductEventEmitterService } from './product-event-emitter.service';

describe('ProductEventEmitterService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProductEventEmitterService = TestBed.get(ProductEventEmitterService);
    expect(service).toBeTruthy();
  });
});
