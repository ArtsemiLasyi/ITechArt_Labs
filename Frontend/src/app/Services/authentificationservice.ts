import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { SignInRequest } from "../Requests/SignInRequest";
import { SignUpRequest } from "../Requests/SignUpRequest";
import { ConfigurationService } from "./ConfigurationService";

@Injectable()
export class AuthentificationService{
   
    constructor(private http: HttpClient){ }
 
    signIn(request: SignInRequest) {         
        return this.http.post(ConfigurationService.URL_SIGNIN, request); 
    }

    signUp(request: SignUpRequest) {         
        return this.http.post(ConfigurationService.URL_SIGNUP, request); 
    }
}