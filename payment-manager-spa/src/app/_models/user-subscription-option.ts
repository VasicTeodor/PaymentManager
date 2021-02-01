export class UserSubscriptionOption {
    name: string;
    description: string;
    expressCheckoutUrl: string;

    constructor(name: string, description: string, expressCheckoutUrl: string){
        this.name = name;
        this.description = description;
        this.expressCheckoutUrl = expressCheckoutUrl;
    }
}
