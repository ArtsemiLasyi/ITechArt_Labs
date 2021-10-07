import { Injectable, Injector } from '@angular/core';
import {
    HttpEvent,
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpResponse,
    HttpErrorResponse
} from '@angular/common/http';
import { StatusCodes } from 'http-status-codes';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { StorageService } from '../Services/StorageService';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

    constructor (
        private storageService : StorageService,
        private injector : Injector
    ) { }

    intercept (
        request : HttpRequest<any>,
        next : HttpHandler) : Observable<HttpEvent<any>> {

        return next.handle(request).pipe(
            retry(1),
            catchError(
                (error : HttpErrorResponse) => {

                    if (error.error.errorText) {
                        return throwError(error.error.errorText);
                    }
                    if (error.status === StatusCodes.UNAUTHORIZED) {
                        this.storageService.deleteEmail();
                        return throwError(error);
                    }
                    if (error.error.errors) {
                        let message = '';
                        for (let key in error.error.errors) {
                            message += error.error.errors[key] + '.';
                        }
                        return throwError(message);
                    }
                    return throwError(error.error.title);                 
                }
            )
        )
    }
}