import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = environment.apiUrl + 'api/authorization/';
jwtHelper = new JwtHelperService();
decodedToken: any;
currentUser: any;
userRoles: any;

constructor(private http: HttpClient) { }

register(user: any) {
  return this.http.post(this.baseUrl + 'register', user);
}

login(model: any) {
  return this.http.post(this.baseUrl + 'login', model).pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
        localStorage.setItem('user', JSON.stringify(user.user));
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        this.currentUser = user.user;
        this.userRoles = this.decodedToken.role as Array<string>;
      }
    })
  );
}

loggedIn() {
  const token = localStorage.getItem('token');
  const user = localStorage.getItem('user');
  if (token !== null && user !== null) {
    this.decodedToken = this.jwtHelper.decodeToken(token);
    this.currentUser = JSON.parse(user);
    this.userRoles = this.decodedToken.role as Array<string>;
  }

  return token !== null ? !this.jwtHelper.isTokenExpired(token) : false;
}

isAdmin() {
  if (this.userRoles != null) {
    const result = this.userRoles.includes('Admin');
    return result;
  }
  return false;
}

isUser() {
  if (this.userRoles != null) {
    const result = this.userRoles.includes('User');
    return result;
  }
  return false;
}
}
