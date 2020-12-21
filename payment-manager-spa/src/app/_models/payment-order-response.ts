export class PaymentOrderResponse {
    paymentUrl: string;
    paymentId: string;

    constructor(paymentUrl: string, paymentId: string) {
        this.paymentId = paymentId;
        this.paymentUrl = paymentUrl;
    }
}
