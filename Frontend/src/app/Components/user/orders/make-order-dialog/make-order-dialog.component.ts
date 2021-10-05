import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CinemaServiceModel } from 'src/app/Models/CinemaServiceModel';
import { PriceModel } from 'src/app/Models/PriceModel';
import { SeatDrawModel } from 'src/app/Models/SeatDrawModel';
import { SeatsModel } from 'src/app/Models/SeatsModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { SessionSeatModel } from 'src/app/Models/SessionSeatModel';
import { SessionSeatsModel } from 'src/app/Models/SessionSeatsModel';
import { SessionSeatStatuses } from 'src/app/Models/SessionSeatStatuses';
import { UserModel } from 'src/app/Models/UserModel';
import { CinemaServiceRequest } from 'src/app/Requests/CinemaServiceRequest';
import { OrderRequest } from 'src/app/Requests/OrderRequest';
import { PriceRequest } from 'src/app/Requests/PriceRequest';
import { SessionSeatRequest } from 'src/app/Requests/SessionSeatRequest';
import { SessionSeatsRequest } from 'src/app/Requests/SessionSeatsRequest';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';
import { DrawingService } from 'src/app/Services/DrawingService';
import { HallService } from 'src/app/Services/HallService';
import { HallSizeService } from 'src/app/Services/HallSizeService';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatService } from 'src/app/Services/SeatService';
import { SessionSeatService } from 'src/app/Services/SessionSeatService';
import { UserService } from 'src/app/Services/UserService';

@Component({
    selector : 'make-order-dialog',
    templateUrl : './make-order-dialog.component.html',
    providers: [DrawingService]
})
export class MakeOrderDialogComponent implements OnInit {
    
    @ViewChild('canvas', { static: true }) 
    canvas! : ElementRef<HTMLCanvasElement>;
    seats : SeatsModel = new SeatsModel();
    seatDrawModels : SeatDrawModel[] = [];

    cinemaId : number = 0;
    user : UserModel = new UserModel();

    cinemaServices : Observable<CinemaServiceModel[]> | undefined;
    sessionSeats : SessionSeatsModel = new SessionSeatsModel();

    orderSeats : SessionSeatsModel = new SessionSeatsModel();
    orderServices : CinemaServiceModel[] = [];

    sum : PriceModel = new PriceModel();
    calculatedSum : boolean = false;

    constructor(
        public dialogRef : MatDialogRef<MakeOrderDialogComponent>,
        private userService : UserService,
        private hallSizeService : HallSizeService,
        private seatService : SeatService,
        private hallService : HallService,
        private sessionSeatService : SessionSeatService,
        private orderService : OrderService,
        private cinemaServiceService : CinemaServiceService,
        private hallDrawingService : DrawingService,
        @Inject(MAT_DIALOG_DATA) private model : SessionModel
    ) { }

    async ngOnInit() {
        this.user = await this.userService.getCurrentUser().toPromise();

        let hall = await this.hallService.getHall(this.model.hallId).toPromise();
        this.cinemaId = hall.cinemaId;

        this.seats = await this.seatService.getSeats(this.model.hallId).toPromise();
        this.sessionSeats = await this.sessionSeatService
            .getSessionSeats(this.model.id)
            .toPromise();

        this.hallDrawingService.init(this.canvas.nativeElement);
        this.drawHall();

        this.getCinemaServices();
    }
    
    drawHall() {
        this.seatDrawModels = this.hallDrawingService.drawHall(
            this.seats.value,
            this.canvas.nativeElement
        );
        this.hallDrawingService.drawSessionSeats(
            this.sessionSeats.value,
            this.hallSizeService.getHallSize(this.seats.value)
        );
    } 

    onNoClick() {
        this.dialogRef.close();
    }

    getCinemaServices() {
        this.cinemaServices = this.cinemaServiceService.getCinemaServices(this.cinemaId);
    }

    async calculateSum() {
        this.sum = await this.orderService.calculateSum(
            new OrderRequest(
                this.model.id,
                new Date(),
                this.getSessionSeatsRequest(),
                this.getCinemaServicesRequest()
            )
        )
        .toPromise();
        this.calculatedSum = true;
    }

    async tryToTakeOrFreeSeat(event : MouseEvent) {
        let x = event.offsetX;
        let y = event.offsetY;

        let index = this.hallDrawingService.checkSeats(x, y, this.seatDrawModels);
        if (index !== -1) {

            let request = new SessionSeatRequest(
                this.seatDrawModels[index].seat!.id,
                this.model.id,
                this.user.id
            );

            for (let sessionSeat of this.sessionSeats.value) {

                if (this.seatDrawModels[index].seat?.id === sessionSeat.seatId) {
                    if (sessionSeat.status === SessionSeatStatuses.Ordered) {
                        return;
                    } 

                    if (sessionSeat.status === SessionSeatStatuses.Free) {
                        await this.sessionSeatService.take(
                            this.model.id,
                            sessionSeat.seatId,
                            request
                        ).toPromise();
                        this.addSeat(sessionSeat);
                        this.drawHall();
                        this.calculateSum();
                        return;
                    } else if (sessionSeat.status === SessionSeatStatuses.Taken) {
                        await this.sessionSeatService.free(
                            this.model.id,
                            sessionSeat.seatId,
                            request
                        ).toPromise();
                        this.deleteSeat(sessionSeat);
                        this.drawHall();
                        this.calculateSum();
                        return;
                    }
                }
            }
            await this.sessionSeatService.take(
                this.model.id,
                this.seatDrawModels[index].seat!.id,
                request
            ).toPromise();
            this.addSeat(
                await this.sessionSeatService
                    .getSessionSeat(
                        this.model.id,
                        this.seatDrawModels[index].seat!.id
                    )
                    .toPromise()
            );
            this.sessionSeats = await this.sessionSeatService
                .getSessionSeats(this.model.id)
                .toPromise();
            this.drawHall();
            this.calculateSum();
        }
    }

    makeOrder() {
        this.orderService.addOrder(
            new OrderRequest(
                this.model.id,
                new Date(),
                this.getSessionSeatsRequest(),
                this.getCinemaServicesRequest()
            )
        );
    }

    addSeat(seat : SessionSeatModel) {
        this.orderSeats.value.push(seat);
    }

    deleteSeat(seat : SessionSeatModel) {
        let index = 0;
        for (let i = 0; i < this.orderSeats.value.length; i++) {
            if (this.orderSeats.value[i] === seat) {
                index = i;
                break;
            }
        }
        this.orderSeats.value.splice(index, 1);
    }

    addService(service : CinemaServiceModel) {
        this.orderServices.push(service);
    }

    deleteService(service : CinemaServiceModel) {
        let index = 0;
        for (let i = 0; i < this.orderServices.length; i++) {
            if (this.orderServices[i] === service) {
                index = i;
                break;
            }
        }
        this.orderServices.splice(index, 1);
    }

    private getCinemaServicesRequest() : CinemaServiceRequest[] {
        let request : CinemaServiceRequest[] = [];
        for (let service of this.orderServices) {
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

    private getSessionSeatsRequest() : SessionSeatsRequest {
        let value : SessionSeatRequest[] = [];
        for (let seat of this.orderSeats.value) {
            value.push(
                new SessionSeatRequest(
                    seat.seatId,
                    seat.sessionId,
                    this.user.id
                )
            );
        }
        let request = new SessionSeatsRequest(value);
        return request;
    }
}