import { CurrencyModel } from "../Models/CurrencyModel";
import { CurrencyRequest } from "./CurrencyRequest";

export class PriceRequest {
    value : number;
    currencyId : number;

    constructor(value : number, currencyId : number) {
        this.value = value;
        this.currencyId = currencyId;
    }
}