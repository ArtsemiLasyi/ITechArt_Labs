  
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FilmModel } from 'src/app/Models/FilmModel';
import { autocomplete } from 'src/app/Operators/autocomplete.operator';
import { FilmSearchRequest } from 'src/app/Requests/FilmSearchRequest';
import { FilmService } from 'src/app/Services/FilmService';
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

    term = new BehaviorSubject<FilmSearchRequest>(new FilmSearchRequest());
    films : Observable<FilmModel[]> = this.term.pipe(
        autocomplete(
            500, 
            (request : FilmSearchRequest) => 
                this.filmService.getFilms(
                    this.pageService.getPageNumber(),
                    this.pageService.getPageSize(),
                    request
                )
        )
    );
    filmName : string = '';

    constructor (
        private filmService: FilmService,
        private pageService : PageService
    ) { }

    getFilms(request = new FilmSearchRequest()) {
        this.term.next(request);
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
    
    search() {
        let request = new FilmSearchRequest();
        request.filmName = this.filmName;
        this.pageService.clearPageNumber();
        this.term.next(request);
    }
}
