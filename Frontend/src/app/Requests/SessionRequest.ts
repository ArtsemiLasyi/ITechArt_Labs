export class SessionRequest {
    filmId : number;
    hallId : number;
    startDateTime : Date;
    
    constructor(filmId : number, hallId : number, startDateTime : Date) {
        this.filmId = filmId;
        this.hallId = hallId;
        this.startDateTime = startDateTime;
    }
}