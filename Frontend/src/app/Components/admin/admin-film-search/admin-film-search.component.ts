  
import { Component, OnInit } from '@angular/core';
import { FilmModel } from 'src/app/Models/FilmModel';
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

    films : FilmModel[] = [];
    text : string = "";

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
                    this.films = this.films.concat(data);
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
    
    search() {
        console.log(this.text);
        // Todo
    }
}