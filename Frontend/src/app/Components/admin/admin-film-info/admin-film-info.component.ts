  
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { FilmModel } from 'src/app/Models/FilmModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { FilmRequest } from 'src/app/Requests/FilmRequest';
import { FilmService } from 'src/app/Services/FilmService';

@Component({
    selector: 'admin-film-info',
    templateUrl: './admin-film-info.component.html',
    providers: [
        FilmService
    ]
})
export class AdminFilmInfoComponent implements OnInit {

    readonly defaultFileName : string = "Add poster";

    poster : File | undefined;
    model = new FilmModel();
    error = new ErrorModel();
    success = new SuccessModel();
    selectedFileName : string = this.defaultFileName;

    constructor (
        private filmService: FilmService,
        private activateRoute: ActivatedRoute
    ) { }

    loadPhoto(event : any) : any {
        this.poster = event.target.files[0];
        if (this.poster === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.poster.name; 
    }

    ngOnInit() {
        this.model.id = this.activateRoute.snapshot.params['id'];
        this.filmService
            .getFilm(this.model.id)
            .subscribe(
                (film : FilmModel) => {
                    this.model = film;
                }
            )
    }


    editFilm() {
        let request = new FilmRequest(
            this.model.name,
            this.model.description,
            this.model.durationInMinutes,
            this.model.releaseYear
        );
        this.filmService
            .editFilm(this.model.id, request)
            .subscribe(
                () => {
                    this.success.flag = true;
                }
            );
    }

    deleteFilm() {
        this.filmService
            .deleteFilm(this.model.id)
            .subscribe(
                () => {
                    this.success.flag = true;
                }
            );
    }
}