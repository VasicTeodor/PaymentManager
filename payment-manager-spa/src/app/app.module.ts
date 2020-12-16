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
    PaginationComponent
  ],
  imports: [
    JwtModule.forRoot({
      config: {
         tokenGetter: getToken,
         allowedDomains: ['localhost:5021'],
         disallowedRoutes: ['localhost:5021/api/authorization']
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
    PaymentServicesResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
