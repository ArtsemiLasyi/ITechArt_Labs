import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { ServiceModel } from "../Models/ServiceModel";
import { ServiceRequest } from "../Requests/ServiceRequest";

@Injectable()
export class ServiceService {

    constructor(private http : HttpClient) { }

    getServices() {
        return this.http.get<ServiceModel[]>(ApiUrls.Services);
    }

    addService(service : ServiceRequest) {         
        return this.http.post(ApiUrls.Services, service); 
    }

    editService(id : number, service : ServiceRequest) {         
        return this.http.put(
            `${ApiUrls.Services}/${id}`,
            service
        ); 
    }

    getService(id : number) {
        return this.http.get<ServiceModel>(`${ApiUrls.Services}/${id}`);
    }

    deleteService(id : number) {
        return this.http.delete(`${ApiUrls.Services}/${id}`);
    }
}