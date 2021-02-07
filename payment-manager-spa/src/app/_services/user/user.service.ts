import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaymentOrderDetails } from 'src/app/_models/payment-order-details';
import { PaymentOrderResponse } from 'src/app/_models/payment-order-response';
import { UserPaymentService } from 'src/app/_models/user-payment-service';
import { UserSubscriptionOption } from 'src/app/_models/user-subscription-option';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiUrl;
  
  constructor(private http: HttpClient, private authService: AuthService) { }
  
  getPaymentServices(orderId: string) : Observable<UserPaymentService[]>{
    return this.http.get<UserPaymentService[]>(this.baseUrl + 'api/payment/getpaymentoptionsfororder?orderId=' + orderId);
  }

  getSubscriptionOptions(webstoreId: string) : Observable<UserSubscriptionOption[]>{
    return this.http.get<UserSubscriptionOption[]>('http://192.168.0.14:5006/paypal/paypal/subscription/get?webStoreId=' + webstoreId);
  }

  createBillingPlan(billingPlan: any) : Observable<any>{
    return this.http.post<any>('http://192.168.0.14:5006/paypal/paypal/billingplan/create', billingPlan);
  }

  createSubscription(subscription: any) : Observable<any>{
    return this.http.post<any>('http://192.168.0.14:5006/paypal/paypal/subscription/create', subscription);
  }

  getPaymentRequestUrl(orderId: string, serviceUrl: string) : Observable<PaymentOrderResponse>{
    const data = {
      orderId: orderId,
      paymentServiceUrl: serviceUrl
    };
    return this.http.post<PaymentOrderResponse>(this.baseUrl + `api/payment/paybypaymentcard`, data);
  }

  getPaymentDetails(orderId: string) {
    return this.http.get<PaymentOrderDetails>(this.baseUrl + 'api/payment/getpaymentrequestdetails?orderId=' + orderId)
  }

  payPalCreatePayment(payPalPaymentRequest: any, serviceUrl: string | null) {
    return this.http.post<any>(serviceUrl + 'paypal/createpayment', payPalPaymentRequest);
  }

  payPalExecutePayment(payPalExecuteRequest: any, serviceUrl: string | null) {
    return this.http.post<any>(serviceUrl + 'paypal/executepayment', payPalExecuteRequest);
  }

  bitCoinExecutePayment(cryptoRequest: any, serviceUrl: string) {
    return this.http.post<any>(serviceUrl + 'bitcoin/create-payment', cryptoRequest);
  }
}
