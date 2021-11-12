import { Injectable } from "@angular/core";
import { SessionModel } from "../Models/SessionModel";

@Injectable()
export class DateTimeService {

    getTime(date : Date) {
        return new Date(date).toLocaleTimeString(
            [], {
                hour : '2-digit',
                minute :'2-digit'
            }
        );
    }

    getDate(date : Date) {
        return new Date(date).toLocaleDateString();
    }
}