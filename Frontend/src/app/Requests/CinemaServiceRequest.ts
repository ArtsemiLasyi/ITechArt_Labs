import { PriceRequest } from "./PriceRequest";

export class CinemaServiceRequest {
    cinemaId : number;
    serviceId : number;
    price : PriceRequest;

    constructor(
        cinemaId : number,
        serviceId : number,
        price : PriceRequest
    ) {
        this.cinemaId = cinemaId;
        this.serviceId = serviceId;
        this.price = price;    
    }
}