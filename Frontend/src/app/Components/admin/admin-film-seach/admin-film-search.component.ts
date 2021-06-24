import { Component, OnInit } from '@angular/core';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmService } from 'src/app/Services/FilmService';
import { PageService } from 'src/app/Services/PageService';

@Component({
    selector: 'admin-film-search',
    templateUrl: './admin-film-search.component.html',
    styleUrls: ['./admin-film-search.component.css'],
    providers: [
        FilmService,
        PageService
    ]
})
export class AdminFilmSearchComponent implements OnInit {

    films: FilmModel[] = [];

    constructor (
        private filmService: FilmService,
        private pageService : PageService) { }

    getFilms() {
        this.filmService
            .getFilms(
                this.pageService.getPageNumber(),
                this.pageService.getPageSize()
            )
            .subscribe(
                (data) =>  {
                    this.films = this.films.concat(data as FilmModel[]);
                }
            )
    }

    getPoster(id : number) : string {
        return this.filmService.getPoster(id);
    }

    ngOnInit() {
        this.getFilms();
    }
  
    onScroll(event : any) {
        this.pageService.nextPage();
        this.getFilms();
    }   
}