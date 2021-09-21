import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { UrlSegments } from "../Constants/UrlSegments";
import { HallModel } from "../Models/HallModel";
import { HallRequest } from "../Requests/HallRequest";

@Injectable()
export class HallService {
    
    constructor(private http : HttpClient){ }

    addHall(hall : HallRequest) {         
        return this.http.post(ApiUrls.Halls, hall); 
    }

    editHall(id : number, hall : HallRequest) {         
        return this.http.put(
            `${ApiUrls.Halls}/${id}`,
            hall
        ); 
    }

    addPhoto(id : number, photo : any) {
        return this.http.post(
            `${ApiUrls.Halls}/${id}/photo`,
            photo
        );
    }

    getPhoto(id : number) : string {
        return `${ApiUrls.Halls}/${id}/photo`;
    }

    getHalls(cinemaId : number) {
        return this.http.get<HallModel[]>(
            `${ApiUrls.Cinemas}/${cinemaId}${UrlSegments.Halls}`
        );
    }

    getHall(id : number) {
        return this.http.get<HallModel>(`${ApiUrls.Halls}/${id}`);
    }

    deleteHall(id : number) {
        return this.http.delete(`${ApiUrls.Halls}/${id}`);
    }
}