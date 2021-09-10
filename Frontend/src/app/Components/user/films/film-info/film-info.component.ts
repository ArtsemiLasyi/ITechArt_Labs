  
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalErrorHandler } from 'src/app/ErrorHandlers/GlobalErrorHandler';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmService } from 'src/app/Services/FilmService';

@Component({
    selector: 'films-filminfo',
    templateUrl: './film-info.component.html',
    styleUrls: ['./film-info.component.scss'],
    providers: [FilmService, GlobalErrorHandler]
})
export class FilmInfoComponent implements OnInit {

    film : FilmModel = new FilmModel();

    constructor(
        private filmService : FilmService,
        private route : ActivatedRoute,
        private handler : GlobalErrorHandler) 
    { }

    ngOnInit() {
        this.getFilm();
    }

    getFilm() {
        this.route.params.subscribe(params => this.film.id = params['id']);
        this.filmService.getFilm(this.film.id).subscribe(
            (data) => {
                this.film = data;
            },
            (error) => {
                this.handler.handleError(error);
            }
        );
    }
  
    getPoster(id : number) : string {
        return this.filmService.getPoster(id);
    }

}