import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './_services/auth/auth.service';
import { ToastrService } from './_services/toastr/toastr.service';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { LoginComponent } from './_components/login/login.component';
import { SideMenuComponent } from './_components/side-menu/side-menu.component';
import { WebShopComponent } from './_components/web-shop/web-shop.component';
import { PaymentServiceComponent } from './_components/payment-service/payment-service.component';
import { PaymentServicesService } from './_services/payment-services/payment-services.service';
import { PaymentServicesResolver } from './_resolvers/payment-services.resolver';
import { PaginationComponent } from './_components/pagination/pagination.component';
import { PayPalComponent } from './_components-user/pay-pal/pay-pal.component';
import { PaymentOptionsComponent } from './_components-user/payment-options/payment-options.component';
import { PayPalResultComponent } from './_components-user/pay-pal-result/pay-pal-result.component';
import { UserService } from './_services/user/user.service';
import { PayPalFailedComponent } from './_components-user/pay-pal-failed/pay-pal-failed.component';
import { SubscriptionComponent } from './_components/subscription/subscription.component';
import { CryptoComponent } from './_components-user/crypto/crypto.component';
import { SubscriptionOptionsComponent } from './_components-user/subscription-options/subscription-options.component';
import { AddPaymentServiceComponent } from './_components/payment-service/add-payment-service/add-payment-service.component';
import { MerchantComponent } from './_components/merchant/merchant.component';

export function getToken() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SideMenuComponent,
    WebShopComponent,
    PaymentServiceComponent,
    PaginationComponent,
    PayPalComponent,
    PaymentOptionsComponent,
    PayPalResultComponent,
    PayPalFailedComponent,
    SubscriptionComponent,
    CryptoComponent,
    SubscriptionOptionsComponent,
    AddPaymentServiceComponent,
    MerchantComponent
  ],
  imports: [
    JwtModule.forRoot({
      config: {
         tokenGetter: getToken,
         allowedDomains: ['192.168.0.14:5020'],
         disallowedRoutes: ['192.168.0.14:5020/api/authorization']
      }
   }),
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [
    AuthService,
    ToastrService,
    PaymentServicesService,
    UserService,
    PaymentServicesResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
