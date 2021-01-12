import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { WebStore } from 'src/app/_models/web-store';
import { PaymentServicesService } from 'src/app/_services/payment-services/payment-services.service';
import { ToastrService } from 'src/app/_services/toastr/toastr.service';
import { UserService } from 'src/app/_services/user/user.service';
import { WebStoreService } from 'src/app/_services/web-store/web-store.service';

@Component({
  selector: 'app-add-payment-service',
  templateUrl: './add-payment-service.component.html',
  styleUrls: ['./add-payment-service.component.scss']
})
export class AddPaymentServiceComponent implements OnInit {
  serviceForm = new FormGroup({
    name: new FormControl(''),
    url: new FormControl(''),
    description: new FormControl(''),
    isPassTrough: new FormControl(''),
    webStoreId: new FormControl(''),
  });

  response: string = '';
  confirmUrl: string = '';
  executeUrl: string = '';
  error: boolean = false;
  isVisible: boolean = false;
  success: boolean = false;
  statusCode: string | null = null;
  webStores: WebStore[] = new Array<WebStore>();
  showForm = false;

  constructor(private activatedRoute: ActivatedRoute, private paymentService: PaymentServicesService, private toastrService: ToastrService,
     private webStore: WebStoreService, private location: Location) { 
    
  }

  ngOnInit() {
    this.webStore.getWebStores(1, 100).subscribe((result: any) => {
      this.webStores = result.items;
      console.log('WebStores rec: ', result);
      this.showForm = true;
    });
  }

  formData() {
    let serviceRequest = {
      name: this.serviceForm.value.name,
      url: this.serviceForm.value.url,
      description: this.serviceForm.value.description,
      isPassTrough: this.serviceForm.value.isPassTrough === 'True' ? true : false,
      webStoreId: this.serviceForm.value.webStoreId
    };
    this.createService(serviceRequest);
  }

  private createService(request: any) {
    this.paymentService.addPaymentService(request).subscribe((result: any) => {
      console.log(result.state);
      this.toastrService.successMessage('Added new service');
      this.location.back();
    });
  }

  changeIsPassThrough(e: any){
    this.serviceForm.patchValue(e.target.value, {
      onlySelf: true
    })
  }

  changeWebStore(e: any){
    this.serviceForm.patchValue(e.target.value, {
      onlySelf: true
    })
  }

}
