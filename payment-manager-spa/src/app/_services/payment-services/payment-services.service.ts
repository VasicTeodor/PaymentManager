import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
import * as constants from '../../_helpers/constants';
import { Observable } from 'rxjs';
import { PaymentService } from 'src/app/_models/payment-service';

@Injectable({
  providedIn: 'root'
})
export class PaymentServicesService {
private baseUrl = environment.apiUrl;

constructor(private http: HttpClient, private authService: AuthService) { }

getPaymentServices(pageNumber?: number, pageSize?: number) : Observable<PaymentService[]>{
  pageNumber = pageNumber ? pageNumber : constants.paginationConstants.pageNumber;
  pageSize = pageSize ? pageSize : constants.paginationConstants.pageSize;
  
  const params = new HttpParams();
  params.append('pageNumber', JSON.stringify(pageNumber));
  params.append('pageSize', JSON.stringify(pageSize));

  return this.http.get<PaymentService[]>(this.baseUrl + 'api/paymentservices/getpaymentservices', {params});
}

getPaymentService(id: string) : Observable<PaymentService>{
  return this.http.get<PaymentService>(this.baseUrl + 'api/paymentservices/getpaymentservice?id=' + id);
}

removePaymentService(id: string) : Observable<PaymentService>{
  return this.http.get<PaymentService>(this.baseUrl + 'api/paymentservices/removepaymentservice?id=' + id);
}

editPaymentService(id: string) : Observable<PaymentService>{
  return this.http.get<PaymentService>(this.baseUrl + 'api/paymentservices/addpaymentservice?id=' + id);
}

addPaymentService(id: string) : Observable<PaymentService>{
  return this.http.get<PaymentService>(this.baseUrl + 'api/paymentservices/addpaymentservice?id=' + id);
}
}
