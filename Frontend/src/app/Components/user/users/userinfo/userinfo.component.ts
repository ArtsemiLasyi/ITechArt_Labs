import { Component } from '@angular/core';
import { UserModel } from 'src/app/Models/UserModel';
import { UserRequest } from 'src/app/Requests/UserRequest';
import { UserService } from 'src/app/Services/userservice';

@Component({
  selector: 'users-userinfo',
  templateUrl: './userinfo.component.html',
  styleUrls: ['./userinfo.component.css'],
  providers: [UserService]
})
export class UserInfoComponent {

  constructor(private service : UserService) { }

  model : UserModel = new UserModel();
  newPassword : string = "";

  ngOnInit() {
    let id = parseInt(window.location.pathname.split('/').pop()!);
    this.service.getUser(id).subscribe(
      (data : any) => {
        this.model.email = data.email;
        this.model.id = data.id;
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
}