import { Component, ElementRef, Inject, OnInit, ViewChild } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { NgbModalConfig } from "@ng-bootstrap/ng-bootstrap";
import { Observable } from 'rxjs';
import { HallSizeModel } from "src/app/Models/HallSizeModel";
import { SeatDrawModel } from "src/app/Models/SeatDrawModel";
import { SeatModel } from "src/app/Models/SeatModel";
import { SeatsModel } from "src/app/Models/SeatsModel";
import { SeatTypeModel } from "src/app/Models/SeatTypeModel";
import { HallDrawingService } from "src/app/Services/HallDrawingService";
import { HallSizeService } from "src/app/Services/HallSizeService";
import { SeatTypeService } from "src/app/Services/SeatTypeService";

@Component({
    selector : 'admin-hall-constructor-dialog',
    templateUrl : './admin-hall-constructor-dialog.component.html',
    styleUrls : ['./admin-hall-constructor-dialog.component.scss'],
    providers : []
})
export class AdminHallConstructorDialogComponent implements OnInit {
    
    @ViewChild('canvas', { static: true }) 
    canvas! : ElementRef<HTMLCanvasElement>;
    seatDrawModels : SeatDrawModel[] = [];
    seatTypes : Observable<SeatTypeModel[]> | undefined;
    size : HallSizeModel = new HallSizeModel();
    activeSeatType : SeatTypeModel = new SeatTypeModel();
    newSeats : SeatsModel = new SeatsModel();

    constructor(
        private config : NgbModalConfig,
        private seatTypeService : SeatTypeService,
        private hallSizeService : HallSizeService,
        private hallDrawingService : HallDrawingService,
        private dialogRef : MatDialogRef<AdminHallConstructorDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private seats : SeatsModel
    ) {
        config.backdrop = 'static';
        config.centered = true;
        config.keyboard = true;
        config.animation = true;
    }

    ngOnInit(): void {
        this.getSeatTypes();
        this.size = this.hallSizeService.getHallSize(this.seats.value);
        this.hallDrawingService.init(this.canvas.nativeElement);
        this.drawHall();
    }
    
    getSeatTypes() {
        this.seatTypes = this.seatTypeService.getSeatTypes();
    }

    onNoClick() {
        this.seatDrawModels.forEach(
            value => {
                if (value.seat) {
                    this.newSeats.value.push(value.seat);
                }
            }
        );
        this.dialogRef.close(this.newSeats.value);
    }

    drawHall() {
        this.seatDrawModels = this.hallDrawingService.drawHall(
            this.seats.value,
            this.canvas.nativeElement,
            this.size
        );
    }

    rowsNumberChanged(rowsNumber : number) {
        this.size.rowsNumber = rowsNumber;
        this.drawHall();
    }

    placesNumberChanged(placesNumber : number) {
        this.size.placesNumber = placesNumber;
        this.drawHall();
    }

    selectSeatType(seatType : SeatTypeModel) {
        this.activeSeatType = seatType;
    }

    tryToAddOrDeleteSeat(event : MouseEvent) {

        let x = event.offsetX;
        let y = event.offsetY;

        let index = this.hallDrawingService.checkSeats(x, y, this.seatDrawModels);
        if (index !== -1) {
            if (this.seatDrawModels[index].seat) {
                this.hallDrawingService.clearSeat(
                    this.seatDrawModels[index].seat!.row,
                    this.seatDrawModels[index].seat!.place
                );
                this.hallDrawingService.drawEmptyPlace(
                    this.seatDrawModels[index].seat!.row,
                    this.seatDrawModels[index].seat!.place
                );
                this.seatDrawModels[index].seat = undefined;
            } else {
                if (!this.activeSeatType.id) {
                    return;
                }
                let seat = new SeatModel();
                seat.place = this.hallDrawingService.getPlaceNumber(x);
                seat.row = this.hallDrawingService.getRowNumber(y);
                seat.colorRgb = this.activeSeatType.colorRgb;
                seat.seatTypeId = this.activeSeatType.id;

                this.seatDrawModels[index].seat = seat;
                this.hallDrawingService.drawSeat(this.seatDrawModels[index].seat!);
            }
        }
    }
}