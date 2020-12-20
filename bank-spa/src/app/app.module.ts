import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { BankformComponent } from "./bankform/bankform.component";
import { BankService } from "src/service/bank.service";

@NgModule({
  declarations: [AppComponent, BankformComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule],
  providers: [BankService],
  bootstrap: [AppComponent],
})
export class AppModule {}
