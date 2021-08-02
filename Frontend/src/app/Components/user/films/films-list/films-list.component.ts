import { Component, OnInit } from '@angular/core';
import { FilmModel } from 'src/app/Models/FilmModel';
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

    getPoster(id : number) {
        return this.filmService.getPoster(id);
    }

    ngOnInit() {
        this.getFilms();
    }
  
    onScroll(event : any) {
        console.log("a");
        this.pageService.nextPage();
        this.getFilms();
    }
}
