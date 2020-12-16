/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PaymentServicesService } from './payment-services.service';

describe('Service: PaymentServices', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PaymentServicesService]
    });
  });

  it('should ...', inject([PaymentServicesService], (service: PaymentServicesService) => {
    expect(service).toBeTruthy();
  }));
});
