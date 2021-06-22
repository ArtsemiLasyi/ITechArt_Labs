import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import * as Config from '../../../config.json';
import { UserRequest } from "../Requests/UserRequest";

@Injectable()
export class UserService{
   
    private _users : string = '/users';
    
    constructor(private http: HttpClient){ }
 
    editUser(id : number, user: UserRequest) {         
        return this.http.put(Config.ApiUrl + this._users + '/id', user); 
    }


    getCurrentUser() {
        return this.http.get(Config.ApiUrl + this._users + '/current');       
    }

    getUser(id : number) {
        return this.http.get(Config.ApiUrl + this._users + '/id');
    }

    deleteUser(id : number) {
        return this.http.delete(Config.ApiUrl + this._users + '/id');
    }
}