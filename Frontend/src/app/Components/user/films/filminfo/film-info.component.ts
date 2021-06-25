import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmService } from 'src/app/Services/FilmService';

@Component({
    selector: 'films-filminfo',
    templateUrl: './filminfo.component.html',
    styleUrls: ['./filminfo.component.css'],
    providers: [FilmService]
})
export class FilmInfoComponent implements OnInit {

    film : FilmModel = new FilmModel();

    constructor(
        private filmService : FilmService,
        private route : ActivatedRoute) { }

    ngOnInit() {
        this.getFilm();
    }

    getFilm() {
        let id = parseInt(window.location.pathname.split('/').pop()!);
        this.filmService.getFilm(id).subscribe(
            (data) => {
                this.film = data as FilmModel;
            }
        );
  }
  
  getPoster(id : number) : string {
      return this.filmService.getPoster(id);
  }

}