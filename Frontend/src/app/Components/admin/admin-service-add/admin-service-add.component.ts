import { Component, OnInit } from '@angular/core';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { ServiceModel } from 'src/app/Models/ServiceModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { ServiceRequest } from 'src/app/Requests/ServiceRequest';
import { ServiceService } from 'src/app/Services/ServiceService';
@Component({
    selector: 'admin-service-add',
    templateUrl: './admin-service-add.component.html',
    providers: [ServiceService]
})
export class AdminServiceAddComponent {

    model : ServiceModel = new ServiceModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    constructor(private serviceService : ServiceService) { }

    addService() {
        const request = new ServiceRequest(this.model.name);
        this.serviceService.addService(request).subscribe(
            () => {
                this.success.flag = true;
            },
            (error : Error) => {
                this.error.exists = true;
            }
        )
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}