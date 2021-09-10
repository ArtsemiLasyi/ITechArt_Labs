import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiUrls } from "../Constants/ApiUrls";
import { FilmModel } from "../Models/FilmModel";
import { FilmRequest } from "../Requests/FilmRequest";
import { FilmSearchRequest } from "../Requests/FilmSearchRequest";

@Injectable()
export class FilmService {

    constructor(private http : HttpClient){ }

    addFilm(film : FilmRequest) {         
        return this.http.post(ApiUrls.Films, film); 
    }

    editFilm(id : number, film : FilmRequest) {         
        return this.http.put(
            `${ApiUrls.Films}/${id}`,
            film
        ); 
    }

    addPoster(id : number, poster : any) {
        return this.http.post(
            `${ApiUrls.Films}/${id}/poster`,
            poster
        );
    }

    getPoster(id : number) : string {
        return `${ApiUrls.Films}/${id}/poster`;
    }

    getFilms(pageNumber : number, pageSize : number, request : FilmSearchRequest) {

        return this.http.get<FilmModel[]>(
            ApiUrls.Films, {
                params : this.getParams(pageNumber, pageSize, request) 
            }
        );
    }

    getFilm(id : number) {
        return this.http.get<FilmModel>(`${ApiUrls.Films}/${id}`);
    }

    deleteFilm(id : number) {
        return this.http.delete(`${ApiUrls.Films}/${id}`);
    }

    private getParams(pageNumber : number, pageSize : number, request : FilmSearchRequest) {
        let params : any = { };
        if (request.filmName) {
            params.filmName = request.filmName;
        }
        if (request.firstSessionDateTime) {
            params.firstSessionDateTime = request.firstSessionDateTime;
        }
        if (request.lastSessionDateTime) {
            params.lastSessionDateTime = request.lastSessionDateTime;
        }
        params.pageSize = pageSize.toString();
        params.pageNumber = pageNumber.toString();
        return params;
    }
}