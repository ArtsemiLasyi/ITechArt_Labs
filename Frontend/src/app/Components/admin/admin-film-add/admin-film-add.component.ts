import { Component, OnInit } from '@angular/core';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { FilmModel } from 'src/app/Models/FilmModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { FilmRequest } from 'src/app/Requests/FilmRequest';
import { FilmService } from 'src/app/Services/filmservice';

@Component({
    selector: 'admin-film-add',
    templateUrl: './admin-film-add.component.html',
    providers: [FilmService]
})
export class AdminFilmAddComponent {

    readonly defaultFileName : string = "Add poster";

    poster : File | undefined;
    model : FilmModel = new FilmModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();
    selectedFileName : string = this.defaultFileName;

    constructor(private filmService : FilmService) { }

    loadPhoto(event : any) : any {
        this.poster = event.target.files[0];
        if (this.poster === undefined) {
            this.selectedFileName = this.defaultFileName;
            return;
        }
        this.selectedFileName = this.poster.name; 
    }

    addFilm() {
        const request = new FilmRequest(
            this.model.name,
            this.model.description,
            this.model.durationInMinutes,
            this.model.releaseYear
        );
        this.filmService.addFilm(request).subscribe(
            (data : any) => {
                console.log(data);
                const id = data.id;
                const formData = new FormData();
                formData.append("formFile", this.poster!);  
                this.filmService.addPoster(id, formData).subscribe(
                    () => {
                        this.success.flag = true;
                    }
                )
            }
        )
    }
}