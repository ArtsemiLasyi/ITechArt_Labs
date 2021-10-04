import { PriceModel } from './PriceModel';

export class OrderModel {
    id : number = 0;
    userId : number = 0;
    sessionId : number = 0;
    price : PriceModel = new PriceModel();
    sessionStart : Date = new Date();
    cinemaId : number = 0;
    cinemaName : string = '';
    hallId : number = 0;
    hallName : string = '';
}