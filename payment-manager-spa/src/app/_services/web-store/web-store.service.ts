import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WebStore } from 'src/app/_models/web-store';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';
import * as constants from '../../_helpers/constants';

@Injectable({
  providedIn: 'root'
})
export class WebStoreService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient, private authService: AuthService) { }

  getWebStores(pageNumber?: number, pageSize?: number) : Observable<WebStore[]>{
    pageNumber = pageNumber ? pageNumber : constants.paginationConstants.pageNumber;
    pageSize = pageSize ? pageSize : constants.paginationConstants.pageSize;
    
    const params = new HttpParams();
    params.append('pageNumber', JSON.stringify(pageNumber));
    params.append('pageSize', JSON.stringify(pageSize));
  
    return this.http.get<WebStore[]>(this.baseUrl + 'api/webstore/getwebstores', {params});
  }
  
  getWebStore(id: string) : Observable<WebStore>{
    return this.http.get<WebStore>(this.baseUrl + 'api/webstore/getwebstore?id=' + id);
  }
  
  removeWebStore(id: string) : Observable<WebStore>{
    return this.http.delete<WebStore>(this.baseUrl + 'api/webstore/removewebstore?id=' + id);
  }
  
  editWebStore(id: string, webStore: WebStore) : Observable<WebStore>{
    return this.http.put<WebStore>(this.baseUrl + 'api/webstore/updatewebstore?id=' + id, webStore );
  }
  
  addWebStore(webStore: WebStore) : Observable<WebStore>{
    return this.http.post<WebStore>(this.baseUrl + 'api/webstore/addnewwebstore', webStore);
  }
}
