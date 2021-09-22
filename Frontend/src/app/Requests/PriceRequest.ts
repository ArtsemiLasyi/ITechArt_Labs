import { CurrencyRequest } from "./CurrencyRequest";

export class PriceRequest {
    value : number;
    currency : CurrencyRequest;

    constructor(value : number, currency : CurrencyRequest) {
        this.value = value;
        this.currency = currency;
    }
}