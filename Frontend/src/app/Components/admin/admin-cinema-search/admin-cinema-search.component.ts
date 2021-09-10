import { Component } from "@angular/core";
import { Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { CinemaModel } from "src/app/Models/CinemaModel";
import { CityModel } from "src/app/Models/CityModel";
import { CinemaSearchRequest } from "src/app/Requests/CinemaSearchRequest";
import { CinemaService } from "src/app/Services/CinemaService";

@Component({
    selector: 'admin-cinema-search',
    templateUrl: './admin-cinema-search.component.html',
    styleUrls: ['./admin-cinema-search.component.scss'],
    providers: [CinemaService]
})
export class AdminCinemaSearchComponent {

    cinemas : Observable<CinemaModel[]> | undefined;
    cinemaName : string = "";
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

    getPhoto(id : number) : string {
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
    
    search(cinemaName : string) {
        let request = new CinemaSearchRequest();
        request.cinemaName = cinemaName;
        this.cinemaService.getCinemas(
            1,
            request
        );
    }
}