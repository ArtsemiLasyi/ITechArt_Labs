import { Component, Input, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/internal/Observable';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { SessionService } from 'src/app/Services/SessionService';

@Component({
    selector : 'cinemas-list',
    templateUrl : './cinemas-list.component.html',
    styleUrls : ['./cinemas-list.component.scss'],
    providers : [
        CinemaService
    ]
})
export class CinemasListComponent implements OnInit {

    sessions : Observable<SessionModel[]>[] = [];
    flags : boolean[] = [];
    cinemas : CinemaModel[] = [];
    city : CityModel = new CityModel();

    constructor (
        private cinemaService : CinemaService,
        private sessionService : SessionService,
        private store : Store<{ city : CityModel }>
    ) { }

    async getCinemas(cityId : number) {
        this.cinemas = await this.cinemaService
            .getCinemas(
                cityId, 
                new CinemaSearchRequest()
            )
            .toPromise();
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

    getSessions(cinemaId : number) {
        this.sessions[cinemaId] = this.sessionService.getSessions(cinemaId);
    }

    openCloseCollapse(cinemaId : number) {
        if (this.flags[cinemaId] === true) {
            this.flags[cinemaId] = false;
            return;
        }
        this.flags[cinemaId] = true;  
        this.getSessions(cinemaId);
    }

    getTime(session : SessionModel) {
        return new Date(session.startDateTime).toLocaleTimeString();
    }

    getDate(session : SessionModel) {
        return new Date(session.startDateTime).toLocaleDateString();
    }

}
