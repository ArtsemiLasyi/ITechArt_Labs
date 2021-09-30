import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SeatTypeModel } from 'src/app/Models/SeatTypeModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { SeatTypeRequest } from 'src/app/Requests/SeatTypeRequest';
import { SeatTypeService } from 'src/app/Services/SeatTypeService';

@Component({
    selector: 'admin-seat-type-info',
    templateUrl: './admin-seat-type-info.component.html',
    providers: [SeatTypeService]
})
export class AdminSeatTypeInfoComponent implements OnInit {

    model = new SeatTypeModel();
    error = new ErrorModel();
    success = new SuccessModel();

    constructor (
        private seatTypeService: SeatTypeService,
        private activateRoute: ActivatedRoute
    ) { }

    ngOnInit() {
        this.model.id = this.activateRoute.snapshot.params['id'];
        this.seatTypeService
            .getSeatType(this.model.id)
            .subscribe(
                (seatType : SeatTypeModel) => {
                    this.model = seatType;
                }
            )
    }

    editSeatType() {
        let request = new SeatTypeRequest(
            this.model.name,
            this.model.colorRgb
        );
        console.log(this.model);
        console.log(this.model.colorRgb.length);
        this.seatTypeService
            .editSeatType(this.model.id, request)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error) => {
                    this.error.exists = true;
                    this.error.text = error;
                }
            );
    }

    deleteSeatType() {
        this.seatTypeService
            .deleteSeatType(this.model.id)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error) => {
                    this.error.exists = true;
                    this.error.text = error;
                }
            );
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}