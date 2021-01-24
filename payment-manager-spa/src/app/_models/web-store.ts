export class WebStore {
    id: string;
    successUrl: string;
    errorUrl: string;
    failureUrl: string;
    url: string;
    singleMerchantStore: boolean;
    storeName: string;

    constructor(id: string, successUrl: string, errorUrl: string, failureUrl: string, url: string, singleMerchantStore: boolean, storeName: string){
        this.errorUrl = errorUrl;
        this.failureUrl = failureUrl;
        this.singleMerchantStore = singleMerchantStore;
        this.successUrl = successUrl;
        this.url = url;
        this.id = id;
        this.storeName = storeName;
    }
}
