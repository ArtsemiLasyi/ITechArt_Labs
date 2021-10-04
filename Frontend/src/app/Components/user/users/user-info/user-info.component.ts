import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StorageKeyNames } from 'src/app/Constants/StorageKeyNames';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SuccessModel } from 'src/app/Models/SuccessModel';
import { UserModel } from 'src/app/Models/UserModel';
import { UserRequest } from 'src/app/Requests/UserRequest';
import { AccountStorageService } from 'src/app/Services/AccountStorageService';
import { UserService } from 'src/app/Services/UserService';

@Component({
    selector: 'users-user-info',
    templateUrl: './user-info.component.html',
    providers: [
        UserService,
        AccountStorageService
    ]
})
export class UserInfoComponent {

    constructor(
        private service : UserService,
        private accountStorageService : AccountStorageService,
        private router : Router
    ) { }

    model = new UserModel();
    error = new ErrorModel();
    success = new SuccessModel();
    newPassword : string = '';

    ngOnInit() {
        this.service.getCurrentUser()
            .subscribe(
                (data : UserModel) => {
                    this.model.email = data.email;
                    this.model.id = data.id;
                    this.model.role = data.role;
                },
                error => {
                    this.router.navigate(['/account/signin']);
                }
            );
    }

    editUser() {
        let request = new UserRequest(this.newPassword);
        this.service
            .editUser(this.model.id, request)
            .subscribe(
                () => {
                    this.success.flag = true;
                },
                (error  : string) => {
                    this.error.exists = true;
                    this.error.text = error;
                }
            )
    }

    async deleteUser() {
        await this.service
            .deleteUser(this.model.id)
            .toPromise();
        this.success.flag = true;
        this.signOut();
    }

    signOut() {
        this.accountStorageService.deleteToken();
        this.router.navigate(['/']);
    }

    clearForm() {
        this.success.flag = false;
        this.error.exists = false;
    }
}