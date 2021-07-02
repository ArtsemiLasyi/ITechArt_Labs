import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import { UserRequest } from "../Requests/UserRequest";
import { ApiUrls } from "../Constants/ApiUrls";

@Injectable()
export class UserService{
    
    constructor(private http : HttpClient){ }
 
    editUser(id : number, user : UserRequest) {         
        return this.http.put(ApiUrls.Users + '/' + id, user); 
    }

    getCurrentUser() {
        return this.http.get(ApiUrls.Users + '/current');       
    }

    getUser(id : number) {
        return this.http.get(ApiUrls.Users + '/' + id);
    }

    deleteUser(id : number) {
        return this.http.delete(ApiUrls.Users + '/' + id);
    }
}