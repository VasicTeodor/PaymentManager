import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserSubscriptionOption } from 'src/app/_models/user-subscription-option';
import { UserService } from 'src/app/_services/user/user.service';

@Component({
  selector: 'app-subscription-options',
  templateUrl: './subscription-options.component.html',
  styleUrls: ['./subscription-options.component.scss']
})
export class SubscriptionOptionsComponent implements OnInit {
  token: string = '';
  userId: string = '';
  webstoreId: string = '';
  subscriptionOptions: UserSubscriptionOption[] | null = null;
  constructor(private activatedRoute: ActivatedRoute, private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params  => {
      this.token = params['token'];
      this.userId = params['orderId'];
      this.webstoreId = params['webStoreId']
      localStorage.setItem('token', this.token);
      this.userService.getSubscriptionOptions(this.webstoreId).subscribe((result: UserSubscriptionOption[]) => {
        console.log('Received user subscriptions', result);
        this.subscriptionOptions = result;
      })
     });
  }

  getSubscription(subscriptionOption: UserSubscriptionOption) {
    if(subscriptionOption.expressCheckoutUrl) {
      window.open(subscriptionOption.expressCheckoutUrl, "_self");
    }
  }
}
