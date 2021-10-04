import { SeatRequest } from "./SeatRequest";

export class SeatsRequest {
    value : SeatRequest[];

    constructor(value : SeatRequest[]) {
        this.value = value;
    }
}