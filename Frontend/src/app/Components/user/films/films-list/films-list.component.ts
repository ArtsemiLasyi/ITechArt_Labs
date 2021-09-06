import { HostListener } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmSearchRequest } from 'src/app/Requests/FilmSearchRequest';
import { FilmService } from 'src/app/Services/FilmService';
import { PageService } from 'src/app/Services/pageservice';

@Component({
    selector: 'films-films-list',
    templateUrl: './films-list.component.html',
    styleUrls: ['./films-list.component.scss'],
    providers: [
        FilmService,
        PageService
    ]
})
export class FilmsListComponent implements OnInit {

    films : Observable<FilmModel[]> | undefined;
    filmName : string | undefined;
    firstSessionDate : Date | undefined;
    lastSessionDate : Date | undefined;

    constructor (
        private filmService: FilmService,
        private pageService : PageService
    ) { }

    getFilms(request = new FilmSearchRequest()) {
        this.films = this.filmService
            .getFilms(
                this.pageService.getPageNumber(),
                this.pageService.getPageSize(),
                request
            );
    }

    getPoster(id : number) {
        return this.filmService.getPoster(id);
    }

    ngOnInit() {
        this.getFilms();
    }
  
    getMoreFilms() {
        this.pageService.nextPage();
        this.getFilms();
    }

    searchFilms() {
        this.pageService.clearPageNumber();
        let request = new FilmSearchRequest();
        request.filmName = this.filmName;
        request.firstSessionDateTime = this.firstSessionDate;
        request.lastSessionDateTime = this.lastSessionDate;
        this.getFilms(request);
    }
}
