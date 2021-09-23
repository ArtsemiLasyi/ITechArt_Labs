import { Component, HostListener, OnInit } from '@angular/core';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CurrencyRequest } from 'src/app/Requests/CurrencyRequest';
import { CurrencyService } from 'src/app/Services/CurrencyService';
@Component({
    selector: 'admin-currrency-add',
    templateUrl: './admin-currency-add.component.html',
    providers: [CurrencyService]
})
export class AdminCurrencyAddComponent {

    model : CurrencyModel = new CurrencyModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    constructor(private currencyService : CurrencyService) { }

    @HostListener('document:click', ['$event'])
    click(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }

    addCurrency() {
        const request = new CurrencyRequest(this.model.name);
        this.currencyService.addCurrency(request).subscribe(
            () => {
                this.success.flag = true;
            },
            (error : Error) => {
                this.error.exists = true;
            }
        )
    }

    @HostListener('document:click', ['$event'])
    documentClick(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}