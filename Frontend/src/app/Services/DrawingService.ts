import { Injectable } from "@angular/core";
import { HallDrawingParameters } from "../Constants/HallDrawingParameters";
import { DrawModel } from "../Models/DrawModel";
import { HallSizeModel } from "../Models/HallSizeModel";
import { SeatDrawModel } from "../Models/SeatDrawModel";
import { SeatModel } from "../Models/SeatModel";
import { SeatTypeModel } from "../Models/SeatTypeModel";
import { SessionSeatModel } from "../Models/SessionSeatModel";
import { SessionSeatStatuses } from "../Models/SessionSeatStatuses";
import { HallSizeService } from "./HallSizeService";

@Injectable()
export class DrawingService {
    
    context! : CanvasRenderingContext2D | null;

    constructor(
        private hallSizeService : HallSizeService
    ) { }

    init(canvas : HTMLCanvasElement) {
        this.context = canvas.getContext('2d');
    }

    drawHall(
        seats : SeatModel[],
        canvas : HTMLCanvasElement,
        size = this.hallSizeService.getHallSize(seats)
    ) : SeatDrawModel[] {
        this.setCanvasSize(canvas, size);
        this.drawRect(canvas);
        let seatDrawModels = this.drawSeats(seats, size);
        this.drawRowNumbers(canvas, size);
        this.drawPlaceNumbers(canvas, size);
        return seatDrawModels;
    }

    drawSeatStatuses(canvas : HTMLCanvasElement) {
        const statusesNumber = 3;
        canvas.height = 
            (1) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;
        canvas.width = 
            (statusesNumber * 3 + 2) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;

        for (let i = 0; i < statusesNumber; i++) {
            let x = this.getX(i);
            let y = this.getY(0);
            x += HallDrawingParameters.SEAT_RADIUS * 6 * i;
            
            this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;
            this.context!.font = 
                (HallDrawingParameters.SEAT_RADIUS * 1.2).toString() + 'px roboto';
            this.context!.textAlign = 'center';

            if (i === SessionSeatStatuses.Taken) {
                this.drawCross(x, y);
                x += HallDrawingParameters.SEAT_RADIUS * 9 / 2;
                this.context!.fillText(
                    'Taken',
                    x,
                    y + 5,
                    HallDrawingParameters.SEAT_RADIUS * 6
                );
            }

            if (i === SessionSeatStatuses.Free) {
                this.drawFigure(x, y, "#ffffff");
                x += HallDrawingParameters.SEAT_RADIUS * 9 / 2;
                this.context!.fillText(
                    'Free',
                    x,
                    y + 5,
                    HallDrawingParameters.SEAT_RADIUS * 6
                );
            }

            if (i === SessionSeatStatuses.Ordered) {
                this.context!.beginPath();
                let startAngle = HallDrawingParameters.MIN_ANGLE;
                let endAngle = HallDrawingParameters.MAX_ANGLE;
                this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;

                this.context!.arc(
                    x,
                    y,
                    HallDrawingParameters.SEAT_RADIUS / 2,
                    startAngle,
                    endAngle,
                    true
                );
                this.context!.fill();
                x += HallDrawingParameters.SEAT_RADIUS * 9 / 2;
                this.context!.fillText(
                    'Ordered',
                    x,
                    y + 5,
                    HallDrawingParameters.SEAT_RADIUS * 6
                );
            }
        }
    }

    drawSeatTypesLegend(
        seatTypes : SeatTypeModel[],
        canvas : HTMLCanvasElement,
    ) {
        canvas.height = 
            (1) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;
        canvas.width = 
            (seatTypes.length * 3 + 2) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;

        for (let i = 0; i < seatTypes.length; i++) {
            let x = this.getX(i);
            let y = this.getY(0);
            x += HallDrawingParameters.SEAT_RADIUS * 6 * i;
                   
            this.drawFigure(x, y, seatTypes[i].colorRgb);
            x += HallDrawingParameters.SEAT_RADIUS * 9 / 2;
            this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;
            this.context!.font = 
                (HallDrawingParameters.SEAT_RADIUS * 1.2).toString() + 'px roboto';
            this.context!.textAlign = 'center';
            this.context!.fillText(
                seatTypes[i].name,
                x,
                y + 5,
                HallDrawingParameters.SEAT_RADIUS * 6
            );
        }
    }

    private getX(place : number) {
        return HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3 
        + (place - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;
    }

    private getY(row : number) {
        return HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 3
            + (row - 1) * HallDrawingParameters.INDENT_BETWEEN_SEATS * 2;
    }

    private drawCross(x : number, y : number) {
        this.context!.beginPath();

        x = x - HallDrawingParameters.SEAT_RADIUS / 2;
        y = y - HallDrawingParameters.SEAT_RADIUS / 2;

        this.context!.moveTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
            y
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
            y
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
            y + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS,
            y + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS,
            y + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
            y + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
            y + HallDrawingParameters.SEAT_RADIUS,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
            y + HallDrawingParameters.SEAT_RADIUS,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
            y + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
        );
        this.context!.lineTo(
            x,
            y + HallDrawingParameters.SEAT_RADIUS / 2 + 2,
        );
        this.context!.lineTo(
            x,
            y + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
            y + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
        );
        this.context!.lineTo(
            x + HallDrawingParameters.SEAT_RADIUS / 2 - 2,
            y
        );
        this.context!.fill();
    }

    private drawSessionSeat(sessionSeat : SessionSeatModel) {
        this.context!.beginPath();

        let x = this.getX(sessionSeat.place);
        let y = this.getY(sessionSeat.row);

        if (sessionSeat.status === SessionSeatStatuses.Free) {
            return;
        }
        
        if (sessionSeat.status === SessionSeatStatuses.Ordered) {
            let startAngle = HallDrawingParameters.MIN_ANGLE;
            let endAngle = HallDrawingParameters.MAX_ANGLE;
            this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;

            this.context!.arc(
                x,
                y,
                HallDrawingParameters.SEAT_RADIUS / 2,
                startAngle,
                endAngle,
                true
            );
            this.context!.fill();
        }

        if (sessionSeat.status === SessionSeatStatuses.Taken) {
            this.drawCross(x, y);
        }
    }

    drawSessionSeats(sessionSeats : SessionSeatModel[], size : HallSizeModel) {
        for (let i = 1; i <= size.rowsNumber; i++) {
            for (let j = 1; j <= size.placesNumber; j++) {
                for (const sessionSeat of sessionSeats) {
                    if (sessionSeat.row === i && sessionSeat.place === j) {
                        this.drawSessionSeat(sessionSeat);
                        break;
                    }
                }
            }
        }    
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
        this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;
        this.context!.beginPath();

        let x = this.getX(place);
        let y = this.getY(row);

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

    drawFigure(x : number, y : number, colorRgb : string) {
        this.context!.beginPath();

        const fillStyle = this.context!.fillStyle;

        let startAngle = HallDrawingParameters.MIN_ANGLE;
        let endAngle = HallDrawingParameters.MAX_ANGLE;

        this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;

        this.context!.arc(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            startAngle,
            endAngle,
            true
        );
        
        this.context!.fill();

        this.context!.fillStyle = colorRgb;

        this.context!.arc(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            startAngle,
            endAngle,
            true
        );
        this.context!.fill();

        this.context!.fillStyle = fillStyle;
    }

    drawSeat(seat : SeatModel) : SeatDrawModel {
        let x = this.getX(seat.place);
        let y = this.getY(seat.row);

        let drawing = new DrawModel(
            x,
            y,
            HallDrawingParameters.SEAT_RADIUS,
            HallDrawingParameters.SEAT_RADIUS
        );

        this.drawFigure(x, y, seat.colorRgb);
        return new SeatDrawModel(drawing, seat);
    }

    private setCanvasSize(canvas : HTMLCanvasElement, size : HallSizeModel) {
        canvas.height = 
            (size.rowsNumber + 2) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;
        canvas.width = 
            (size.placesNumber + 2) * 2 * HallDrawingParameters.SEAT_RADIUS * 2;
    }

    private drawRect(canvas : HTMLCanvasElement) {
        this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;
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
        this.context!.fillStyle = HallDrawingParameters.COMMON_COLOR_RGB;
        this.context!.beginPath();

        let x = this.getX(place);
        let y = this.getY(row);

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
        let oldFillStyle = this.context!.fillStyle;
        this.context!.fillStyle = "rgb(0, 0, 200)";
        this.context!.font = 
                (HallDrawingParameters.SEAT_RADIUS * 1.2).toString() + 'px roboto';
        
        for (let i = 0; i < size.rowsNumber; i++) {
            let y = this.getY(i + 1);
            this.context!.fillText(
                String(i + 1),
                0,
                y
            );
        }
        this.context!.fillStyle = oldFillStyle;
    }

    private drawPlaceNumbers(canvas : HTMLCanvasElement, size : HallSizeModel) {
        let oldFillStyle = this.context!.fillStyle;

        this.context!.fillStyle = "rgb(0, 0, 200)";
        this.context!.font = 
                (HallDrawingParameters.SEAT_RADIUS * 1.2).toString() + 'px roboto';
        
        for (let i = 0; i < size.placesNumber; i++) {
            let x = this.getX(i + 1);
            this.context!.fillText(
                String(i + 1),
                x,
                HallDrawingParameters.INDENT_BETWEEN_SEAT_AND_HALL_BORDER * 2 / 3
            );
        }
        this.context!.fillStyle = oldFillStyle;
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