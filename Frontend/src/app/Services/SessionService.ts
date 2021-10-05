import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SessionModel } from "../Models/SessionModel";
import { SessionRequest } from "../Requests/SessionRequest";
import { SessionSearchRequest } from "../Requests/SessionSearchRequest";

@Injectable()
export class SessionService {
    
    constructor(private http : HttpClient){ }

    addSession(session : SessionRequest) {         
        return this.http.post(ApiUrls.Sessions, session); 
    }

    editSession(id : number, session : SessionRequest) {         
        return this.http.put(
            `${ApiUrls.Sessions}/${id}`,
            session
        ); 
    }

    getSessions(cinemaId : number, request : SessionSearchRequest) {
        return this.http.get<SessionModel[]>(
            `${ApiUrls.Cinemas}/${cinemaId}${UrlSegments.Sessions}`, {
                params : this.getParams(request)
            }
        );
    }

    getSession(id : number) {
        return this.http.get<SessionModel>(`${ApiUrls.Sessions}/${id}`);
    }

    private getParams(request : SessionSearchRequest) {
        let params : any = { };
        if (request.firstSessionDateTime) {
            params.firstSessionDateTime = request.firstSessionDateTime;
        }
        if (request.lastSessionDateTime) {
            params.lastSessionDateTime = request.lastSessionDateTime;
        }
        if (request.freeSeatsNumber) {
            params.freeSeatsNumber = request.freeSeatsNumber;
        }
        return params;
    }
}