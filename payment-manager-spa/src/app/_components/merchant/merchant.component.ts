import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PaymentService } from 'src/app/_models/payment-service';
import { WebStore } from 'src/app/_models/web-store';
import { MerchantService } from 'src/app/_services/merchant/merchant.service';
import { PaymentServicesService } from 'src/app/_services/payment-services/payment-services.service';
import { ToastrService } from 'src/app/_services/toastr/toastr.service';
import { WebStoreService } from 'src/app/_services/web-store/web-store.service';

@Component({
  selector: 'app-merchant',
  templateUrl: './merchant.component.html',
  styleUrls: ['./merchant.component.scss']
})
export class MerchantComponent implements OnInit {
  merchantForm = new FormGroup({
    merchantUniqueId: new FormControl('', [Validators.required]),
    merchantPassword: new FormControl('', [Validators.required]),
    merchantUniqueStoreId: new FormControl('', [Validators.required]),
    webStoreId: new FormControl('', [Validators.required]),
    paymentServices: new FormControl('', [Validators.required]),
  });

  response: string = '';
  confirmUrl: string = '';
  executeUrl: string = '';
  error: boolean = false;
  isVisible: boolean = false;
  success: boolean = false;
  statusCode: string | null = null;
  webStores: WebStore[] = new Array<WebStore>();
  paymentServices: PaymentService[] = new Array<PaymentService>();
  showForm = false;
  showPaymentOptions = false;

  constructor(private activatedRoute: ActivatedRoute, private merchantService: MerchantService, private location: Location,
              private paymentServicesService: PaymentServicesService, private webStoreService: WebStoreService, private toastr: ToastrService) { 
  }

  ngOnInit() {
    this.webStoreService.getWebStores(1, 100).subscribe((result: any) => {
      this.webStores = result.items;
      this.showForm = true;
    });
  }

  formData() {
    let merchantRequest = {
      merchantUniqueId: this.merchantForm.value.merchantUniqueId,
      merchantPassword: this.merchantForm.value.merchantPassword,
      merchantUniqueStoreId: this.merchantForm.value.merchantUniqueStoreId,
      webStoreId: this.merchantForm.value.webStoreId,
      paymentServices: this.merchantForm.value.paymentServices
    };
    this.addMerchant(merchantRequest);
  }

  private addMerchant(request: any) {
    if (!this.merchantForm.valid) {
      return false;
    } else {
      alert(JSON.stringify(request));
      this.merchantService.addMerchant(request).subscribe((val) => {
        this.toastr.successMessage('Merchant added');
        this.location.back();
      });
      return true;
    }
    
  }

  changeWebStore(e: any){
    this.showPaymentOptions = false;
    let webstoreId = e.target.value;
    webstoreId = webstoreId.substring(3);
    this.webStoreService.getWebStore(webstoreId).subscribe((result: any) => {
      console.log('RESULLLT', result);
      result.paymentOptions.forEach((el: any) => {
        this.paymentServices.push(el.paymentService as PaymentService);
      });
      this.showPaymentOptions = true;
    })
    this.merchantForm.patchValue(e.target.value, {
      onlySelf: true
    })
  }

  changeSelection(e: any){
    this.merchantForm.patchValue(e.target.value, {
      onlySelf: true
    })
  }
}
