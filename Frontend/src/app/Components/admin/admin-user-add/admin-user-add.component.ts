import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SignUpModel } from 'src/app/Models/SignUpModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { SignUpRequest } from 'src/app/Requests/SignUpRequest';
import { AuthentificationService } from 'src/app/Services/AuthentificationService';

@Component({
    selector: 'admin-user-add',
    templateUrl: './admin-user-add.component.html',
    providers: [AuthentificationService]
})
export class AdminUserAddComponent {

    constructor(
        private service : AuthentificationService,
        private router : Router
    ) { }

    model = new SignUpModel();
    error = new ErrorModel();
    success = new SuccessModel();

    addUser() {
        let request = new SignUpRequest(this.model.email, this.model.password);
        this.service.signUp(request)
            .subscribe(
                () => {
                    this.router.navigate(['/account/signin']);
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