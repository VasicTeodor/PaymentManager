/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WebStoreService } from './web-store.service';

describe('Service: WebStore', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WebStoreService]
    });
  });

  it('should ...', inject([WebStoreService], (service: WebStoreService) => {
    expect(service).toBeTruthy();
  }));
});
