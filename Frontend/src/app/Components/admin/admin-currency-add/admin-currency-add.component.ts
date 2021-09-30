import { Component, OnInit } from '@angular/core';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CurrencyRequest } from 'src/app/Requests/CurrencyRequest';
import { CurrencyService } from 'src/app/Services/CurrencyService';
@Component({
    selector: 'admin-currency-add',
    templateUrl: './admin-currency-add.component.html',
    providers: [CurrencyService]
})
export class AdminCurrencyAddComponent {

    model : CurrencyModel = new CurrencyModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    constructor(private currencyService : CurrencyService) { }

    addCurrency() {
        const request = new CurrencyRequest(this.model.name);
        this.currencyService.addCurrency(request).subscribe(
            () => {
                this.success.flag = true;
            },
            (error) => {
                this.error.exists = true;
                this.error.text = error;
            }
        )
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}