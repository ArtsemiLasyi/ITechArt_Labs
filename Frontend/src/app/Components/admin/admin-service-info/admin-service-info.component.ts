import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ServiceModel } from 'src/app/Models/ServiceModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { ServiceRequest } from 'src/app/Requests/ServiceRequest';
import { ServiceService } from 'src/app/Services/ServiceService';

@Component({
    selector: 'admin-service-info',
    templateUrl: './admin-service-info.component.html',
    providers: [ServiceService]
})
export class AdminServiceInfoComponent implements OnInit {

    model = new ServiceModel();
    error = new ErrorModel();
    success = new SuccessModel();

    constructor (
        private serviceService: ServiceService,
        private activateRoute: ActivatedRoute
    ) { }

    ngOnInit() {
        this.model.id = this.activateRoute.snapshot.params['id'];
        this.serviceService
            .getService(this.model.id)
            .subscribe(
                (service : ServiceModel) => {
                    this.model = service;
                }
            )
    }

    editService() {
        let request = new ServiceRequest(
            this.model.name
        );
        this.serviceService
            .editService(this.model.id, request)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error : Error) => {
                    this.error.exists = true;
                }
            );
    }

    deleteService() {
        this.serviceService
            .deleteService(this.model.id)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error : Error) => {
                    this.error.exists = true;
                }
            );
    }
}