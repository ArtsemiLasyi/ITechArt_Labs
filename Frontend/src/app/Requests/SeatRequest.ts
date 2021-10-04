export class SeatRequest {
    row : number;
    place : number;
    seatTypeId : number;

    constructor(seatTypeId : number, row : number, place : number) {
        this.place = place;
        this.row = row;
        this.seatTypeId = seatTypeId;
    }
}