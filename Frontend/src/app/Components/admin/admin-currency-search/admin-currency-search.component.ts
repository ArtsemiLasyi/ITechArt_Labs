import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { CinemaService } from 'src/app/Services/CinemaService';
import { CurrencyService } from 'src/app/Services/CurrencyService';

@Component({
    selector : 'admin-currency-search',
    templateUrl : './admin-currency-search.component.html',
    providers : []
})
export class AdminCurrencySearchComponent {

    currencies : Observable<CurrencyModel[]> | undefined

    constructor (
        private currencyService: CurrencyService
    ) { }

    getCurrencies() {
        this.currencies = this.currencyService.getCurrencies()
    }

    ngOnInit() {
        this.getCurrencies();
    }
}
