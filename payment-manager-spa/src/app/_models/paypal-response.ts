import { PayPalLink } from "./pay-pal-link";

export interface PaypalResponse {
    id: string;
    state: string;
    links: Array<PayPalLink>;
 }