import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Store } from "@ngrx/store";
import { BehaviorSubject, Observable } from "rxjs";
import { CinemaModel } from "src/app/Models/CinemaModel";
import { CityModel } from "src/app/Models/CityModel";
import { CurrencyModel } from "src/app/Models/CurrencyModel";
import { ErrorModel } from "src/app/Models/ErrorModel";
import { FilmModel } from "src/app/Models/FilmModel";
import { HallModel } from "src/app/Models/HallModel";
import { SeatTypeModel } from "src/app/Models/SeatTypeModel";
import { SeatTypePriceModel } from "src/app/Models/SeatTypePriceModel";
import { SessionModel } from "src/app/Models/SessionModel";
import { SuccessModel } from "src/app/Models/SuccessModel";
import { autocomplete } from "src/app/Operators/autocomplete.operator";
import { CinemaSearchRequest } from "src/app/Requests/CinemaSearchRequest";
import { FilmSearchRequest } from "src/app/Requests/FilmSearchRequest";
import { PriceRequest } from "src/app/Requests/PriceRequest";
import { SeatTypePriceRequest } from "src/app/Requests/SeatTypePriceRequest";
import { SessionRequest } from "src/app/Requests/SessionRequest";
import { CinemaService } from "src/app/Services/CinemaService";
import { CurrencyService } from "src/app/Services/CurrencyService";
import { FilmService } from "src/app/Services/FilmService";
import { HallService } from "src/app/Services/HallService";
import { PageService } from "src/app/Services/pageservice";
import { SeatTypePriceService } from "src/app/Services/SeatTypePriceService";
import { SeatTypeService } from "src/app/Services/SeatTypeService";
import { SessionService } from "src/app/Services/SessionService";

@Component({
    selector : 'admin-session-info',
    templateUrl : './admin-session-info.component.html',
    providers : []
})
export class AdminSessionInfoComponent implements OnInit {

    model : SessionModel = new SessionModel();
    city : CityModel = new CityModel();

    success : SuccessModel = new SuccessModel();
    error : ErrorModel = new ErrorModel();

    currencies : Observable<CurrencyModel[]> | undefined;
    seatTypePrices : SeatTypePriceModel[] = [];

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

    constructor(
        private sessionService : SessionService,
        private activateRoute : ActivatedRoute,
        private filmService : FilmService,
        private cinemaService : CinemaService,
        private hallService : HallService,
        private pageService : PageService,
        private seatTypeService : SeatTypeService,
        private seatTypePriceService : SeatTypePriceService,
        private currencyService : CurrencyService,
        private store : Store<{ city : CityModel }>
    ) { }

    ngOnInit() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
        this.model.id = this.activateRoute.snapshot.params['id'];
        this.sessionService
            .getSession(this.model.id)
            .subscribe(
                async (session) => {
                    this.model = session;
                    this.hallName = this.model.hallName;
                    this.filmName = this.model.filmName;
                    this.seatTypePrices = await this.seatTypePriceService
                        .getSeatTypePrices(this.model.id)
                        .toPromise();
                }
            )
    }

    editSession() {
        this.sessionService.editSession(
            this.model.id,
            new SessionRequest(
                this.model.filmId,
                this.model.hallId,
                this.model.startDateTime,
                this.getSeatTypePricesRequest()
            )
        );
    }

    getSeatTypePricesRequest() {
        let seatTypePricesRequest : SeatTypePriceRequest[] = [];
        for (let seatTypePrice of this.seatTypePrices) {
            seatTypePricesRequest.push(
                new SeatTypePriceRequest(
                    seatTypePrice.sessionId,
                    seatTypePrice.seatTypeId,
                    new PriceRequest(
                        seatTypePrice.price.value,
                        seatTypePrice.price.currency.id
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
        this.seatTypePrices.forEach(
            seatTypePrice => {
                seatTypePrice.price.currency = currency;
            }
        );
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

    getCurrencies() {
        this.currencies = this.currencyService.getCurrencies();
    }
}