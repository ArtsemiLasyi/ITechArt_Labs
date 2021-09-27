import { CurrencyModel } from "./CurrencyModel";

export class PriceModel {
    value : number = 0;
    currency : CurrencyModel = new CurrencyModel();
}