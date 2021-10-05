import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SessionSeatsModel } from "../Models/SessionSeatsModel";
import { SessionSeatsRequest } from "../Requests/SessionSeatsRequest";

@Injectable()
export class SessionSeatService {

    constructor(private http : HttpClient){ }

    addSessionSeats(sessionId : number, request : SessionSeatsRequest) {         
        return this.http.post(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Seats}`,
            request
        ); 
    }

    editSessionSeats(sessionId : number, request : SessionSeatsRequest) {         
        return this.http.put(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Seats}`,
            request
        ); 
    }

    getSessionSeats(sessionId : number) {
        return this.http.get<SessionSeatsModel>(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Seats}`
        );
    }
}