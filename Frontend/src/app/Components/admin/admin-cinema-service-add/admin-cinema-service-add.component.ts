import { Component, HostListener, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CinemaServiceModel } from 'src/app/Models/CinemaServiceModel';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { ServiceModel } from 'src/app/Models/ServiceModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CinemaServiceRequest } from 'src/app/Requests/CinemaServiceRequest';
import { PriceRequest } from 'src/app/Requests/PriceRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';
import { CurrencyService } from 'src/app/Services/CurrencyService';
import { ServiceService } from 'src/app/Services/ServiceService';

@Component({
    selector : 'admin-cinema-service-add',
    templateUrl : './admin-cinema-service-add.component.html',
    providers : [CinemaServiceService]
})
export class AdminCinemaServiceAddComponent implements OnInit {

    @Input() cinemaId = 0;
    model : CinemaServiceModel = new CinemaServiceModel();

    currencies : Observable<CurrencyModel[]> | undefined;
    services : Observable<ServiceModel[]> | undefined;

    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    constructor(
        private cinemaServiceService : CinemaServiceService,
        private serviceService : ServiceService,
        private currencyService : CurrencyService
    ) { }

    ngOnInit() {
        this.model.cinemaId = this.cinemaId;
    }

    clearForm() {
        this.success.flag = false;
        this.error.exists = false;
    }

    getCurrencies() {
        this.currencies = this.currencyService.getCurrencies();
        this.clearForm();
    }

    getServices() {
        this.services = this.serviceService.getServices();
        this.clearForm();
    }

    addCinemaService() {
        let request = new CinemaServiceRequest(
            this.model.cinemaId,
            this.model.serviceId,
            new PriceRequest(
                this.model.price.value,
                this.model.price.currency.id
            )
        );
        this.cinemaServiceService
            .addCinemaService(this.cinemaId, request)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error) => {
                    this.error.exists = true;
                    this.error.text = error;
                }
            );
    }

    selectCurrency(currency : CurrencyModel) {
        this.model.price.currency = currency;
    }

    selectService(service : ServiceModel) {
        this.model.serviceId = service.id;
        this.model.name = service.name;
    }
}