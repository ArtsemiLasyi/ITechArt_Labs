import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ServiceModel } from 'src/app/Models/ServiceModel';
import { ServiceService } from 'src/app/Services/ServiceService';

@Component({
    selector: 'admin-service-search',
    templateUrl: './admin-service-search.component.html',
    providers: []
})
export class AdminServiceSearchComponent {

    services : Observable<ServiceModel[]> | undefined

    constructor (
        private serviceService: ServiceService
    ) { }

    getServices() {
        this.services = this.serviceService.getServices()
    }

    ngOnInit() {
        this.getServices();
    }
}
