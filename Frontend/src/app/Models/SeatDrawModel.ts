import { DrawModel } from "./DrawModel";
import { SeatModel } from "./SeatModel";

export class SeatDrawModel {
    drawing : DrawModel;
    seat : SeatModel | undefined;

    constructor(
        drawing : DrawModel,
        seat : SeatModel | undefined = undefined
    ) {
        this.drawing = drawing;
        this.seat = seat;
    }
}