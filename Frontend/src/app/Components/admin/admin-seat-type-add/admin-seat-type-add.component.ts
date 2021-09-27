import { Component, OnInit } from '@angular/core';
import { SeatTypeModel } from 'src/app/Models/SeatTypeModel';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { SeatTypeRequest } from 'src/app/Requests/SeatTypeRequest';
import { SeatTypeService } from 'src/app/Services/SeatTypeService';
@Component({
    selector: 'admin-seat-type-add',
    templateUrl: './admin-seat-type-add.component.html',
    providers: [SeatTypeService]
})
export class AdminSeatTypeAddComponent {

    model : SeatTypeModel = new SeatTypeModel();
    error : ErrorModel = new ErrorModel();
    success : SuccessModel = new SuccessModel();

    constructor(private seatTypeService : SeatTypeService) { }

    addSeatType() {
        const request = new SeatTypeRequest(
            this.model.name,
            this.model.colorRgb
        );
        this.seatTypeService.addSeatType(request).subscribe(
            () => {
                this.success.flag = true;
            },
            (error : Error) => {
                this.error.exists = true;
            }
        )
    }

    clearForm(event : Event) {
        this.success.flag = false;
        this.error.exists = false;
    }
}