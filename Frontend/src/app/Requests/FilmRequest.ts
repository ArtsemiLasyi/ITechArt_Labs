export class FilmRequest {
    name : string;
    description : string;
    durationInMinutes : number;
    releaseYear : number;

    constructor(
        name : string,
        description : string,
        durationInMinutes : number,
        releaseYear : number
    ) {
        this.name = name;
        this.description = description;
        this.durationInMinutes = durationInMinutes;
        this.releaseYear = releaseYear;
    }
}