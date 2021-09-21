import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { BehaviorSubject, Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { CinemaService } from 'src/app/Services/CinemaService';

@Component({
    selector: 'admin-cinema-search',
    templateUrl: './admin-cinema-search.component.html',
    styleUrls: ['./admin-cinema-search.component.scss'],
    providers: [CinemaService]
})
export class AdminCinemaSearchComponent {

    term = new BehaviorSubject<CinemaSearchRequest>(new CinemaSearchRequest());
    cinemas : Observable<CinemaModel[]> = this.term.pipe(
        autocomplete(
            500, 
            (request : CinemaSearchRequest) => 
                this.cinemaService.getCinemas(this.city.id, request)
        )
    );
    cinemaName : string = '';
    city : CityModel = new CityModel();

    constructor (
        private cinemaService: CinemaService,
        private store : Store<{ city : CityModel }>
    ) { }

    getCinemas(request : CinemaSearchRequest = new CinemaSearchRequest()) {
        this.term.next(request);
    }

    getPhoto(id : number) : string {
        return this.cinemaService.getPhoto(id);
    }

    ngOnInit() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
        this.getCinemas();
    }
    
    search(cinemaName : string) {
        let request = new CinemaSearchRequest();
        request.cinemaName = cinemaName;
        this.term.next(request);
    }
}
