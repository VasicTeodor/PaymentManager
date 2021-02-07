import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PayPalLink } from 'src/app/_models/pay-pal-link';
import { PaymentOrderDetails } from 'src/app/_models/payment-order-details';
import { UserService } from 'src/app/_services/user/user.service';

@Component({
  selector: 'app-crypto',
  templateUrl: './crypto.component.html',
  styleUrls: ['./crypto.component.scss']
})
export class CryptoComponent implements OnInit {

  cryptoForm = new FormGroup({
    priceAmount: new FormControl(''),
    priceCurrency: new FormControl(''),
    receiveCurrency: new FormControl(''),
    title: new FormControl(''),
    description: new FormControl(''),
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
  merchantId: string = '';

  constructor(private activatedRoute: ActivatedRoute, private userService: UserService) { 
    this.activatedRoute.params.subscribe(params  => {
      this.orderId = params['orderId'];
      this.serviceUrl = localStorage.getItem('BitCoinUrl');

      this.userService.getPaymentDetails(this.orderId).subscribe((result: PaymentOrderDetails) => {
        console.log('Received paymentOrderDetails', result);
        this.paymentOrderDetails = result;
        this.merchantId = result.merchantOrderId;

        this.cryptoForm = new FormGroup({
          priceAmount: new FormControl(this.paymentOrderDetails.amount),
          priceCurrency: new FormControl(''),
          receiveCurrency: new FormControl(''),
          title: new FormControl('Publishing Company Book'),
          description: new FormControl('Book'),
        });
      });
     });
  }

  ngOnInit() {
  }

  formData() {
    let cryptoRequest = {
      priceAmount: this.cryptoForm.value.priceAmount,
      priceCurrency: this.cryptoForm.value.priceCurrency,
      receiveCurrency: this.cryptoForm.value.receiveCurrency,
      title: this.cryptoForm.value.title,
      description: this.cryptoForm.value.description,
      callbackUrl: this.serviceUrl + `bitcoin/payment-status?orderId=${this.orderId}&status=error`,
      cancelUrl: this.serviceUrl + `bitcoin/payment-status?orderId=${this.orderId}&status=failure`,
      successUrl: this.serviceUrl + `bitcoin/payment-status?orderId=${this.orderId}&status=success`,
      orderId: this.orderId,
      merchantId: this.merchantId
    };
    this.createOrder(cryptoRequest);
  }

  private createOrder(request: any) {
    if(this.serviceUrl) {
      this.userService.bitCoinExecutePayment(request, this.serviceUrl).subscribe((val) => {
        this.response = val.state;
        this.isVisible = true;
        this.error = false;
        this.success = true;

        window.open(val.payment_url, "_self");
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

  changeCurrency(e: any){
    this.cryptoForm.patchValue(e.target.value, {
      onlySelf: true
    })
  }
}
