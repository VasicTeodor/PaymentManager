export class WebStore {
    successUrl: string;
    errorUrl: string;
    failureUrl: string;
    url: string;
    singleMerchantStore: boolean;

    constructor(successUrl: string, errorUrl: string, failureUrl: string, url: string, singleMerchantStore: boolean){
        this.errorUrl = errorUrl;
        this.failureUrl = failureUrl;
        this.singleMerchantStore = singleMerchantStore;
        this.successUrl = successUrl;
        this.url = url;
    }
}
