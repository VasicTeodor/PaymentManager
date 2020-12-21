import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PayPalFailedComponent } from './_components-user/pay-pal-failed/pay-pal-failed.component';
import { PayPalResultComponent } from './_components-user/pay-pal-result/pay-pal-result.component';
import { PayPalComponent } from './_components-user/pay-pal/pay-pal.component';
import { PaymentOptionsComponent } from './_components-user/payment-options/payment-options.component';
import { LoginComponent } from './_components/login/login.component';
import { PaymentServiceComponent } from './_components/payment-service/payment-service.component';
import { SideMenuComponent } from './_components/side-menu/side-menu.component';
import { WebShopComponent } from './_components/web-shop/web-shop.component';
import { PaymentServicesResolver } from './_resolvers/payment-services.resolver';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {path: 'paymentoptions', component: PaymentOptionsComponent},
  {path: 'paypal/:orderId', component: PayPalComponent},
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
