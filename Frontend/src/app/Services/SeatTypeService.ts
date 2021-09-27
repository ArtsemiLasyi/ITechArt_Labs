import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { SeatTypeModel } from "../Models/SeatTypeModel";
import { SeatTypeRequest } from "../Requests/SeatTypeRequest";

@Injectable()
export class SeatTypeService {

    constructor(private http : HttpClient) { }

    getSeatTypes() {
        return this.http.get<SeatTypeModel[]>(ApiUrls.SeatTypes);
    }

    addSeatType(seatType : SeatTypeRequest) {         
        return this.http.post(ApiUrls.SeatTypes, seatType); 
    }

    editSeatType(id : number, seatType : SeatTypeRequest) {         
        return this.http.put(
            `${ApiUrls.SeatTypes}/${id}`,
            seatType
        ); 
    }

    getSeatType(id : number) {
        return this.http.get<SeatTypeModel>(`${ApiUrls.SeatTypes}/${id}`);
    }

    deleteSeatType(id : number) {
        return this.http.delete(`${ApiUrls.SeatTypes}/${id}`);
    }
}