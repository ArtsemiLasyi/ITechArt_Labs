import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { UserRequest } from "../Requests/UserRequest";
import { ConfigurationService } from "./ConfigurationService";

@Injectable()
export class UserService{
    
    constructor(private http: HttpClient){ }
 
    editUser(id : number, user: UserRequest) {         
        return this.http.put(ConfigurationService.URL_USERS + '/' + id, user); 
    }

    getCurrentUser() {
        return this.http.get(ConfigurationService.URL_USERS + '/current');       
    }

    getUser(id : number) {
        return this.http.get(ConfigurationService.URL_USERS + '/' + id);
    }

    deleteUser(id : number) {
        return this.http.delete(ConfigurationService.URL_USERS + '/' + id);
    }
}