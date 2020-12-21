import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/_services/user/user.service';

@Component({
  selector: 'app-pay-pal-result',
  templateUrl: './pay-pal-result.component.html',
  styleUrls: ['./pay-pal-result.component.scss']
})
export class PayPalResultComponent implements OnInit {

  success: boolean = false;
  error: boolean = false;
  errorStatus: string = '';
  token: string = '';
  paymentId: string = '';
  payerId: string = '';
  serviceUrl: string | null = null;

  constructor(private activatedRoute: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    this.success = true;
    this.serviceUrl = localStorage.getItem('PayPalUrl');
    this.activatedRoute.queryParams.subscribe(params => {
      console.log(params);
      this.token = params['token'];
      this.paymentId = params['paymentId'];
      this.payerId = params['PayerID'];
    });
    console.log(this.token);
  }

  execute() {
    let executePayment: any = {
      payerID: this.payerId,
      paymentId: this.paymentId,
      token: this.token
    };
    this.executePayment(executePayment);
  }

  executePayment(executePayment: any) {
    console.log(JSON.stringify(executePayment));
    if(this.serviceUrl){
      this.success = false;
      this.userService.payPalExecutePayment(executePayment, this.serviceUrl).subscribe(
        (val) => {
          this.error = false;
          console.log("Execute payment call successful value returned in body", val);
        },
        (response) => {
          this.errorStatus = response.status;
          this.error = true;
          console.log("Execute payment call in error", response);
        },
        () => {
          console.log("The Execute payment observable is now completed.");
          window.open('localhost:4200/paypal', "_self");
        });
    }
  }
}
