import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/internal/Observable';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { SessionSearchRequest } from 'src/app/Requests/SessionSearchRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { DateTimeService } from 'src/app/Services/DateTimeService';
import { SessionService } from 'src/app/Services/SessionService';
import { MakeOrderDialogComponent } from '../../orders/make-order-dialog/make-order-dialog.component';

@Component({
    selector : 'sessions-list',
    templateUrl : './sessions-list.component.html',
    styleUrls : ['./sessions-list.component.scss'],
    providers : []
})
export class SessionsListComponent implements OnInit {

    @Input() filmId = 0;

    sessions : Observable<SessionModel[]>[] = [];
    flags : boolean[] = [];
    cinemas : CinemaModel[] = [];
    city : CityModel = new CityModel();

    firstSessionDate : Date | undefined;
    lastSessionDate : Date | undefined;
    freeSeatsNumber : number | undefined;
    date : Date = new Date();

    constructor (
        private dialog: MatDialog,
        private cinemaService : CinemaService,
        private sessionService : SessionService,
        private dateTimeService : DateTimeService,
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
        let request = new SessionSearchRequest();
        request.filmId = this.filmId;
        request.firstSessionDateTime = this.firstSessionDate;
        request.lastSessionDateTime = this.lastSessionDate;
        request.freeSeatsNumber = this.freeSeatsNumber;
        this.sessions[cinemaId] = this.sessionService.getSessions(cinemaId, request);
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
        return this.dateTimeService.getTime(session.startDateTime);
    }

    getDate(session : SessionModel) {
        return this.dateTimeService.getDate(session.startDateTime);
    }

    openDialog(model : SessionModel) {
        this.dialog.open(
            MakeOrderDialogComponent, {
                restoreFocus: true,
                data : { 
                    id : model.id,
                    filmId : model.filmId,
                    hallId : model.hallId,
                    startDateTime : model.startDateTime 
                }
            }
        );
    }
}
