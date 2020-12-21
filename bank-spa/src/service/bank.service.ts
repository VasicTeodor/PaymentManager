import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class BankService {
  url: String = "http://localhost:10662";

  constructor(private http: HttpClient) {}

  confirmPayment(formVal, orderId): Observable<any> {
    return this.http.post(
      `${this.url}/payment/FrontPayment/${orderId}`,
      formVal,
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
        }),
      }
    );
  }
}
