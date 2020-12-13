import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth/auth.service';
import { ToastrService } from 'src/app/_services/toastr/toastr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService, private toastr: ToastrService, private router: Router) {}
  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.toastr.successMessage('Logged in succesfully');
      this.router.navigate(['/home']);
    }, error => {
      this.toastr.errorMessage('Failed to login');
    }, () => {
      console.log('This is where magic happens!');
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
