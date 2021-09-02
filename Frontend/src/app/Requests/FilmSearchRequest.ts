export class FilmSearchRequest {
    filmName : string = "";
    cinemaId : number = 0;
    firstSessionDateTime : Date = new Date();
    lastSessionDateTime : Date = new Date();
}