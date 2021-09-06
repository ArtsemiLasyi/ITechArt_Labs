import { HttpErrorResponse } from "@angular/common/http";
import { Injector, NgZone } from "@angular/core";
import { ErrorHandler, Injectable } from "@angular/core";
import { ErrorService } from "../Services/ErrorService";

@Injectable()
export class GlobalErrorHandler extends ErrorHandler {

    constructor(private injector: Injector) {
        super();
    }

    handleError(error: Error) {
        const errorService = this.injector.get(ErrorService);
        let message;
        let stackTrace;

        if (!(error instanceof HttpErrorResponse)) {
            message = errorService.getClientMessage(error);
            stackTrace = errorService.getClientStack(error);
        }

        console.error(message);
    }
}