import { Component } from '@angular/core';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { props, Store } from '@ngrx/store';
import { BehaviorSubject, Observable } from 'rxjs';
import { saveCity } from 'src/app/Actions/city.actions';
import { StorageKeyNames } from 'src/app/Constants/StorageKeyNames';
import { CityModel } from 'src/app/Models/CityModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { CityService } from 'src/app/Services/CityService';
import { StorageService } from 'src/app/Services/StorageService';

@Component({
    selector : 'app-header',
    templateUrl : './header.component.html',
    styleUrls : ['./header.component.scss']
})
export class HeaderComponent {
    term = new BehaviorSubject<string>(''); 
    cities : Observable<CityModel[]> = this.term.pipe(
        autocomplete(100, ((term: string) => this.cityService.getCities(term)))
    );
    model = new CityModel();
    activeCityName : string = 'No city selected'; 
    cityName : string = '';

    constructor (
        private cityService : CityService,
        private storageService : StorageService,
        private router : Router,
        private store : Store<{ city : CityModel }>
    ) {
        let city = storageService.getCurrentCity();
        if (city) {
            this.setNewActiveCity(city);
        }
    }

    getCities() {
        this.term.next(this.cityName);
    }

    setCity(city : CityModel) {
        this.setNewActiveCity(city);
        this.saveCity(city);
        const url = this.router.url;
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate([url]);
    }

    setNewActiveCity(city : CityModel) {
        this.activeCityName = city.name;
        this.cityName = '';
        this.model = city;
        this.store.dispatch(saveCity({city}));
    }

    saveCity(city : CityModel) {
        this.storageService.setNewCity(city);
    }
}
