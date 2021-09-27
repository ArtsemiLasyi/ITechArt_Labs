import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SeatsModel } from "../Models/SeatsModel";
import { SeatRequest } from "../Requests/SeatRequest";
import { SeatsRequest } from "../Requests/SeatsRequest";

@Injectable()
export class SeatService {

    constructor(private http : HttpClient){ }

    addSeats(hallId : number, request : SeatsRequest) {         
        return this.http.post(
            `${ApiUrls.Halls}/${hallId}${UrlSegments.Seats}`,
            request
        ); 
    }

    editSeats(hallId : number, request : SeatsRequest) {         
        return this.http.put(
            `${ApiUrls.Halls}/${hallId}${UrlSegments.Seats}`,
            request
        ); 
    }

    getSeats(hallId : number) {
        return this.http.get<SeatsModel>(
            `${ApiUrls.Halls}/${hallId}${UrlSegments.Seats}`
        );
    }
}