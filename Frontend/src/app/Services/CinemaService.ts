import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { CinemaModel } from "../Models/CinemaModel";
import { CinemaRequest } from "../Requests/CinemaRequest";
import { CinemaSearchRequest } from "../Requests/CinemaSearchRequest";

@Injectable()
export class CinemaService {
    
    constructor(private http : HttpClient){ }

    addCinema(cinema : CinemaRequest) {         
        return this.http.post(ApiUrls.Cinemas, cinema); 
    }

    editCinema(id : number, cinema : CinemaRequest) {         
        return this.http.put(
            `${ApiUrls.Cinemas}/${id}`,
            cinema
        ); 
    }

    addPhoto(id : number, photo : any) {
        return this.http.post(
            `${ApiUrls.Cinemas}/${id}/photo`,
            photo
        );
    }

    getPhoto(id : number) : string {
        return `${ApiUrls.Cinemas}/${id}/photo`;
    }

    getCinemas(cityId : number, request : CinemaSearchRequest) {

        return this.http.get<CinemaModel[]>(
            `${ApiUrls.Cities}/${cityId}${UrlSegments.Cinemas}`, {
                params : this.getParams(request) 
            }
        );
    }

    getCinema(id : number) {
        return this.http.get<CinemaModel>(`${ApiUrls.Cinemas}/${id}`);
    }

    deleteCinema(id : number) {
        return this.http.delete(`${ApiUrls.Cinemas}/${id}`);
    }

    private getParams(request : CinemaSearchRequest) {
        let params : any = { };
        if (request.cinemaName) {
            params.cinemaName = request.cinemaName;
        }
        return params;
    }
}