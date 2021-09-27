import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { CurrencyModel } from "../Models/CurrencyModel";
import { CurrencyRequest } from "../Requests/CurrencyRequest";

@Injectable()
export class CurrencyService {

    constructor(private http : HttpClient) { }

    getCurrencies() {
        return this.http.get<CurrencyModel[]>(ApiUrls.Currencies);
    }

    addCurrency(currency : CurrencyRequest) {         
        return this.http.post(ApiUrls.Currencies, currency); 
    }

    editCurrency(id : number, currency : CurrencyRequest) {         
        return this.http.put(
            `${ApiUrls.Currencies}/${id}`,
            currency
        ); 
    }

    getCurrency(id : number) {
        return this.http.get<CurrencyModel>(`${ApiUrls.Currencies}/${id}`);
    }

    deleteCurrency(id : number) {
        return this.http.delete(`${ApiUrls.Currencies}/${id}`);
    }
}