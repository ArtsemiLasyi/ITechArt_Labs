import { NgZone } from "@angular/core";
import { ErrorHandler, Injectable } from "@angular/core";

@Injectable()
export class GlobalErrorHandler extends ErrorHandler {

    constructor() {
        super();
    }

    handleError(error: Error) {
        // Will be in future PR
    }
}