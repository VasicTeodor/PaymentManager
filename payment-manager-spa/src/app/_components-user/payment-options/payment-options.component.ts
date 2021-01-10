import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentOrderResponse } from 'src/app/_models/payment-order-response';
import { UserPaymentService } from 'src/app/_models/user-payment-service';
import { UserService } from 'src/app/_services/user/user.service';

@Component({
  selector: 'app-payment-options',
  templateUrl: './payment-options.component.html',
  styleUrls: ['./payment-options.component.scss']
})
export class PaymentOptionsComponent implements OnInit {
  token: string = '';
  orderId: string = '';
  paymentOptions: UserPaymentService[] | null = null;
  constructor(private activatedRoute: ActivatedRoute, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params  => {
      this.token = params['token'];
      this.orderId = params['orderId'];
      localStorage.setItem('token', this.token);
      this.userService.getPaymentServices(this.orderId).subscribe((result: UserPaymentService[]) => {
        console.log('Received user services', result);
        this.paymentOptions = result;
      })
     });
  }

  redirectToPayment(paymentOption: UserPaymentService) {
    if(paymentOption.paymentManagerUrl) {
      this.payByPaymentCard(paymentOption);
    } else {
      this.payByOtherOption(paymentOption);
    }
  }
  
  private payByPaymentCard(paymentOption: UserPaymentService) {
    this.userService.getPaymentRequestUrl(this.orderId, paymentOption.url).subscribe((result: PaymentOrderResponse) =>{
      window.open(result.paymentUrl, "_self");
    }, error => {
      console.log(error)
    });
  }

  private payByOtherOption(paymentOption: UserPaymentService) {
    if(paymentOption.name === 'PayPal') {
      localStorage.setItem('PayPalUrl', paymentOption.url);
      this.router.navigate([`/paypal/${this.orderId}`])
    } else {
      localStorage.setItem('BitCoinUrl', paymentOption.url);
      this.router.navigate([`/crypto/${this.orderId}`])
    }
  }

}
