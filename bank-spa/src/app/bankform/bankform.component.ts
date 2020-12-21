import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { BankService } from "src/service/bank.service";
import { FormControl } from "@angular/forms";
import {
  MomentDateAdapter,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from "@angular/material-moment-adapter";
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
} from "@angular/material/core";
import { MatDatepicker } from "@angular/material/datepicker";
import * as _moment from "moment";
import { default as _rollupMoment, Moment } from "moment";

const moment = _rollupMoment || _moment;

export const MY_FORMATS = {
  parse: {
    dateInput: "MM/YYYY",
  },
  display: {
    dateInput: "MM/YYYY",
    monthYearLabel: "MMM YYYY",
    dateA11yLabel: "LL",
    monthYearA11yLabel: "MMMM YYYY",
  },
};

@Component({
  selector: "app-bankform",
  templateUrl: "./bankform.component.html",
  styleUrls: ["./bankform.component.css"],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class BankformComponent implements OnInit {
  orderId = "";
  date = new FormControl(moment());

  constructor(
    private bankService: BankService,
    private activatedRoute: ActivatedRoute
  ) {}

  chosenYearHandler(normalizedYear: Moment) {
    const ctrlValue = this.date.value;
    ctrlValue.year(normalizedYear.year());
    this.date.setValue(ctrlValue);
  }

  chosenMonthHandler(
    normalizedMonth: Moment,
    datepicker: MatDatepicker<Moment>
  ) {
    const ctrlValue = this.date.value;
    ctrlValue.month(normalizedMonth.month());
    this.date.setValue(ctrlValue);
    datepicker.close();
  }

  ngOnInit() {
    this.orderId = this.activatedRoute.snapshot.paramMap.get("orderId");
    console.log(this.orderId);
  }

  submitForm(form: NgForm) {
    let send = { ...form.value, validTo: this.date.value.toDate() };

    //console.log(send);
    console.log(this.date.value.toDate());
    if (!form.valid || !this.date.valid) {
      console.log("nije validna forma");
      return false;
    } else {
      this.bankService.confirmPayment(send, this.orderId).subscribe(
        (data) => {
          form.reset();
          console.log(data);
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
}
