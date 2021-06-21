import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import * as Config from '../../../config.json';
import { SignInRequest } from "../Requests/SignInRequest";
import { SignUpRequest } from "../Requests/SignUpRequest";

@Injectable()
export class AuthentificationService{
   
    private _authentification : string = '/account';
    private _signIn : string = '/signin';
    private _signUp : string = '/signup';
    
    constructor(private http: HttpClient){ }
 
    signIn(request: SignInRequest) {         
        return this.http.post(Config.ApiUrl + this._authentification + this._signIn, request); 
    }

    signUp(request: SignUpRequest) {         
        return this.http.post(Config.ApiUrl + this._authentification + this._signUp, request); 
    }
}