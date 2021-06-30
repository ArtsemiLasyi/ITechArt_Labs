import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageKeyNames } from 'src/app/Constants/LocalStorageKeyNames';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SignInModel } from 'src/app/Models/SignInModel';
import { SignInRequest } from 'src/app/Requests/SignInRequest';
import { AuthentificationService } from 'src/app/Services/AuthentificationService';

@Component({
    selector: 'account-signin',
    templateUrl: './signin.component.html',
    styleUrls: ['./signin.component.scss'],
    providers: [ AuthentificationService]
})
export class SignInComponent {

    constructor(
        private service : AuthentificationService,
        private router : Router
    ) { }

    model = new SignInModel();
    error = new ErrorModel();

    signIn() {
        let request = new SignInRequest(this.model.email, this.model.password);
        this.service.signIn(request)
            .subscribe(
                (data : any) => {
                    let token = data.token;
                    localStorage.setItem(LocalStorageKeyNames.TOKEN, token);
                    this.router.navigate(['']);
                },
                error => {
                    this.error.exists = true;
                    this.error.text = this.getError(error);
                }
            );  
    }

    getError(error : any) : string {
        return error.error.errorText 
            || error.error.errors.Email
            || error.error.errors.Password
            || error.error.title;     
    }
}