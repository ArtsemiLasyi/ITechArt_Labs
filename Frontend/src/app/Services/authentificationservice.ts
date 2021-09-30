import { Injectable } from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpResponse} from '@angular/common/http';
import { SignInRequest } from "../Requests/SignInRequest";
import { SignUpRequest } from "../Requests/SignUpRequest";
import { ApiUrls } from "../Constants/ApiUrls";
import { catchError } from "rxjs/operators";
import { throwError } from "rxjs";

@Injectable()
export class AuthentificationService {
   
    constructor(private http : HttpClient){ }
 
    signIn(request : SignInRequest) {         
        return this.http.post(ApiUrls.SignIn, request);
    }

    signUp(request : SignUpRequest) {         
        return this.http.post(ApiUrls.SignUp, request); 
    }
}