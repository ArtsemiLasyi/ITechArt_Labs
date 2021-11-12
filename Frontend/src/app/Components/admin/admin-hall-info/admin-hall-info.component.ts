import { Component} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
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
    seats : SeatsModel = new SeatsModel();

    disabledButton : boolean = false;

    constructor(
        private hallService : HallService,
        private cinemaService : CinemaService,
        private dialog : MatDialog,
        private seatService : SeatService,
        private activateRoute: ActivatedRoute,
        private router : Router,
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

    async ngOnInit() {
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
        this.seats = await this.seatService.getSeats(this.model.id).toPromise();
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
        await this.hallService.editHall(this.model.id, request).toPromise();
        if (this.photo) {
            const formData = new FormData();
            formData.append('formFile', this.photo);  
            await this.hallService
                .addPhoto(this.model.id, formData)
                .toPromise();
        }
        this.seats.value.forEach(seat => seat.hallId = this.model.id);
        await this.seatService
            .editSeats(
                this.model.id, 
                new SeatsRequest(this.seats.value)
            ).toPromise();
        this.success.flag = true;
    }

    async deleteHall() {
        await this.hallService.deleteHall(this.model.id).toPromise();
        await this.seatService.deleteSeats(this.model.id).toPromise();
        this.success.flag = true;
        this.disableButtons();
        this.router.navigate(
            ['../../search'], 
            { relativeTo: this.activateRoute }
        );
    }

    editSeats() {
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

    disableButtons() {
        this.disabledButton = true;
    }
}
