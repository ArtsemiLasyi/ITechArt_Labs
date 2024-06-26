import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
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
    selector: 'admin-cinema-add',
    templateUrl: './admin-cinema-add.component.html',
    providers: [CinemaService, CityService]
})
export class AdminCinemaAddComponent {

    readonly defaultFileName : string = 'Add photo';
    selectedFileName : string = this.defaultFileName;
    photo : File | undefined;

    model : CinemaModel = new CinemaModel();
    city : CityModel = new CityModel();
    
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    cityName : string = "";
    term = new BehaviorSubject<string>(''); 
    cities : Observable<CityModel[]> = this.term.pipe(
        autocomplete(100, ((term: string) => this.cityService.getCities(term)))
    ); 

    constructor(
        private cinemaService : CinemaService,
        private cityService : CityService) { }

    loadPhoto(event : Event) : any {
        this.photo = (event.target as HTMLInputElement).files![0];
        if (this.photo === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.photo.name; 
    }

    getCities() {
        this.term.next(this.cityName);
    }

    setCity(city : CityModel) {
        this.city = city;
        this.cityName = city.name;
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }

    addCinema() {
        const request = new CinemaRequest(
            this.model.name,
            this.model.description,
            this.cityName
        );
        this.cinemaService.addCinema(request).subscribe(
            async (data : any) => {  
                if (this.photo) {
                    const id = data;
                    const formData = new FormData();
                    formData.append('formFile', this.photo);
                    this.cinemaService.addPhoto(id, formData).toPromise();
                }
                this.success.flag = true;  
                this.model = new CinemaModel();
            },
            (error : string) => {
                this.error.exists = true;
                this.error.text = error;
            }
        )
    }
}