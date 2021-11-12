import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SeatOrderModel } from "../Models/SeatOrderModel";
@Injectable()
export class SeatOrderService {

    constructor(private http : HttpClient){ }

    getSeatOrders(orderId : number) {
        return this.http.get<SeatOrderModel[]>(
            `${ApiUrls.Orders}/${orderId}${UrlSegments.Seats}`
        );
    }

    getSeatOrder(seatId : number, orderId : number) {
        return this.http.get<SeatOrderModel[]>(
            `${ApiUrls.Orders}/${orderId}${UrlSegments.Seats}/${seatId}`
        );
    }
}