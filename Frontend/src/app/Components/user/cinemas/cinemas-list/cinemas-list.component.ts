import { Component, Input, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/internal/Observable';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { CinemaService } from 'src/app/Services/CinemaService';

@Component({
    selector: 'cinemas-list',
    templateUrl: './cinemas-list.component.html',
    providers: [
        CinemaService
    ]
})
export class CinemasListComponent implements OnInit {

    cinemas : Observable<CinemaModel[]> | undefined;
    city : CityModel = new CityModel();

    constructor (
        private cinemaService: CinemaService,
        private store : Store<{ city : CityModel }>
    ) { }

    getCinemas(cityId : number) {
        this.cinemas = this.cinemaService
            .getCinemas(
                cityId, 
                new CinemaSearchRequest()
            );
    }

    getPhoto(id : number) {
        return this.cinemaService.getPhoto(id);
    }

    ngOnInit() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
        this.getCinemas(this.city.id);
    }

}
