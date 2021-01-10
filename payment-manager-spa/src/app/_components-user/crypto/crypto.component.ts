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
    amount: new FormControl(''),
    priceCurrency: new FormControl(''),
    recieveCurrency: new FormControl(''),
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

  constructor(private activatedRoute: ActivatedRoute, private userService: UserService) { 
    this.activatedRoute.params.subscribe(params  => {
      this.orderId = params['orderId'];
      this.serviceUrl = localStorage.getItem('BitCoinUrlradio');

      this.userService.getPaymentDetails(this.orderId).subscribe((result: PaymentOrderDetails) => {
        console.log('Received paymentOrderDetails', result);
        this.paymentOrderDetails = result;
        
        this.cryptoForm = new FormGroup({
          amount: new FormControl(this.paymentOrderDetails.amount),
          priceCurrency: new FormControl(''),
          recieveCurrency: new FormControl(''),
          title: new FormControl(''),
          description: new FormControl(''),
        });
      });
     });
  }

  ngOnInit() {
  }

  formData() {
    let cryptoRequest = {
      amount: this.cryptoForm.value.amount,
      priceCurrency: this.cryptoForm.value.priceCurrency,
      recieveCurrency: this.cryptoForm.value.recieveCurrency,
      title: this.cryptoForm.value.title,
      description: this.cryptoForm.value.description,
      callbackUrl: 'http://localhost:4200/paypalfailed',
      cancelUrl: 'http://localhost:4200/paypalfailed',
      successUrl: 'http://localhost:4200/paypalfailed',
      orderId: this.orderId
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
