export class CinemaRequest {
    name : string;
    description : string;
    cityName : string;

    constructor(
        name : string,
        description : string,
        cityName : string) {
        
        this.name = name;
        this.description = description;
        this.cityName = cityName;
    }
}