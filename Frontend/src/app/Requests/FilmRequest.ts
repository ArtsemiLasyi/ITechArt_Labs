export class FilmRequest {
    name : string;
    description : string;
    releaseYear : number;
    durationInMinutes : number;
    poster : File | undefined;
    
    constructor(
        name : string,
        description : string,
        releaseYear : number,
        durationInMinutes : number){
        
        this.name = name;
        this.description = description;
        this.releaseYear = releaseYear;
        this.durationInMinutes = durationInMinutes;
    }
}