import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { BankformComponent } from "./bankform/bankform.component";

const routes: Routes = [
  {
    path: "banka/card/:transaction",
    component: BankformComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
