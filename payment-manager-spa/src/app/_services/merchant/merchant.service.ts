import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import * as constants from '../../_helpers/constants';
import { Observable } from 'rxjs';
import { Merchant } from 'src/app/_models/merchant';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MerchantService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getMerchants(pageNumber?: number, pageSize?: number) : Observable<Merchant[]>{
    pageNumber = pageNumber ? pageNumber : constants.paginationConstants.pageNumber;
    pageSize = pageSize ? pageSize : constants.paginationConstants.pageSize;
    
    const params = new HttpParams();
    params.append('pageNumber', JSON.stringify(pageNumber));
    params.append('pageSize', JSON.stringify(pageSize));
  
    return this.http.get<Merchant[]>(this.baseUrl + 'api/merchant/getmerchants', {params});
  }
  
  getMerchant(id: string) : Observable<Merchant>{
    return this.http.get<Merchant>(this.baseUrl + 'api/merchant/getmerchant?id=' + id);
  }
  
  removeMerchant(id: string) : Observable<Merchant>{
    return this.http.delete<Merchant>(this.baseUrl + 'api/merchant/removemerchant?id=' + id);
  }
  
  editMerchant(id: string, merchant: Merchant) : Observable<Merchant>{
    return this.http.put<Merchant>(this.baseUrl + 'api/merchant/editmerchant?id=' + id, merchant );
  }
  
  addMerchant(merchant: any) : Observable<any>{
    return this.http.post<any>(this.baseUrl + 'api/merchant/addmerchant', merchant);
  }
}
