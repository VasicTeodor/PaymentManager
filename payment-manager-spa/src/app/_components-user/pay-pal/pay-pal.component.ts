import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PayPalLink } from 'src/app/_models/pay-pal-link';
import { PaymentOrderDetails } from 'src/app/_models/payment-order-details';
import { UserService } from 'src/app/_services/user/user.service';

@Component({
  selector: 'app-pay-pal',
  templateUrl: './pay-pal.component.html',
  styleUrls: ['./pay-pal.component.scss']
})
export class PayPalComponent implements OnInit {

  paypalForm = new FormGroup({
    amount: new FormControl(''),
    currency: new FormControl(''),
    description: new FormControl(''),
    intent: new FormControl('SALE'),
    paymentMethod: new FormControl('PAYPAL')
  });

  response: string = '';
  confirmUrl: string = '';
  executeUrl: string = '';
  error: boolean = false;
  isVisible: boolean = false;
  success: boolean = false;
  statusCode: string | null = null;
  orderId: string = '';
  serviceUrl: string | null = null;
  paymentOrderDetails: PaymentOrderDetails | null = null;

  constructor(private activatedRoute: ActivatedRoute, private userService: UserService) { 
    this.activatedRoute.params.subscribe(params  => {
      this.orderId = params['orderId'];
      this.serviceUrl = localStorage.getItem('PayPalUrl');

      this.userService.getPaymentDetails(this.orderId).subscribe((result: PaymentOrderDetails) => {
        console.log('Received paymentOrderDetails', result);
        this.paymentOrderDetails = result;
        
        this.paypalForm = new FormGroup({
          amount: new FormControl(this.paymentOrderDetails.amount),
          currency: new FormControl(''),
          description: new FormControl(''),
          intent: new FormControl('SALE'),
          paymentMethod: new FormControl('PAYPAL')
        });
      });
     });
  }

  ngOnInit() {
  }

  formData() {
    let paypalRequest = {
      id: 'DJN',
      amount: this.paypalForm.value.amount,
      currency: this.paypalForm.value.currency,
      description: this.paypalForm.value.description,
      paymentIntent: this.paypalForm.value.intent,
      paymentMethod: this.paypalForm.value.paymentMethod,
      successUrl: 'http://192.168.0.14:4200/paypalresult',
      errorUrl: 'http://192.168.0.14:4200/paypalfailed',
      orderId: this.orderId
    };
    this.createOrder(paypalRequest);
  }

  private createOrder(request: any) {
    if(this.serviceUrl) {
      this.userService.payPalCreatePayment(request, this.serviceUrl).subscribe((val) => {
        this.response = val.state;
        this.isVisible = true;
        this.error = false;
        this.success = true;
        val.links.forEach((link: PayPalLink) => {
          if (link.type === 'execute') {
            this.executeUrl = link.url;
          } else if (link.type === 'approval_url') {
            this.confirmUrl = link.url;
          }
        });
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

  confirm() {
    localStorage.setItem('executeUrl', this.executeUrl);
    window.open(this.confirmUrl, "_self");
  }

}
