import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { HallModel } from 'src/app/Models/HallModel';
import { SeatDrawModel } from 'src/app/Models/SeatDrawModel';
import { SeatsModel } from 'src/app/Models/SeatsModel';
import { DrawingService } from 'src/app/Services/DrawingService';
import { SeatService } from 'src/app/Services/SeatService';

@Component({
    selector : 'hall-view-dialog',
    templateUrl : './hall-view-dialog.component.html',
    providers: [DrawingService]
})
export class HallViewDialogComponent implements OnInit {
    
    @ViewChild('canvas', { static: true }) 
    canvas! : ElementRef<HTMLCanvasElement>;
    seats : SeatsModel = new SeatsModel();
    seatDrawModels : SeatDrawModel[] = [];

    constructor(
        public dialogRef : MatDialogRef<HallViewDialogComponent>,
        private seatService : SeatService,
        private hallDrawingService : DrawingService,
        @Inject(MAT_DIALOG_DATA) private model : HallModel
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
}