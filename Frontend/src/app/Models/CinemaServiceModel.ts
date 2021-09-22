import { PriceModel } from "./PriceModel";

export class CinemaServiceModel {
    cinemaId : number;
    serviceId : number;
    price : PriceModel;

    constructor(
        cinemaId : number,
        serviceId : number,
        price : PriceModel
    ) {
        this.cinemaId = cinemaId;
        this.serviceId = serviceId;
        this.price = price;    
    }
}