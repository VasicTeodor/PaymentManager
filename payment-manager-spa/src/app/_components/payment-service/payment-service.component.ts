import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PaymentService } from 'src/app/_models/payment-service';
import { PaymentServicesResolver } from 'src/app/_resolvers/payment-services.resolver';
import { PaymentServicesService } from 'src/app/_services/payment-services/payment-services.service';
import { ToastrService } from 'src/app/_services/toastr/toastr.service';

@Component({
  selector: 'app-payment-service',
  templateUrl: './payment-service.component.html',
  styleUrls: ['./payment-service.component.scss']
})
export class PaymentServiceComponent implements OnInit {
  paymentServices: PaymentService[] = new Array<PaymentService>();
  paymentService: PaymentService | null = null;

  constructor(private paymentServicesService: PaymentServicesService, private toastrService: ToastrService, 
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.paymentServices = data.paymentServices.items;
      console.log(this.paymentServices);
      console.log(data);
    });
  }

  getPaymentService(id: string) {
    this.paymentServicesService.getPaymentService(id).subscribe((service: PaymentService) => {
      this.paymentService = service;
      console.log(service);
      this.toastrService.successMessage('Recieved all payment services');
    });
  }

  close() {
    this.paymentService = null;
  }

  deletePaymentService(service: PaymentService){
    this.paymentServicesService.removePaymentService(service.id).subscribe((result: any) => {
      this.toastrService.successMessage('Removed Payment Service: ' + service.name);
      this.fetchServices();
    });
  }

  private fetchServices(){
    this.paymentServicesService.getPaymentServices().subscribe((data: any) => {
      this.paymentServices = data.items;
    });
  }
}
