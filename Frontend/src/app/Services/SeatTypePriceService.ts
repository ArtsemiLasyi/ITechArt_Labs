import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SeatTypePriceModel } from "../Models/SeatTypePriceModel";
import { SeatTypePriceRequest } from "../Requests/SeatTypePriceRequest";

@Injectable()
export class SeatTypePriceService {
    
    constructor(private http : HttpClient){ }

    addSeatTypePrice(
        sessionId : number,
        seatTypeId : number,
        request : SeatTypePriceRequest
    ) {         
        return this.http.post(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.SeatTypes}/${seatTypeId}`,
            request
        ); 
    }

    editSeatTypePrice(
        sessionId : number,
        seatTypeId : number,
        request : SeatTypePriceRequest
    ) {         
        return this.http.put(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.SeatTypes}/${seatTypeId}`,
            request
        ); 
    }

    getSeatTypePrices(sessionId : number) {
        return this.http.get<SeatTypePriceModel[]>(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.SeatTypes}`
        );
    }

    getSeatTypePrice(sessionId : number, seatTypeId : number) {
        return this.http.get<SeatTypePriceModel>(
            `${ApiUrls.Sessions}/${sessionId}${UrlSegments.Services}/${seatTypeId}`
        );
    }
}