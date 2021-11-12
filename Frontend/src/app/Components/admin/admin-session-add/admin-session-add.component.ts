import { CursorError } from '@angular/compiler/src/ml_parser/lexer';
import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { BehaviorSubject, Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { CurrencyModel } from 'src/app/Models/CurrencyModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { FilmModel } from 'src/app/Models/FilmModel';
import { HallModel } from 'src/app/Models/HallModel';
import { SeatTypeModel } from 'src/app/Models/SeatTypeModel';
import { SeatTypePriceModel } from 'src/app/Models/SeatTypePriceModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { FilmSearchRequest } from 'src/app/Requests/FilmSearchRequest';
import { PriceRequest } from 'src/app/Requests/PriceRequest';
import { SeatTypePriceRequest } from 'src/app/Requests/SeatTypePriceRequest';
import { SessionRequest } from 'src/app/Requests/SessionRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { CurrencyService } from 'src/app/Services/CurrencyService';
import { FilmService } from 'src/app/Services/FilmService';
import { HallService } from 'src/app/Services/HallService';
import { PageService } from 'src/app/Services/pageservice';
import { SeatTypeService } from 'src/app/Services/SeatTypeService';
import { SessionService } from 'src/app/Services/SessionService';

@Component({
    selector : 'admin-session-add',
    templateUrl : './admin-session-add.component.html',
    providers : []
})
export class AdminSessionAddComponent implements OnInit {

    model : SessionModel = new SessionModel();
    city : CityModel = new CityModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    filmName : string = '';
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

    hallName : string = '';
    halls : Observable<HallModel[]> | undefined;

    currency : CurrencyModel = new CurrencyModel();
    currencies : Observable<CurrencyModel[]> | undefined;
    seatTypes : Observable<SeatTypeModel[]> | undefined;
    values : number[] = [];
    seatTypePrices : SeatTypePriceModel[] = [];

    constructor(
        private sessionService : SessionService,
        private filmService : FilmService,
        private cinemaService : CinemaService,
        private hallService : HallService,
        private pageService : PageService,
        private seatTypeService : SeatTypeService,
        private currencyService : CurrencyService,
        private store : Store<{ city : CityModel }>
    ) { }

    ngOnInit() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
        this.getSeatTypes();
    }

    addSession() {
        const request = new SessionRequest(
            this.model.filmId,
            this.model.hallId,
            this.model.startDateTime,
            this.getSeatTypePricesRequest()
        );
        this.sessionService.addSession(request).subscribe(
            async (data : any) => {
                this.success.flag = true;  
                this.model = new SessionModel();
                this.seatTypePrices = [];
            },
            (error : string) => {
                this.error.exists = true;
                this.error.text = error;
            }
        )
    }

    getSeatTypePricesRequest() {
        let seatTypePricesRequest : SeatTypePriceRequest[] = [];
        for (let key in this.values) {
            seatTypePricesRequest.push(
                new SeatTypePriceRequest(
                    0,
                    parseInt(key),
                    new PriceRequest(
                        this.values[key] ?? 0,
                        this.currency.id
                    )
                )
            )
        }
        return seatTypePricesRequest;
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }

    setFilm(film : FilmModel) {
        this.filmName = film.name;
        this.model.filmId = film.id;
    }

    setCinema(cinema : CinemaModel) {
        this.cinema = cinema;
        this.cinemaName = cinema.name;
    }

    setHall(hall : HallModel) {
        this.model.hallId = hall.id;
        this.hallName = hall.name;
    }
    
    setCurrency(currency : CurrencyModel) {
        this.currency = currency;
    }

    getCinemas() {
        this.cinemaTerm.next(this.cinemaName);
    }

    getFilms() {
        this.filmTerm.next(this.filmName);
    }

    getHalls() {
        this.halls = this.hallService.getHalls(this.cinema.id);
    }

    getSeatTypes() {
        this.seatTypes = this.seatTypeService.getSeatTypes();
    }

    getCurrencies() {
        this.currencies = this.currencyService.getCurrencies();
    }
}