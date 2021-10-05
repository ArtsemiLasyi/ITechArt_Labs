import { Injectable } from "@angular/core";
import { SessionModel } from "../Models/SessionModel";

@Injectable()
export class DateTimeService {

    getTime(date : Date) {
        return new Date(date).toLocaleTimeString();
    }

    getDate(date : Date) {
        return new Date(date).toLocaleDateString();
    }
}