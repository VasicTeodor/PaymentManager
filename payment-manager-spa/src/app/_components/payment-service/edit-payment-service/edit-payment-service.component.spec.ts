/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EditPaymentServiceComponent } from './edit-payment-service.component';

describe('EditPaymentServiceComponent', () => {
  let component: EditPaymentServiceComponent;
  let fixture: ComponentFixture<EditPaymentServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditPaymentServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditPaymentServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
