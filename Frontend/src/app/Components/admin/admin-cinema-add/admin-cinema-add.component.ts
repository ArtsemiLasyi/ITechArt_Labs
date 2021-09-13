import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
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

    photo : File | undefined;
    model : CinemaModel = new CinemaModel();
    city : CityModel = new CityModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();
    selectedFileName : string = this.defaultFileName;
    cityName : string = "";
    cities : CityModel[] = []; 

    constructor(
        private cinemaService : CinemaService,
        private cityService : CityService) { }

    loadPhoto(event : any) : any {
        this.photo = event.target.files[0];
        if (this.photo === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.photo.name; 
    }

    getCities() {
        this.cityService
            .getCities(this.cityName)
            .subscribe(
                (data) =>  {
                    this.cities = data;
                }
            )
    }

    setCity(city : CityModel) {
        this.city = city;
        this.cities = [];
    }

    addCinema() {
        const request = new CinemaRequest(
            this.model.name,
            this.model.description,
            this.cityName
        );
        this.cinemaService.addCinema(request).subscribe(
            async (data : any) => {
                const id = data.id;
                const formData = new FormData();
                formData.append('formFile', this.photo!);  
                await this.cinemaService.addPhoto(id, formData);
                this.success.flag = true;
            }
        )
    }
}