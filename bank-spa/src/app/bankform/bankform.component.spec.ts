import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankformComponent } from './bankform.component';

describe('BankformComponent', () => {
  let component: BankformComponent;
  let fixture: ComponentFixture<BankformComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankformComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
