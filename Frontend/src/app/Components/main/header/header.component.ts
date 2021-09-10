import { Component } from '@angular/core';
import { NgModel } from '@angular/forms';
import { props, Store } from '@ngrx/store';
import { saveCity } from 'src/app/Actions/city.actions';
import { StorageKeyNames } from 'src/app/Constants/StorageKeyNames';
import { CityModel } from 'src/app/Models/CityModel';
import { CityService } from 'src/app/Services/CityService';

@Component({
    selector : 'app-header',
    templateUrl : './header.component.html',
    styleUrls : ['./header.component.scss']
})
export class HeaderComponent { 
    cities: CityModel[] = [];
    model = new CityModel();
    activeCityName : string = "No city selected"; 
    cityName : string = "";

    constructor (
        private cityService : CityService,
        private store : Store<{ city : CityModel }>
    ) { 
        let name = sessionStorage.getItem(StorageKeyNames.CURRENT_CITY_NAME);
        let id = sessionStorage.getItem(StorageKeyNames.CURRENT_CITY_ID);
        if (id && name) {
            this.model.name = name;
            this.model.id = Number.parseInt(id);
            this.setCity(this.model);
        }
    }

    getCities() {
        this.cityService
            .getCities(this.cityName)
            .subscribe(
                (data) =>  {
                    this.cities = data;
                }
            )
    }

    setCity(city : CityModel) {
        this.activeCityName = city.name;
        this.cityName = "";
        this.model = city;
        this.cities = [];
        this.store.dispatch(saveCity({city}));
        sessionStorage.setItem(StorageKeyNames.CURRENT_CITY_NAME, city.name);
        sessionStorage.setItem(StorageKeyNames.CURRENT_CITY_ID, city.id.toString());
    }
}
