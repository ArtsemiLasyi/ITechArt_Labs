import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CinemaServiceModel } from 'src/app/Models/CinemaServiceModel';
import { HallModel } from 'src/app/Models/HallModel';
import { PriceModel } from 'src/app/Models/PriceModel';
import { SeatDrawModel } from 'src/app/Models/SeatDrawModel';
import { SeatsModel } from 'src/app/Models/SeatsModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { SessionSeatModel } from 'src/app/Models/SessionSeatModel';
import { CinemaServiceRequest } from 'src/app/Requests/CinemaServiceRequest';
import { OrderRequest } from 'src/app/Requests/OrderRequest';
import { PriceRequest } from 'src/app/Requests/PriceRequest';
import { SessionSeatsRequest } from 'src/app/Requests/SessionSeatsRequest';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';
import { HallDrawingService } from 'src/app/Services/HallDrawingService';
import { HallService } from 'src/app/Services/HallService';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatService } from 'src/app/Services/SeatService';
import { SessionSeatService } from 'src/app/Services/SessionSeatService';

@Component({
    selector : 'make-order-dialog',
    templateUrl : './make-order-dialog.component.html',
    providers: [HallDrawingService]
})
export class MakeOrderDialogComponent implements OnInit {
    
    @ViewChild('canvas', { static: true }) 
    canvas! : ElementRef<HTMLCanvasElement>;
    seats : SeatsModel = new SeatsModel();
    seatDrawModels : SeatDrawModel[] = [];

    cinemaId : number = 0;

    cinemaServices : Observable<CinemaServiceModel[]> | undefined;
    sessionSeats : SessionSeatModel[] = [];
    services : CinemaServiceModel[] = [];

    sum : PriceModel = new PriceModel();
    calculatedSum : boolean = false;

    constructor(
        public dialogRef : MatDialogRef<MakeOrderDialogComponent>,
        private seatService : SeatService,
        private hallService : HallService,
        private sessionSeatService : SessionSeatService,
        private orderService : OrderService,
        private cinemaServiceService : CinemaServiceService,
        private hallDrawingService : HallDrawingService,
        @Inject(MAT_DIALOG_DATA) private model : SessionModel
    ) { }

    ngOnInit() {
        this.hallService
            .getHall(this.model.hallId)
            .subscribe(
                (hall : HallModel) => {
                    this.cinemaId = hall.cinemaId;
                }
            );
        this.seatService
            .getSeats(this.model.hallId)
            .subscribe(
                (seats) => {
                    this.seats = seats;
                    this.hallDrawingService.init(this.canvas.nativeElement);
                    this.seatDrawModels = this.hallDrawingService.drawHall(
                        this.seats.value,
                        this.canvas.nativeElement
                    );
                }
            );
        this.getCinemaServices();
    }
    
    onNoClick() {
        this.dialogRef.close();
    }

    getCinemaServices() {
        this.cinemaServices = this.cinemaServiceService.getCinemaServices(this.cinemaId);
    }

    calculateSum() {
        this.orderService.calculateSum(
            new OrderRequest(
                this.model.id,
                new Date(),
                new SessionSeatsRequest(
                    []
                ),
                this.getCinemaServicesRequest()
            )
        )
        .subscribe(
            (sum : PriceModel) => {
                this.sum = sum;
            }
        )
        this.calculatedSum = true;
    }

    tryToTakeOrFreeSeat(event : MouseEvent) {

    }

    addService(cinemaService : CinemaServiceModel) {
        this.services.push(cinemaService);
    }

    makeOrder() {
        this.orderService.addOrder(
            new OrderRequest(
                this.model.id,
                new Date(),
                new SessionSeatsRequest(
                    []
                ),
                []
            )
        );
    }

    private getCinemaServicesRequest() : CinemaServiceRequest[] {
        let request : CinemaServiceRequest[] = [];
        for (let service of this.services) {
            request.push(
                new CinemaServiceRequest(
                    service.cinemaId,
                    service.serviceId,
                    new PriceRequest(
                        service.price.value,
                        service.price.currency.id
                    )
                )
            );
        }
        return request;
    }
}