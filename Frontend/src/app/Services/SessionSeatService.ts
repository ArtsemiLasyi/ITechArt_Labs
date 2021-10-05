import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SessionSeatModel } from "../Models/SessionSeatModel";
import { SessionSeatsModel } from "../Models/SessionSeatsModel";
import { SessionSeatRequest } from "../Requests/SessionSeatRequest";
import { SessionSeatsRequest } from "../Requests/SessionSeatsRequest";

@Injectable()
export class SessionSeatService {

    constructor(private http : HttpClient){ }

    take(sessionId : number, seatId : number, request : SessionSeatRequest) {         
        return this.http.put(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Seats}/${seatId}`,
            request, {
                params : {
                    take : true
                }
            }
        ); 
    }

    free(sessionId : number, seatId : number, request : SessionSeatRequest) {         
        return this.http.put(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Seats}/${seatId}`,
            request, {
                params : {
                    free : true
                }
            }
        ); 
    }

    update(sessionId : number, request : SessionSeatsRequest) {         
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

    getSessionSeat(sessionId : number, seatId : number) {
        return this.http.get<SessionSeatModel>(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Seats}/${seatId}`
        );
    }
}