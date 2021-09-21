import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { CinemaRequest } from 'src/app/Requests/CinemaRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { CityService } from 'src/app/Services/CityService';

@Component({
    selector: 'admin-cinema-info',
    templateUrl: './admin-cinema-info.component.html',
    providers: [CinemaService]
})
export class AdminCinemaInfoComponent {

    readonly defaultFileName : string = 'Add photo';

    photo : File | undefined;
    error = new ErrorModel();
    success = new SuccessModel();
    selectedFileName : string = this.defaultFileName;

    cityName : string = '';
    city : CityModel = new CityModel();
    term = new BehaviorSubject<string>(''); 
    cities : Observable<CityModel[]> = this.term.pipe(
        autocomplete(500, ((term: string) => this.cityService.getCities(term)))
    );
    model : CinemaModel = new CinemaModel();

    constructor(
        private cityService : CityService,
        private cinemaService : CinemaService,
        private activateRoute: ActivatedRoute) { }

    getCities() {
        this.term.next(this.cityName);
    }

    setCity(city : CityModel) {
        this.city = city;
    }

    loadPhoto(event : Event) : any {
        this.photo = (event.target as HTMLInputElement).files![0];
        if (this.photo === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.photo.name; 
    }

    ngOnInit() {
        this.model.id = this.activateRoute.snapshot.params['id'];
        this.cinemaService
            .getCinema(this.model.id)
            .subscribe(
                (cinema : CinemaModel) => {
                    this.model = cinema;
                }
            )
    }


    async editCinema() {
        let request = new CinemaRequest(
            this.model.name,
            this.model.description,
            this.model.cityName
        );
        await this.cinemaService.editCinema(this.model.id, request);
        this.success.flag = true;
    }

    async deleteCinema() {
        await this.cinemaService.deleteCinema(this.model.id);
        this.success.flag = true;
    }
}