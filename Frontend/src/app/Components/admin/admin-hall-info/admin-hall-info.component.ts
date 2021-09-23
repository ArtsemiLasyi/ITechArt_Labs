import { Component, HostListener } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { CityModel } from 'src/app/Models/CityModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { HallModel } from 'src/app/Models/HallModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { CinemaSearchRequest } from 'src/app/Requests/CinemaSearchRequest';
import { HallRequest } from 'src/app/Requests/HallRequest';
import { CinemaService } from 'src/app/Services/CinemaService';
import { HallService } from 'src/app/Services/HallService';
import { AdminHallConstructorDialogComponent } from '../admin-hall-constructor-dialog/admin-hall-constructor-dialog.component';

@Component({
    selector : 'admin-hall-info',
    templateUrl : './admin-hall-info.component.html',
    providers : [HallService]
})
export class AdminHallInfoComponent {

    readonly defaultFileName : string = 'Add photo';

    photo : File | undefined;
    selectedFileName : string = this.defaultFileName;
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();
    model : HallModel = new HallModel();
    cinemaName : string = '';
    cinemas : Observable<CinemaModel[]> | undefined;

    constructor(
        private hallService : HallService,
        private cinemaService : CinemaService,
        private dialog : MatDialog,
        private activateRoute: ActivatedRoute,
        private store : Store<{ city : CityModel }>
    ) { }

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
        this.hallService
            .getHall(this.model.id)
            .subscribe(
                (hall) => {
                    this.model = hall;
                    this.cinemaService
                        .getCinema(this.model.cinemaId)
                        .subscribe(
                            (cinema) => {
                                this.cinemaName = cinema.name;
                            }
                        )
                }
            )
    }

    getCinemas() {
        let activeCity = new CityModel();
        this.store.select('city').subscribe(
            (city) => {
                activeCity = city;
            }
        );

        let request = new CinemaSearchRequest();
        request.cinemaName = this.cinemaName;
        this.cinemas = this.cinemaService.getCinemas(
            activeCity.id,
            request
        );
    }

    setCinema(cinema : CinemaModel) {
        this.model.cinemaId = cinema.id;
        this.cinemaName = cinema.name;
    }

    async editHall() {
        let request = new HallRequest(
            this.model.cinemaId,
            this.model.name
        );
        await this.hallService.editHall(this.model.id, request);
        this.success.flag = true;
    }

    async deleteHall() {
        await this.hallService.deleteHall(this.model.id);
        this.success.flag = true;
    }

    editSeats() {
        const dialogRef = this.dialog.open(
            AdminHallConstructorDialogComponent, {
                restoreFocus: false
            }
        );
    }

    @HostListener('document:click', ['$event'])
    documentClick(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}
