import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { FilmRequest } from "../Requests/FilmRequest";
import * as Config from '../../../config.json';

@Injectable()
export class FilmService{
   
    private _films : string = '/films';
    
    constructor(private http: HttpClient){ }
 
    addFilm(film : FilmRequest) {         
        return this.http.post(Config.ApiUrl + this._films, film); 
    }

    editFilm(id : number, film : FilmRequest) {         
        return this.http.put(Config.ApiUrl + this._films + '/' + id, film); 
    }

    addPoster(id : number, poster : any) {
        return this.http.post(
            Config.ApiUrl + 
            this._films + 
            '/' + 
            id +
            '/poster',
            poster
        );
    }

    getPoster(id : number) : string {
        return Config.ApiUrl + 
            this._films + 
            '/' + 
            id +
            '/poster';
    }

    getFilms(pageNumber : number, pageSize : number) {
        return this.http.get(
            Config.ApiUrl + 
            this._films +
            "?" +
            "pageNumber=" +
            pageNumber.toString() +
            "&" +
            "pageSize=" +
            pageSize.toString()
        );
    }

    getFilm(id : number) {
        return this.http.get(Config.ApiUrl + this._films + '/' + id);
    }

    deleteFilm(id : number) {
        return this.http.delete(Config.ApiUrl + this._films + '/' + id);
    }
}