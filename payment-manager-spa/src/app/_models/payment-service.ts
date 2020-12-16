export class PaymentService {
    name: string;
    url: string;
    description: string;
    id: string;

    constructor(name: string, url: string, description: string, id: string)
    {
        this.name = name;
        this.url = url;
        this.id = id;
        this.description = description;
    }
}
