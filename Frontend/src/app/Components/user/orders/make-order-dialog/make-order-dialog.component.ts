import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HallModel } from 'src/app/Models/HallModel';
import { SeatDrawModel } from 'src/app/Models/SeatDrawModel';
import { SeatsModel } from 'src/app/Models/SeatsModel';
import { SessionModel } from 'src/app/Models/SessionModel';
import { SessionSeatModel } from 'src/app/Models/SessionSeatModel';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';
import { HallDrawingService } from 'src/app/Services/HallDrawingService';
import { OrderService } from 'src/app/Services/OrderService';
import { SeatService } from 'src/app/Services/SeatService';

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

    constructor(
        public dialogRef : MatDialogRef<MakeOrderDialogComponent>,
        private seatService : SeatService,
        private sessionSeatService : SessionSeatService,
        private orderService : OrderService,
        private cinemaServiceService : CinemaServiceService,
        private hallDrawingService : HallDrawingService,
        @Inject(MAT_DIALOG_DATA) private model : SessionModel
    ) { }

    ngOnInit() {
        this.seatService
            .getSeats(this.model.id)
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
    }
    
    onNoClick() {
        this.dialogRef.close();
    }

    calculateSum() {

    }

    takeSeat() {

    }

    freeSeat() {

    }

    makeOrder() {

    }
}