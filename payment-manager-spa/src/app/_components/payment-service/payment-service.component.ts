import { Component, OnInit } from '@angular/core';
import { PaymentServicesService } from 'src/app/_services/payment-services/payment-services.service';
import { ToastrService } from 'src/app/_services/toastr/toastr.service';

@Component({
  selector: 'app-payment-service',
  templateUrl: './payment-service.component.html',
  styleUrls: ['./payment-service.component.scss']
})
export class PaymentServiceComponent implements OnInit {
  paymentServices: any;

  constructor(private paymentServicesService: PaymentServicesService, private toastrService: ToastrService) { }

  ngOnInit() {
    this.paymentServicesService.getPaymentService(1, 10).subscribe((res: any[]) => {
      this.paymentServices = res;
      this.toastrService.successMessage('Recieved all payment services');
      console.log(this.paymentServices);
    })
  }

}
