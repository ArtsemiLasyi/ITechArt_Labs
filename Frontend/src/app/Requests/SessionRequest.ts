import { SeatTypePriceRequest } from "./SeatTypePriceRequest";

export class SessionRequest {
    filmId : number;
    hallId : number;
    startDateTime : Date;
    seatTypePrices : SeatTypePriceRequest[];
    
    constructor(
        filmId : number,
        hallId : number,
        startDateTime : Date,
        seatTypePrices : SeatTypePriceRequest[]
    ) {
        this.filmId = filmId;
        this.hallId = hallId;
        this.startDateTime = startDateTime;
        this.seatTypePrices = seatTypePrices;
    }
}