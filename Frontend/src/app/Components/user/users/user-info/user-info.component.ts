import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageKeyNames } from 'src/app/Constants/LocalStorageKeyNames';
import { UserModel } from 'src/app/Models/UserModel';
import { UserRequest } from 'src/app/Requests/UserRequest';
import { UserService } from 'src/app/Services/UserService';

@Component({
    selector: 'users-user-info',
    templateUrl: './user-info.component.html',
    styleUrls: ['./user-info.component.css'],
    providers: [UserService]
})
export class UserInfoComponent {

    constructor(
        private service : UserService,
        private router : Router) { }

    model : UserModel = new UserModel();
    newPassword : string = "";

    ngOnInit() {
        let id = parseInt(window.location.pathname.split('/').pop()!);
        this.service.getCurrentUser().subscribe(
            (data : any) => {
                this.model.email = data.email;
                this.model.id = data.id;
            },
            error => {
                this.router.navigate(["/account/signin"]);
            }
        )
    }

    editUser() {
        let request = new UserRequest(this.newPassword);
        this.service.editUser(this.model.id!, request);
    }

    deleteUser() {
        this.service.deleteUser(this.model.id!);        
    }

    signOut() {
        localStorage.removeItem(LocalStorageKeyNames.TOKEN);
        this.router.navigate(["/"]);
    }
}