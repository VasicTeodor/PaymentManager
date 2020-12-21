export class UserPaymentService {
    name: string;
    url: string;
    description: string;
    paymentManagerUrl: string;

    constructor(name: string, url: string, description: string, paymentManagerUrl: string)
    {
        this.name = name;
        this.url = url;
        this.paymentManagerUrl = paymentManagerUrl;
        this.description = description;
    }
}
