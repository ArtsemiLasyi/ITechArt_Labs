import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { CinemaServiceModel } from "../Models/CinemaServiceModel";
import { CinemaServiceRequest } from "../Requests/CinemaServiceRequest";

@Injectable()
export class CinemaServiceService {
    
    constructor(private http : HttpClient){ }

    addCinemaService(cinemaId : number, request : CinemaServiceRequest) {         
        return this.http.post(
            `${ApiUrls.Cinemas}/${cinemaId}${UrlSegments.Services}`,
            request
        ); 
    }

    editCinemaService(
        serviceId : number,
        cinemaId : number,
        request : CinemaServiceRequest
    ) {         
        return this.http.put(
            `${ApiUrls.Cinemas}/${cinemaId}${UrlSegments.Services}/${serviceId}`,
            request
        ); 
    }

    getCinemaServices(cinemaId : number) {
        return this.http.get<CinemaServiceModel[]>(
            `${ApiUrls.Cinemas}/${cinemaId}${UrlSegments.Services}`
        );
    }

    getCinemaService(serviceId : number, cinemaId : number) {
        return this.http.get<CinemaServiceModel>(
            `${ApiUrls.Cinemas}/${cinemaId}${UrlSegments.Services}/${serviceId}`
        );
    }
}