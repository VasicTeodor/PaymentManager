import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user/user.service';

@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.scss']
})
export class SubscriptionComponent implements OnInit {
  subscriptionForm = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    subscriptionType: new FormControl(''),
    paymentType: new FormControl(''),
    frequency: new FormControl(''),
    frequencyInterval: new FormControl(''),
    cycles: new FormControl(''),
    currency: new FormControl(''),
    amount: new FormControl(''),
    successUrl: new FormControl(''),
    failedUrl: new FormControl(''),
    webStoreId: new FormControl('')
  });

  response: string = '';
  confirmUrl: string = '';
  executeUrl: string = '';
  billingPlanId: string = '';
  error: boolean = false;
  isVisible: boolean = false;
  success: boolean = false;
  statusCode: string | null = null;

  constructor(private activatedRoute: ActivatedRoute, private userService: UserService) { 
    this.activatedRoute.params.subscribe(params  => {
     });
  }

  ngOnInit() {
  }

  formData() {
    let billingPlanRequest = {
      name: this.subscriptionForm.value.name,
      description: this.subscriptionForm.value.description,
      subscriptionType: this.subscriptionForm.value.subscriptionType,
      paymentType: this.subscriptionForm.value.paymentType,
      frequency: this.subscriptionForm.value.frequency,
      frequencyInterval: this.subscriptionForm.value.frequencyInterval,
      cycles: this.subscriptionForm.value.cycles,
      currency: this.subscriptionForm.value.currency,
      amount: this.subscriptionForm.value.amount,
      successUrl: this.subscriptionForm.value.successUrl,
      failedUrl: this.subscriptionForm.value.failedUrl,
      webStoreId: this.subscriptionForm.value.webStoreId
    };
    this.createBillingPlan(billingPlanRequest);
  }

  private createBillingPlan(request: any) {
    this.userService.createBillingPlan(request).subscribe((val) => {
      this.response = val.state;
      this.isVisible = true;
      this.error = false;
      this.success = true;
      this.billingPlanId = val.id;
      console.log("POST call successful value returned in body", val);
    },
    response => {
      this.isVisible = false;
      this.statusCode = response.status;
      this.error = true;
      this.success = false;
      console.log("POST call in error", response);
    },
    () => {
      console.log("The POST observable is now completed.");
    });
  }

  public createSubscription() {
    let subscription = {
      billingPlanId: this.billingPlanId,
      webStoreId: this.subscriptionForm.value.webStoreId
    };

    this.userService.createSubscription(subscription).subscribe((val) => {
      this.response = val.state;
      this.isVisible = true;
      this.error = false;
      this.success = true;
      console.log("POST call successful value returned in body", val);
    },
    response => {
      this.isVisible = false;
      this.statusCode = response.status;
      this.error = true;
      this.success = false;
      console.log("POST call in error", response);
    },
    () => {
      console.log("The POST observable is now completed.");
    });
  }
}
