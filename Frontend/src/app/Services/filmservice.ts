import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { FilmModel } from "../Models/FilmModel";
import { FilmRequest } from "../Requests/FilmRequest";

@Injectable()
export class FilmService {

    constructor(private http : HttpClient){ }

    addFilm(film : FilmRequest) {         
        return this.http.post(ApiUrls.Films, film); 
    }

    editFilm(id : number, film : FilmRequest) {         
        return this.http.put(ApiUrls.Films + '/' + id, film); 
    }

    addPoster(id : number, poster : any) {
        return this.http.post(
            ApiUrls.Films
            + '/'
            + id
            + '/poster',
            poster
        );
    }

    getPoster(id : number) : string {
        return ApiUrls.Films
            + '/' 
            + id
            + '/poster';
    }

    getFilms(pageNumber : number, pageSize : number) {
        return this.http.get<FilmModel[]>(
            ApiUrls.Films
            + "?"
            + "pageNumber="
            + pageNumber.toString()
            + "&"
            + "pageSize="
            + pageSize.toString()
        );
    }

    getFilm(id : number) {
        return this.http.get(ApiUrls.Films + '/' + id);
    }

    deleteFilm(id : number) {
        return this.http.delete(ApiUrls.Films + '/' + id);
    }
}