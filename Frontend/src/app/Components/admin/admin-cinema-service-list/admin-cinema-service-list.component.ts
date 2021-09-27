import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CinemaServiceModel } from 'src/app/Models/CinemaServiceModel';
import { CinemaServiceService } from 'src/app/Services/CinemaServiceService';

@Component({
    selector : 'admin-cinema-service-list',
    templateUrl : './admin-cinema-service-list.component.html',
    providers : [CinemaServiceService]
})
export class AdminCinemaServiceListComponent implements OnInit {

    @Input() cinemaId = 0;
    cinemaServices: Observable<CinemaServiceModel[]> | undefined;

    constructor(
        private cinemaServiceService : CinemaServiceService
    ) { }

    ngOnInit(): void {
        this.getCinemaServices(this.cinemaId);
    }

    getCinemaServices(cinemaId : number) {
        this.cinemaServices = this.cinemaServiceService.getCinemaServices(cinemaId);
    }
}
