import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CryptoComponent } from './_components-user/crypto/crypto.component';
import { PayPalFailedComponent } from './_components-user/pay-pal-failed/pay-pal-failed.component';
import { PayPalResultComponent } from './_components-user/pay-pal-result/pay-pal-result.component';
import { PayPalComponent } from './_components-user/pay-pal/pay-pal.component';
import { PaymentOptionsComponent } from './_components-user/payment-options/payment-options.component';
import { SubscriptionOptionsComponent } from './_components-user/subscription-options/subscription-options.component';
import { LoginComponent } from './_components/login/login.component';
import { MerchantComponent } from './_components/merchant/merchant.component';
import { AddPaymentServiceComponent } from './_components/payment-service/add-payment-service/add-payment-service.component';
import { PaymentServiceComponent } from './_components/payment-service/payment-service.component';
import { SideMenuComponent } from './_components/side-menu/side-menu.component';
import { SubscriptionComponent } from './_components/subscription/subscription.component';
import { WebShopComponent } from './_components/web-shop/web-shop.component';
import { PaymentServicesResolver } from './_resolvers/payment-services.resolver';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'paymentoptions', component: PaymentOptionsComponent},
  {path: 'subscriptionoptions', component: SubscriptionOptionsComponent},
  {path: 'paypal/:orderId', component: PayPalComponent},
  {path: 'crypto/:orderId', component: CryptoComponent},
  {path: 'paypalresult', component: PayPalResultComponent},
  {path: 'paypalfailed', component: PayPalFailedComponent},
  {path: 'dashboard', component: SideMenuComponent, children: [
    {path:'admin', children: [
      {
        path:'webstore',
        component: WebShopComponent,
        outlet: 'sidemenu'
      },
      {
        path: 'paymentservice',
        component: PaymentServiceComponent,
        resolve: {paymentServices: PaymentServicesResolver},
        outlet: 'sidemenu'
      },
      {
        path: 'subscription',
        component: SubscriptionComponent,
        outlet: 'sidemenu'
      },
      {
        path: 'create-service',
        component: AddPaymentServiceComponent,
        outlet: 'sidemenu'
      },
      {
        path: 'add-merchant',
        component: MerchantComponent,
        outlet: 'sidemenu'
      }
    ]},
  ]},
  { path: '**', redirectTo: '', pathMatch: 'full'} 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
