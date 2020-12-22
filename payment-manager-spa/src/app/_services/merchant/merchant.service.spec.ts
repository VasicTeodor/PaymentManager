/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MerchantService } from './merchant.service';

describe('Service: Merchant', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MerchantService]
    });
  });

  it('should ...', inject([MerchantService], (service: MerchantService) => {
    expect(service).toBeTruthy();
  }));
});
