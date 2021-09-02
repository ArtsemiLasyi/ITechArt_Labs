  
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmSearchRequest } from 'src/app/Requests/FilmSearchRequest';
import { FilmService } from 'src/app/Services/filmservice';
import { PageService } from 'src/app/Services/pageservice';

@Component({
    selector: 'admin-film-search',
    templateUrl: './admin-film-search.component.html',
    styleUrls: ['./admin-film-search.component.scss'],
    providers: [
        FilmService,
        PageService
    ]
})
export class AdminFilmSearchComponent implements OnInit {

    films : Observable<FilmModel[]> | undefined;
    filmName : string = "";

    constructor (
        private filmService: FilmService,
        private pageService : PageService
    ) { }

    getFilms() {
        this.films = this.filmService
            .getFilms(
                this.pageService.getPageNumber(),
                this.pageService.getPageSize(),
                new FilmSearchRequest()
            );
    }

    getPoster(id : number) : string {
        return this.filmService.getPoster(id);
    }

    ngOnInit() {
        this.getFilms();
    }
  
    getMoreFilms() {
        this.pageService.nextPage();
        this.getFilms();
    }
    
    search(filmName : string) {
        let request = new FilmSearchRequest();
        request.filmName = filmName;
        this.pageService.clearPageNumber();
        this.filmService.getFilms(
            this.pageService.getPageNumber(),
            this.pageService.getPageSize(),
            request
        );
    }
}