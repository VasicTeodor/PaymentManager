import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { BankformComponent } from "./bankform/bankform.component";

const routes: Routes = [
  {
    path: ":orderId",
    component: BankformComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
