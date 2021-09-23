import { Component, HostListener, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { GlobalErrorHandler } from 'src/app/ErrorHandlers/GlobalErrorHandler';
import { CinemaServiceModel } from 'src/app/Models/CinemaServiceModel';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { ServiceModel } from 'src/app/Models/ServiceModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';
import { CurrencyService } from 'src/app/Services/CurrencyService';
import { ServiceService } from 'src/app/Services/ServiceService';

@Component({
    selector : 'admin-cinema-service-info',
    templateUrl : './admin-cinema-service-info.component.html',
    providers : [CinemaServiceService]
})
export class AdminCinemaServiceInfoComponent implements OnInit  {

    @Input() cinemaId = 0;
    model : CinemaServiceModel = new CinemaServiceModel();

    currencies : Observable<CurrencyModel[]> | undefined;
    services : Observable<ServiceModel[]> | undefined;

    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    constructor(
        private cinemaServiceService : CinemaServiceService,
        private currencyService : CurrencyService,
        private serviceService : ServiceService,
        private activateRoute : ActivatedRoute,
        private handler : GlobalErrorHandler
    ) { }

    ngOnInit(): void {
        this.model.serviceId = this.activateRoute.snapshot.params['id'];
        this.model.cinemaId = this.activateRoute.snapshot.params['cinemaId'];
        this.cinemaServiceService
            .getCinemaService(this.model.serviceId, this.model.cinemaId)
            .subscribe(
                (data : CinemaServiceModel) => {
                    this.model = data;
                },
                (error : Error) => {
                    this.handler.handleError(error);
                }
            )
    }

    @HostListener('document:click', ['$event'])
    click(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }

    getCurrencies() {
        this.currencies = this.currencyService.getCurrencies()
    }

    getServices() {
        this.services = this.serviceService.getServices()
    }

    selectCurrency(currency : CurrencyModel) {
        this.model.price.currency = currency;
    }

    selectService(service : ServiceModel) {
        this.model.serviceId = service.id;
        this.model.name = service.name;
    }

    editCinemaService() {
        
    }
}