import { PriceRequest } from "./PriceRequest";

export class SeatTypePriceRequest {
    sessionId : number;
    seatTypeId : number;
    price : PriceRequest;

    constructor(
        sessionId : number,
        seatTypeId : number,
        price : PriceRequest
    ) {
        this.sessionId = sessionId;
        this.seatTypeId = seatTypeId;
        this.price = price;
    }
}