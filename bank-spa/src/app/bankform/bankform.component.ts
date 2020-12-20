import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { BankService } from "src/service/bank.service";

@Component({
  selector: "app-bankform",
  templateUrl: "./bankform.component.html",
  styleUrls: ["./bankform.component.css"],
})
export class BankformComponent implements OnInit {
  isSubmitted = false;
  transaction = "";

  constructor(
    private bankservice: BankService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.transaction = this.activatedRoute.snapshot.paramMap.get("transaction");
    console.log(this.transaction);
  }

  submitForm(form: NgForm) {
    this.isSubmitted = true;
    if (!form.valid) {
      return false;
    } else {
    }
  }
}
