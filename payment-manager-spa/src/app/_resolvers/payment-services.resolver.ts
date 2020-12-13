import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { PaymentService } from "../_models/payment-service";
import { PaymentServicesService } from "../_services/payment-services/payment-services.service";
import { ToastrService } from "../_services/toastr/toastr.service";

@Injectable()
export class PaymentServicesResolver implements Resolve<PaymentService[] | null> {

    constructor(private router: Router, private toastr: ToastrService, private paymentServicesService: PaymentServicesService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<PaymentService[] | null> {
        return this.paymentServicesService.getPaymentServices().pipe(
            catchError(error => {
                this.toastr.errorMessage('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}