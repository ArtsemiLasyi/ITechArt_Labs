import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { HallModel } from 'src/app/Models/HallModel';
import { SeatsModel } from 'src/app/Models/SeatsModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { HallRequest } from 'src/app/Requests/HallRequest';
import { SeatsRequest } from 'src/app/Requests/SeatsRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { HallService } from 'src/app/Services/HallService';
import { SeatService } from 'src/app/Services/SeatService';
import { AdminHallConstructorDialogComponent } from '../admin-hall-constructor-dialog/admin-hall-constructor-dialog.component';

@Component({
    selector: 'admin-hall-add',
    templateUrl: './admin-hall-add.component.html',
    providers: [HallService]
})
export class AdminHallAddComponent {

    readonly defaultFileName : string = 'Add photo';

    photo : File | undefined;
    cinemas : Observable<CinemaModel[]> | undefined;
    model : HallModel = new HallModel();
    error : ErrorModel = new ErrorModel();
    city : CityModel = new CityModel();
    success : SuccessModel = new SuccessModel();
    selectedFileName : string = this.defaultFileName;
    cinemaName : string = '';
    seats : SeatsModel = new SeatsModel();

    constructor(
        private hallService : HallService,
        private dialog: MatDialog,
        private store : Store<{ city : CityModel }>,
        private seatService : SeatService,
        private cinemaService : CinemaService
    ) { }

    loadPhoto(event : Event) : any {
        this.photo = (event.target as HTMLInputElement).files![0];
        if (this.photo === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.photo.name; 
    }

    addHall() {
        const request = new HallRequest(
            this.model.cinemaId,
            this.model.name,
        );
        this.hallService.addHall(request).subscribe(
            async (data : any) => {
                const id = data;
                const formData = new FormData();
                if (this.photo) {
                    formData.append('formFile', this.photo);  
                    await this.hallService
                        .addPhoto(id, formData)
                        .toPromise();
                }
                if (this.seats.value.length === 0) {
                    return;
                }
                this.seats.value.forEach(seat => seat.hallId = id);
                await this.seatService
                    .addSeats(
                        id,
                        new SeatsRequest(this.seats.value)
                    )
                    .toPromise();
                this.success.flag = true;
                this.model = new HallModel();
            },
            (error  : string) => {
                this.error.exists = true;
                this.error.text = error;
            }
        )
    }

    getCinemas() {
        this.store.select('city').subscribe(
            (city) => {
                this.city = city;
            }
        );
        let request = new CinemaSearchRequest();
        request.cinemaName = this.cinemaName;
        this.cinemas = this.cinemaService.getCinemas(
            this.city.id,
            request
        );
    }

    setCinema(cinema : CinemaModel) {
        this.model.cinemaId = cinema.id;
        this.cinemaName = cinema.name;
    }

    addSeats() {
        const dialogRef = this.dialog.open(AdminHallConstructorDialogComponent, {
            restoreFocus : false,
            data : {
                value : this.seats.value
            }
        });

        dialogRef.afterClosed().subscribe(
            result => {
                this.seats.value = result;
            }
        );
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}
