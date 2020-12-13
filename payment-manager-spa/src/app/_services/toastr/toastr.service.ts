import { Injectable } from '@angular/core';
declare var toastr: any;

@Injectable({
  providedIn: 'root'
})
export class ToastrService {

constructor() { }
  errorMessage(message: string) {
    toastr.error(message, '', { timeOut: 0, positionClass: 'toast-bottom-right' });
  }
  
  warningMessage(message: string) {
    toastr.warning(message, '', { positionClass: 'toast-bottom-right' });
  }
  
  infoMessage(message: string) {
    toastr.info(message, '', { positionClass: 'toast-bottom-right'});
  }
  
  successMessage(message: string) {
    toastr.success(message, '', { positionClass: 'toast-bottom-right'});
  }
}
