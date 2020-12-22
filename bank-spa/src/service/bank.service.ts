import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class BankService {
  url: String = "https://localhost:5005/bank";

  constructor(private http: HttpClient) {}

  confirmPayment(formVal, orderId): Observable<any> {
    return this.http.post(
      `${this.url}/Payment/FrontPayment?Id=${orderId}`,
      formVal,
      {
        headers: new HttpHeaders({
          "Content-Type": "application/json",
        }),
      }
    );
  }
}
