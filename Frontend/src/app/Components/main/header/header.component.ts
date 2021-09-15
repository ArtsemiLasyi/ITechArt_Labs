import { Component } from '@angular/core';
import { NgModel } from '@angular/forms';
import { props, Store } from '@ngrx/store';
import { saveCity } from 'src/app/Actions/city.actions';
import { StorageKeyNames } from 'src/app/Constants/StorageKeyNames';
import { CityModel } from 'src/app/Models/CityModel';
import { CityService } from 'src/app/Services/CityService';
import { StorageService } from 'src/app/Services/StorageService';

@Component({
    selector : 'app-header',
    templateUrl : './header.component.html',
    styleUrls : ['./header.component.scss']
})
export class HeaderComponent { 
    cities: CityModel[] = [];
    model = new CityModel();
    activeCityName : string = 'No city selected'; 
    cityName : string = '';

    constructor (
        private cityService : CityService,
        private storageService : StorageService,
        private store : Store<{ city : CityModel }>
    ) {
        let city = storageService.getCurrentCity();
        if (city) {
            this.setNewActiveCity(city);
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
        this.cities = [];
        this.setNewActiveCity(city);
        this.saveCity(city);
    }

    setNewActiveCity(city : CityModel) {
        this.activeCityName = city.name;
        this.cityName = "";
        this.model = city;
        this.store.dispatch(saveCity({city}));
    }

    saveCity(city : CityModel) {
        this.storageService.setNewCity(city);
    }
}
