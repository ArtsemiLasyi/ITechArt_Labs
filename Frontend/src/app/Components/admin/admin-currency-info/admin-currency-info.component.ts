import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CurrencyRequest } from 'src/app/Requests/CurrencyRequest';
import { CurrencyService } from 'src/app/Services/CurrencyService';

@Component({
    selector: 'admin-currency-info',
    templateUrl: './admin-currency-info.component.html',
    providers: [CurrencyService]
})
export class AdminCurrencyInfoComponent implements OnInit {

    model = new CurrencyModel();
    error = new ErrorModel();
    success = new SuccessModel();

    disabledButton : boolean = false;

    constructor (
        private router : Router,
        private currencyService: CurrencyService,
        private activateRoute: ActivatedRoute
    ) { }

    ngOnInit() {
        this.model.id = this.activateRoute.snapshot.params['id'];
        this.currencyService
            .getCurrency(this.model.id)
            .subscribe(
                (currency : CurrencyModel) => {
                    this.model = currency;
                }
            )
    }

    editCurrency() {
        let request = new CurrencyRequest(
            this.model.name
        );
        this.currencyService
            .editCurrency(this.model.id, request)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error  : string) => {
                    this.error.exists = true;
                    this.error.text = error;
                }
            );
    }

    deleteCurrency() {
        this.currencyService
            .deleteCurrency(this.model.id)
            .subscribe(
                () => {
                    this.success.flag = true;
                    this.disableButtons();
                    this.router.navigate(
                        ['../../search'], 
                        { relativeTo: this.activateRoute }
                    );
                },
                (error) => {
                    this.error.exists = true;
                    this.error.text = error;
                }
            );
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }

    disableButtons() {
        this.disabledButton = true;
    }
}