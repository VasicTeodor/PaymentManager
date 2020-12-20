import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: "root",
})
export class BankService {
  url: String = "http://localhost:8091";

  constructor(private http: HttpClient) {}

  // confirmed(formVal,transaction): Observable<any>{
  //   return this.http.post(`${this.url}/payment/${transaction}`, formVal,{
  //     headers: new HttpHeaders({
  //       'Content-Type': 'application/json',
  //       'responseType': 'text'
  //     })
  //   });
  // }
}
