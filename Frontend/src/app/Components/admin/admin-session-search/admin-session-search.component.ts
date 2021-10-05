import { Component, OnInit } from "@angular/core";
import { Store } from "@ngrx/store";
import { BehaviorSubject, Observable } from "rxjs";
import { CinemaModel } from "src/app/Models/CinemaModel";
import { CityModel } from "src/app/Models/CityModel";
import { FilmModel } from "src/app/Models/FilmModel";
import { SessionModel } from "src/app/Models/SessionModel";
import { autocomplete } from "src/app/Operators/autocomplete.operator";
import { CinemaSearchRequest } from "src/app/Requests/CinemaSearchRequest";
import { FilmSearchRequest } from "src/app/Requests/FilmSearchRequest";
import { SessionSearchRequest } from "src/app/Requests/SessionSearchRequest";
import { CinemaService } from "src/app/Services/CinemaService";
import { DateTimeService } from "src/app/Services/DateTimeService";
import { FilmService } from "src/app/Services/FilmService";
import { PageService } from "src/app/Services/pageservice";
import { SessionService } from "src/app/Services/SessionService";

@Component({
    selector : 'admin-session-search',
    templateUrl : './admin-session-search.component.html',
    providers : []
})
export class AdminSessionSearchComponent implements OnInit {

    city : CityModel = new CityModel();
    sessions : Observable<SessionModel[]> | undefined;

    filmName : string = '';
    film : FilmModel = new FilmModel();
    filmTerm = new BehaviorSubject<string>(''); 
    films : Observable<FilmModel[]> = this.filmTerm.pipe(
        autocomplete(
            200,
            (term: string) => {
                let request = new FilmSearchRequest();
                request.filmName = term;
                return this.filmService.getFilms(
                    this.pageService.getPageNumber(),
                    this.pageService.getPageSize(),
                    request)
            }
        )
    );
    
    cinemaName : string = '';
    cinema : CinemaModel = new CinemaModel();
    cinemaTerm = new BehaviorSubject<string>(''); 
    cinemas : Observable<CinemaModel[]> = this.cinemaTerm.pipe(
        autocomplete(
            200,
            (term: string) => {
                let request = new CinemaSearchRequest();
                request.cinemaName = term;
                return this.cinemaService.getCinemas(this.city.id, request);
            }
        )
    );

    constructor(
        private filmService : FilmService,
        private cinemaService : CinemaService,
        private sessionService : SessionService,
        private pageService : PageService,
        private dateTimeService : DateTimeService,
        private store : Store<{ city : CityModel }>
    ) { }

    ngOnInit() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
    }

    setFilm(film : FilmModel) {
        this.film = film;
        this.filmName = film.name;
    }

    setCinema(cinema : CinemaModel) {
        this.cinema = cinema;
        this.cinemaName = cinema.name;
    }

    getCinemas() {
        this.cinemaTerm.next(this.cinemaName);
    }

    getFilms() {
        this.filmTerm.next(this.filmName);
    }

    getSessions() {
        if (this.cinema.id && this.film.id) {
            let request = new SessionSearchRequest();
            request.filmId = this.film.id;
            this.sessions = this.sessionService.getSessions(
                this.cinema.id,
                request
            )
        }
    }

    getTime(date : Date) {
        return this.dateTimeService.getTime(date);
    }

    getDate(date : Date) {
        return this.dateTimeService.getDate(date);
    }
}