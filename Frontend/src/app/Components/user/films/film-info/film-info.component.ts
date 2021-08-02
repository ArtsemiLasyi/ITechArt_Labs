  
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmService } from 'src/app/Services/filmservice';

@Component({
    selector: 'films-filminfo',
    templateUrl: './film-info.component.html',
    providers: [FilmService]
})
export class FilmInfoComponent implements OnInit {

    film : FilmModel = new FilmModel();
    error = new ErrorModel();

    constructor(
        private filmService : FilmService,
        private route : ActivatedRoute
    ) { }

    ngOnInit() {
        this.getFilm();
    }

    getFilm() {
        this.route.params.subscribe(params => this.film.id = params['id']);
        this.filmService.getFilm(this.film.id).subscribe(
            (data) => {
                this.film = data as FilmModel;
            },
            (error) => {
                error.exists = true;
            }
        );
    }
  
    getPoster(id : number) : string {
        return this.filmService.getPoster(id);
    }

}