import { Injectable } from "@angular/core";
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from "@angular/router";
import {Observable, of} from "rxjs";
import { UserModel } from "../Models/UserModel";
import { UserService } from "../Services/UserService";
import { catchError, map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class UserInfoGuard implements CanActivate {

    constructor(
        private service : UserService,
        private router : Router
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<boolean> | boolean {
        
        let flag = false;

        return this.service
            .getCurrentUser()
            .pipe(
                map(
                    (data : UserModel) => {
                        if (data.id) {
                            flag = true;
                        }
                        return flag;
                    }
                ),
                catchError(
                    (err) => {
                        this.router.navigate(['/account/signin']);
                        return of(false);
                    }
                )
            );    
        
    }
}