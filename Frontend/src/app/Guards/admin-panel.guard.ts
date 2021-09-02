import { Injectable } from "@angular/core";
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable, of } from "rxjs";
import { UserRoles } from "../Models/UserRoles";
import { UserModel } from "../Models/UserModel";
import { UserService } from "../Services/UserService";
import { catchError, map } from "rxjs/operators";

@Injectable({
    providedIn : 'root'
})
export class AdminPanelGuard implements CanActivate{
 
    constructor(
        private service : UserService,
        private router : Router
    ) { }

    model = new UserModel();

    canActivate(
        route : ActivatedRouteSnapshot,
        state : RouterStateSnapshot
    ) : Observable<boolean> | boolean {

        let flag = false;

        return this.service.getCurrentUser()
            .pipe(
                map(
                    (data : UserModel) => {
                        this.model.role = data.role;
                        if (this.model.role === UserRoles.Administrator) {
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