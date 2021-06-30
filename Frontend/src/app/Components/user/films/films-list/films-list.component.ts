import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'films-films-list',
    templateUrl: './films-list.component.html',
    styleUrls: ['./films-list.component.scss']
})
export class FilmsListComponent implements OnInit {

    constructor () { }

    getFilms() {
        // Todo
    }

    getPoster(id : number) {
        // Todo
    }

    ngOnInit() {
        this.getFilms();
    }
}
