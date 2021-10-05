import { CinemaServiceRequest } from "./CinemaServiceRequest";
import { SessionSeatsRequest } from "./SessionSeatsRequest";

export class OrderRequest {

    sessionId : number;
    registratedAt : Date;
    sessionSeats : SessionSeatsRequest;
    cinemaServices : CinemaServiceRequest[];

    constructor(
        sessionId : number,
        registratedAt : Date,
        sessionSeats : SessionSeatsRequest,
        cinemaServices : CinemaServiceRequest[]
    ) {
        this.sessionId = sessionId;
        this.registratedAt = registratedAt;
        this.sessionSeats = sessionSeats;
        this.cinemaServices = cinemaServices;
    }

}