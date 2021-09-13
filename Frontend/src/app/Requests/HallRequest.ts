export class HallRequest {
    cinemaId : number;
    name : string;

    constructor(
        cinemaId : number,
        name : string
    ) {
        
        this.name = name;
        this.cinemaId = cinemaId;
    }
}