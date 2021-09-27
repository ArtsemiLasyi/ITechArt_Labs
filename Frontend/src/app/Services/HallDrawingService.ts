import { Injectable } from "@angular/core";
import { Colors } from "../Constants/Colors";
import { HallDrawingParameters } from "../Constants/HallDrawingParameters";
import { DrawModel } from "../Models/DrawModel";
import { HallSizeModel } from "../Models/HallSizeModel";
import { SeatDrawModel } from "../Models/SeatDrawModel";
import { SeatModel } from "../Models/SeatModel";
import { HallSizeService } from "./HallSizeService";

@Injectable()
export class HallDrawingService {
    
    context! : CanvasRenderingContext2D | null;

    constructor(
        private hallSizeService : HallSizeService
    ) { }

    init(canvas : HTMLCanvasElement) {
        this.context = canvas.getContext('2d');
    }

    drawHall(seats : SeatModel[], canvas : HTMLCanvasElement) : SeatDrawModel[] {
        let size = this.hallSizeService.getHallSize(seats);
        this.setCanvasSize(canvas, size);
        this.drawRect(canvas);
        let seatDrawModels = this.drawSeats(seats, size);
        this.drawRowNumbers(canvas, size);
        this.drawPlaceNumbers(canvas, size);
        return seatDrawModels;
    }

    private drawSeats(seats : SeatModel[], size : HallSizeModel) : SeatDrawModel[] {
        let seatDrawModels : SeatDrawModel[] = [];
        for (let i = 1; i <= size.rowsNumber; i++) {
            for (let j = 1; j <= size.placesNumber; j++) {
                let drawen = false;
                let seatDrawModel;
                for (const seat of seats) {
                    if (seat.row === i && seat.place === j) {
                        seatDrawModel = this.drawSeat(seat);
                        seatDrawModels.push(seatDrawModel);
                        drawen = true;
                        break;
                    }
                }
                if (!drawen) {
                    seatDrawModel = this.drawEmptyPlace(i, j);
                    seatDrawModels.push(seatDrawModel);
                }
            }
        }
        return seatDrawModels;
    }

    drawEmptyPlace(row : number, place : number) : SeatDrawModel {
        this.context!.fillStyle = Colors.Black;
        this.context!.beginPath();

        let x = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3 
            + (place - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;
        let y = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3
            + (row - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;

        let startAngle = HallDrawingParameters.MIN_ANGLE;
        let endAngle = HallDrawingParameters.MAX_ANGLE;

        let drawing = new DrawModel(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            HallDrawingParameters.SEAT_RADIUS
        );

        this.context!.arc(
            x,
            y,
            HallDrawingParameters.EMPTY_PLACE_RADIUS,
            startAngle,
            endAngle,
            true
        );
        this.context!.fill();
        return new SeatDrawModel(drawing);
    }

    drawSeat(seat : SeatModel) : SeatDrawModel {
        this.context!.beginPath();

        let x = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3 
            + (seat.place - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;
        let y = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3
            + (seat.row - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;

        let startAngle = HallDrawingParameters.MIN_ANGLE;
        let endAngle = HallDrawingParameters.MAX_ANGLE;

        let drawing = new DrawModel(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            HallDrawingParameters.SEAT_RADIUS
        );

        this.context!.fillStyle = Colors.Black;

        this.context!.arc(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            startAngle,
            endAngle,
            true
        );
        
        this.context!.fill();

        this.context!.fillStyle = seat.colorRgb;

        this.context!.arc(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            startAngle,
            endAngle,
            true
        );
        this.context!.fill();
        return new SeatDrawModel(drawing, seat);
    }

    private setCanvasSize(canvas : HTMLCanvasElement, size : HallSizeModel) {
        canvas.height = 
            (size.rowsNumber + 2) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;
        canvas.width = 
            (size.placesNumber + 2) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;
    }

    private drawRect(canvas : HTMLCanvasElement) {
        this.context!.fillStyle = Colors.Black;
        this.context!.strokeRect(
            HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER,
            HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER,
            canvas.width 
                - 2 * HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER,
            canvas.height
                - 2 * HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER
        );
    }

    clearSeat(row : number, place : number) {
        this.context!.fillStyle = Colors.White;
        this.context!.beginPath();

        let x = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3 
            + (place - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;
        let y = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3
            + (row - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;

        let startAngle = HallDrawingParameters.MIN_ANGLE;
        let endAngle = HallDrawingParameters.MAX_ANGLE;

        this.context!.arc(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            startAngle,
            endAngle,
            true
        );
        this.context!.fill();
    }

    private drawRowNumbers(canvas : HTMLCanvasElement, size : HallSizeModel) {
        this.context!.fillStyle = "rgb(0, 0, 200)";
        this.context!.font = "16px roboto";
        
        for (let i = 0; i < size.rowsNumber; i++) {
            let y = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3  
                + (i * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2);
            this.context!.fillText(
                String(i + 1),
                0,
                y
            );
        }
    }

    private drawPlaceNumbers(canvas : HTMLCanvasElement, size : HallSizeModel) {
        this.context!.fillStyle = "rgb(0, 0, 200)";
        this.context!.font = "16px roboto";
        
        for (let i = 0; i < size.placesNumber; i++) {
            let x = HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3 
            + i * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;
            this.context!.fillText(
                String(i + 1),
                x,
                HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 2 / 3
            );
        }
    }

    private checkSeat(
        x : number,
        y : number,
        model : SeatDrawModel
    ) : boolean {
        let isOnXAxis = 
            (x >= model.drawing.x - model.drawing.width / 2)
            && (x <= model.drawing.x + model.drawing.width / 2);
        let isOnYAxis = 
            (y >= model.drawing.y - model.drawing.height / 2)
            && (y <= model.drawing.y + model.drawing.height / 2);
        return isOnXAxis && isOnYAxis;
    }

    checkSeats(
        x : number,
        y : number,
        models : SeatDrawModel[]
    ) : number {
        for (let i = 0; i < models.length; i++) {
            if (this.checkSeat(x, y, models[i])) {
                return i;
            }
        }
        return -1;
    }

    getRowNumber(y : number) : number {
        let row = (y 
            - (HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3))
            / (HallDrawingParameters.INDENT_BETWEEN_SEATS * 2) + 1;
        return Math.round(row);
    }

    getPlaceNumber(x : number) : number {
        let place = (x 
            - (HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3))
            / (HallDrawingParameters.INDENT_BETWEEN_SEATS * 2) + 1;
        return Math.round(place);
    }

    modelClicked(model : DrawModel, x : number, y : number) : boolean {
        let onAxisX = x >= model.x && x <= model.x + model.width;
        let onAxisY = y >= model.y && y <= model.y + model.height 
        if (onAxisX && onAxisY) {
            return true;
        }
        return false;
    }
}