import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { HallModel } from 'src/app/Models/HallModel';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { HallService } from 'src/app/Services/HallService';

@Component({
    selector : 'admin-hall-search',
    templateUrl : './admin-hall-search.component.html',
    styleUrls : ['./admin-hall-search.component.scss'],
    providers : [HallService]
})
export class AdminHallSearchComponent {
    cinemaName : string = "";
    city : CityModel = new CityModel();
    cinemas : Observable<CinemaModel[]> | undefined;
    halls: Observable<HallModel[]> | undefined;

    constructor(
        private hallService : HallService,
        private store : Store<{ city : CityModel }>,
        private cinemaService : CinemaService
    ) { }
    
    getCinemas() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
        let request = new CinemaSearchRequest();
        request.cinemaName = this.cinemaName;
        this.cinemas = this.cinemaService.getCinemas(
            this.city.id,
            request
        );
    }

    setCinema(cinema : CinemaModel) {
        this.cinemaName = cinema.name;
        this.getHalls(cinema.id);
    }

    search(cinemaName : string) {
        let request = new CinemaSearchRequest();
        request.cinemaName = cinemaName;
        this.cinemas = this.cinemaService.getCinemas(
            this.city.id,
            request
        );
    }

    getHalls(cinemaId : number) {
        this.halls = this.hallService
            .getHalls(cinemaId);
    }

    getPhoto(id : number) {
        return this.hallService.getPhoto(id);
    }
}
