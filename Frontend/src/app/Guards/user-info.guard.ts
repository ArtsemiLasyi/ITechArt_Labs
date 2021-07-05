import { Injectable } from "@angular/core";
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from "@angular/router";
import {Observable} from "rxjs";
import { UserModel } from "../Models/UserModel";
import { UserService } from "../Services/UserService";

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

        this.service.getCurrentUser()
            .subscribe(
                (data : UserModel) => {
                    flag = true;
                },
                error => {
                    flag = false;
                    this.router.navigate(['/account/signin']);
                }
            );
        return flag;
    }
}