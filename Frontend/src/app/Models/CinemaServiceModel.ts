import { PriceModel } from "./PriceModel";

export class CinemaServiceModel {
    cinemaId : number = 0;
    serviceId : number = 0;
    price : PriceModel = new PriceModel();
    name : string = '';
}