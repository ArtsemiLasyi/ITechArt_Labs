import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { SeatTypeModel } from 'src/app/Models/SeatTypeModel';
import { SeatTypeService } from 'src/app/Services/SeatTypeService';

@Component({
    selector : 'admin-seat-type-search',
    templateUrl : './admin-seat-type-search.component.html',
    providers : []
})
export class AdminSeatTypeSearchComponent {

    seatTypes : Observable<SeatTypeModel[]> | undefined;

    constructor (
        private seatTypeService: SeatTypeService
    ) { }

    getSeatTypes() {
        this.seatTypes = this.seatTypeService.getSeatTypes();
    }

    ngOnInit() {
        this.getSeatTypes();
    }
}
