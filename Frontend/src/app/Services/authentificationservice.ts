import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { SignInRequest } from "../Requests/SignInRequest";
import { SignUpRequest } from "../Requests/SignUpRequest";
import { ApiUrls } from "../Constants/ApiUrls";

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