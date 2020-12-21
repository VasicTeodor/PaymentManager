export class PaymentOrderDetails {
    id: string;
    merchantId: string;
    amount: number;
    merchantOrderId: string;

    constructor(id: string, merchantId: string, amount: number, merchantOrderId: string) {
        this.id = id;
        this.merchantId = merchantId;
        this.merchantOrderId = merchantOrderId;
        this.amount = amount;
    }
}
