import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { BehaviorSubject, Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { FilmModel } from 'src/app/Models/FilmModel';
import { HallModel } from 'src/app/Models/HallModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { SessionRequest } from 'src/app/Requests/SessionRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { CurrencyService } from 'src/app/Services/CurrencyService';
import { FilmService } from 'src/app/Services/FilmService';
import { HallService } from 'src/app/Services/HallService';
import { PageService } from 'src/app/Services/pageservice';
import { SeatTypePriceService } from 'src/app/Services/SeatTypePriceService';
import { SessionService } from 'src/app/Services/SessionService';

@Component({
    selector : 'admin-session-add',
    templateUrl : './admin-session-add.component.html',
    providers : []
})
export class AdminSessionAddComponent {

    model : SessionModel = new SessionModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    filmName : string = '';
    filmTerm = new BehaviorSubject<string>(''); 
    films : Observable<FilmModel[]> = this.filmTerm.pipe(
        autocomplete(200, ((term: string) => this.filmService.getFilms(term)))
    );
    
    cinemaName : string = '';
    cinemaTerm = new BehaviorSubject<string>(''); 
    cinemas : Observable<CinemaModel[]> = this.cinemaTerm.pipe(
        autocomplete(200, ((term: string) => this.cinemaService.getCinemas(term)))
    );

    hallName : string = '';
    halls : Observable<HallModel[]> | undefined;

    currencies : Observable<CurrencyModel[]> | undefined;

    constructor(
        private sessionService : SessionService,
        private filmService : FilmService,
        private cinemaService : CinemaService,
        private hallService : HallService,
        private pageService : PageService,
        private seatTypePriceService : SeatTypePriceService,
        private currencyService : CurrencyService,
        private store : Store<{ city : CityModel }>
    ) { }


    setFilm(film : FilmModel) {
        this.filmName = film.name;
        this.model.filmId = film.id;
    }

    addSession() {
        const request = new SessionRequest(
            this.model.filmId,
            this.model.hallId,
            this.model.startDateTime
        );
        this.sessionService.addSession(request).subscribe(
            async (data : any) => {  
                this.success.flag = true;  
                this.model = new SessionModel();
            },
            (error : string) => {
                this.error.exists = true;
                this.error.text = error;
            }
        )
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}