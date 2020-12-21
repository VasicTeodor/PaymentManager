/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PayPalComponent } from './pay-pal.component';

describe('PayPalComponent', () => {
  let component: PayPalComponent;
  let fixture: ComponentFixture<PayPalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PayPalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PayPalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
