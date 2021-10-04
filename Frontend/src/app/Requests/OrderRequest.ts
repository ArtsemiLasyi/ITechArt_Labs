import { CinemaServiceRequest } from "./CinemaServiceRequest";
import { SessionSeatsRequest } from "./SessionSeatsRequest";

export class OrderRequest {

    userId : number;
    sessionId : number;
    registratedAt : Date;
    sessionSeats : SessionSeatsRequest;
    cinemaServices : CinemaServiceRequest[];

    constructor(
        userId : number,
        sessionId : number,
        registratedAt : Date,
        sessionSeats : SessionSeatsRequest,
        cinemaServices : CinemaServiceRequest[]
    ) {
        this.userId = userId;
        this.sessionId = sessionId;
        this.registratedAt = registratedAt;
        this.sessionSeats = sessionSeats;
        this.cinemaServices = cinemaServices;
    }

}