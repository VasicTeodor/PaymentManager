import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './_components/login/login.component';
import { PaymentServiceComponent } from './_components/payment-service/payment-service.component';
import { SideMenuComponent } from './_components/side-menu/side-menu.component';
import { WebShopComponent } from './_components/web-shop/web-shop.component';

const routes: Routes = [
  {path: '', component: LoginComponent},
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
