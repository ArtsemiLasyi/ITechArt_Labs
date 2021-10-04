import { Injectable } from "@angular/core";
import { HallSizeModel } from "../Models/HallSizeModel";
import { SeatModel } from "../Models/SeatModel";

@Injectable()
export class HallSizeService {

    getHallSize(seats : SeatModel[]) : HallSizeModel {
        let result = new HallSizeModel();
        for (let seat of seats) {
            if (seat.row > result.rowsNumber) {
                result.rowsNumber = seat.row;
            }
            if (seat.place > result.placesNumber) {
                result.placesNumber = seat.place;
            }
        }
        return result;
    }

    increaseRowsNumber(size : HallSizeModel) {
        size.rowsNumber++;
    }
    
    increasePlacesNumber(size : HallSizeModel) {
        size.placesNumber++;
    }

    decreaseRowsNumber(size : HallSizeModel) {
        size.rowsNumber--;
    }

    decreasePlacesNumber(size : HallSizeModel) {
        size.placesNumber--;
    }
}