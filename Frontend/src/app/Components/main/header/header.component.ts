import { Component } from '@angular/core';
import { CityModel } from 'src/app/Models/CityModel';
import { CityService } from 'src/app/Services/cityservice';

@Component({
    selector : 'app-header',
    templateUrl : './header.component.html',
    styleUrls : ['./header.component.scss']
})
export class HeaderComponent { 
    cities: CityModel[] = [];
    activeCityName : string = "No city selected"; 
    cityName : string = "";

    constructor (
        private cityService: CityService
    ) { }

    getCities(cityName : string) {
        this.cityService
            .getCities(cityName)
            .subscribe(
                (data) =>  {
                    this.cities = data;
                }
            )
    }

    setCity(cityName : string) {
        this.activeCityName = cityName;
        this.cityName = "";
    }
}
