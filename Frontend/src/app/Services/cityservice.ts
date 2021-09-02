import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { CityModel } from "../Models/CityModel";

@Injectable()
export class CityService {

    constructor(private http : HttpClient){ }

    getCities(name : string) {
        return this.http.get<CityModel[]>(
            ApiUrls.Cities, {
                params : {
                    cityName : name
                }
            }
        );
    }
}