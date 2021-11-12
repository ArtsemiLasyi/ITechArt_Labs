import { HostListener } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, reduce, scan } from 'rxjs/operators';
import { FilmModel } from 'src/app/Models/FilmModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { FilmSearchRequest } from 'src/app/Requests/FilmSearchRequest';
import { FilmService } from 'src/app/Services/FilmService';
import { PageService } from 'src/app/Services/pageservice';

@Component({
    selector : 'films-films-list',
    templateUrl : './films-list.component.html',
    styleUrls : ['./films-list.component.scss'],
    providers : [
        FilmService,
        PageService
    ]
})
export class FilmsListComponent implements OnInit {

    term = new BehaviorSubject<FilmSearchRequest>(new FilmSearchRequest());
    films : Observable<FilmModel[]> = this.term.pipe(
        autocomplete(
            100, 
            (request : FilmSearchRequest) => 
                this.filmService.getFilms(
                    this.pageService.getPageNumber(),
                    this.pageService.getPageSize(),
                    request
                ).pipe(
                    map(
                        (films) => {
                            if (!films.length) {
                                this.allFilmsAreShown = true;
                            }
                            films = this.oldFilms.concat(films);
                            this.oldFilms = films;
                            return films;
                        }
                    )
                )
        )
    );
    oldFilms : FilmModel[] = [];

    filmName : string | undefined;

    allFilmsAreShown : boolean = false;

    constructor (
        private filmService: FilmService,
        private pageService : PageService
    ) { }

    getFilms(request = new FilmSearchRequest()) {
        this.term.next(request);
    }

    getPoster(id : number) {
        return this.filmService.getPoster(id);
    }

    ngOnInit() {
        this.getFilms();
    }
  
    getMoreFilms() {
        if (!this.allFilmsAreShown) {
            this.pageService.nextPage();
            this.getFilms();
        }
    }

    searchFilms() {
        this.allFilmsAreShown = false;
        this.oldFilms = [];
        this.pageService.clearPageNumber();
        let request = new FilmSearchRequest();
        request.filmName = this.filmName;
        this.getFilms(request);
    }
}
