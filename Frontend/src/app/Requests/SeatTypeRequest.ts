export class SeatTypeRequest {
    name : string;
    colorRgb : string;

    constructor(name : string, color : string) {
        this.name = name;
        this.colorRgb = color;
    }
}