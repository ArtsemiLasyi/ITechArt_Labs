import { HostListener } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FilmModel } from 'src/app/Models/FilmModel';
import { FilmSearchRequest } from 'src/app/Requests/FilmSearchRequest';
import { FilmService } from 'src/app/Services/filmservice';
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

    films: FilmModel[] = [];

    constructor (
        private filmService: FilmService,
        private pageService : PageService
    ) { }

    getFilms() {
        let request = new FilmSearchRequest();
        request.filmName = "";
        this.filmService
            .getFilms(
                this.pageService.getPageNumber(),
                this.pageService.getPageSize(),
                request
            )
            .subscribe(
                (data) =>  {
                    this.films = this.films.concat(data);
                }
            )
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
}
