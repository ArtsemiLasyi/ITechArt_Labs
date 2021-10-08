  
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalErrorHandler } from 'src/app/ErrorHandlers/GlobalErrorHandler';
import { CinemaModel } from 'src/app/Models/CinemaModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { CinemaService } from 'src/app/Services/CinemaService';

@Component({
    selector : 'cinemas-cinemainfo',
    templateUrl : './cinema-info.component.html',
    styleUrls : [],
    providers : [CinemaService, GlobalErrorHandler]
})
export class CinemaInfoComponent implements OnInit {

    cinema : CinemaModel = new CinemaModel();

    constructor(
        private cinemaService : CinemaService,
        private route : ActivatedRoute,
        private handler : GlobalErrorHandler
    ) { }

    ngOnInit() {
        this.getCinema();
    }

    getCinema() {
        this.route.params.subscribe(params => this.cinema.id = params['id']);
        this.cinemaService.getCinema(this.cinema.id).subscribe(
            (data) => {
                this.cinema = data;
            },
            (error) => {
                this.handler.handleError(error);
            }
        );
    }
  
    getPhoto(id : number) : string {
        return this.cinemaService.getPhoto(id);
    }

}