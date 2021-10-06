import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject, Observable } from 'rxjs';
import { CinemaServiceModel } from 'src/app/Models/CinemaServiceModel';
import { PriceModel } from 'src/app/Models/PriceModel';
import { SeatDrawModel } from 'src/app/Models/SeatDrawModel';
import { SeatsModel } from 'src/app/Models/SeatsModel';
import { SeatTypeModel } from 'src/app/Models/SeatTypeModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { SessionSeatModel } from 'src/app/Models/SessionSeatModel';
import { SessionSeatsModel } from 'src/app/Models/SessionSeatsModel';
import { SessionSeatStatuses } from 'src/app/Models/SessionSeatStatuses';
import { UserModel } from 'src/app/Models/UserModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
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
import { SeatTypeService } from 'src/app/Services/SeatTypeService';
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

    @ViewChild('legend', { static: true }) 
    legend! : ElementRef<HTMLCanvasElement>;

    @ViewChild('statuses', { static: true }) 
    statuses! : ElementRef<HTMLCanvasElement>;

    seats : SeatsModel = new SeatsModel();
    seatDrawModels : SeatDrawModel[] = [];

    cinemaId : number = 0;
    userId : number = 0;

    seatTypes : SeatTypeModel[] = [];

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
        private seatTypeService : SeatTypeService,
        private seatService : SeatService,
        private hallService : HallService,
        private sessionSeatService : SessionSeatService,
        private orderService : OrderService,
        private cinemaServiceService : CinemaServiceService,
        private hallDrawingService : DrawingService,
        private modalService : NgbModal,
        private router : Router,
        @Inject(MAT_DIALOG_DATA) private model : SessionModel
    ) { }

    async ngOnInit() {
        this.userService.getCurrentUser().subscribe(
            (user : UserModel) => {
                this.userId = user.id;
            },
            (error : Error) => {
                this.router.navigate(['account/signin']);
                this.dialogRef.close();
            }
        );

        let hall = await this.hallService.getHall(this.model.hallId).toPromise();
        this.cinemaId = hall.cinemaId;

        this.seatTypes = await this.seatTypeService
            .getSeatTypes()
            .toPromise();
        this.hallDrawingService.init(this.legend.nativeElement);
        this.hallDrawingService.drawSeatTypesLegend(
            this.seatTypes,
            this.legend.nativeElement
        );

        this.hallDrawingService.init(this.statuses.nativeElement);
        this.hallDrawingService.drawSeatStatuses(this.statuses.nativeElement);

        this.seats = await this.seatService.getSeats(this.model.hallId).toPromise();
        await this.getSessionSeats();
        this.hallDrawingService.init(this.canvas.nativeElement);
        this.drawHall();

        this.getCinemaServices();

        this.sessionSeats.value.forEach(
            sessionSeat => {
                if (sessionSeat.userId === this.userId 
                        && sessionSeat.status === SessionSeatStatuses.Taken) {
                    this.orderSeats.value.push(sessionSeat);
                }
            }
        );
        if (this.orderSeats.value.length > 0) {
            this.calculateSum();
        }
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

    async getSessionSeats() {
        this.sessionSeats = await this.sessionSeatService
            .getSessionSeats(this.model.id)
            .toPromise();
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
                this.userId
            );

            for (let sessionSeat of this.sessionSeats.value) {

                if (this.seatDrawModels[index].seat?.id === sessionSeat.seatId) {
                    if (sessionSeat.status === SessionSeatStatuses.Ordered) {
                        return;
                    } 

                    if (sessionSeat.status === SessionSeatStatuses.Free) {
                        sessionSeat.status = SessionSeatStatuses.Taken;
                        this.takeSeat(sessionSeat.seatId, request);
                        this.addSeat(sessionSeat);
                        this.updateSessionSeats();    
                        return;
                    } else if (sessionSeat.status === SessionSeatStatuses.Taken) {
                        sessionSeat.status = SessionSeatStatuses.Free;
                        this.freeSeat(sessionSeat.seatId, request);
                        this.deleteSeat(sessionSeat);
                        this.updateSessionSeats();
                        return;
                    }
                }
            }
            this.takeSeat(this.seatDrawModels[index].seat!.id, request);

            let sessionSeat = new SessionSeatModel();
            sessionSeat.place = this.seatDrawModels[index].seat!.place;
            sessionSeat.row = this.seatDrawModels[index].seat!.row;
            sessionSeat.seatTypeId = this.seatDrawModels[index].seat!.seatTypeId;
            sessionSeat.seatId = this.seatDrawModels[index].seat!.id;
            sessionSeat.sessionId = this.model.id;
            sessionSeat.userId = this.userId;
            sessionSeat.status = SessionSeatStatuses.Taken;
            this.sessionSeats.value.push(sessionSeat);
            
            this.updateSessionSeats();
            this.sessionSeats.value.forEach(
                sessionSeat => {
                    if (sessionSeat.seatId === this.seatDrawModels[index].seat!.id) {
                        this.addSeat(sessionSeat);
                    }
                }
            )
        }
    }

    async makeOrder(content : any) {
        if (this.orderSeats.value.length > 0) {
            await this.orderService.addOrder(
                new OrderRequest(
                    this.model.id,
                    new Date(),
                    this.getSessionSeatsRequest(),
                    this.getCinemaServicesRequest()
                )
            ).toPromise();
            this.modalService.open(content);
            this.onNoClick();
        }
    }

    addSeat(seat : SessionSeatModel) {
        this.orderSeats.value.push(seat);
    }

    deleteSeat(seat : SessionSeatModel) {
        let index = 0;
        for (let i = 0; i < this.orderSeats.value.length; i++) {
            if (this.orderSeats.value[i].seatId === seat.seatId) {
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
                    this.userId
                )
            );
        }
        let request = new SessionSeatsRequest(value);
        return request;
    }

    private async takeSeat(
        seatId : number,
        request : SessionSeatRequest
    ) {
        await this.sessionSeatService.take(
            this.model.id,
            seatId,
            request
        ).toPromise();
    }

    private async freeSeat(
        seatId : number,
        request : SessionSeatRequest
    ) {
        await this.sessionSeatService.free(
            this.model.id,
            seatId,
            request
        ).toPromise();
    }

    private async updateSessionSeats() {
        this.drawHall();
    }
}