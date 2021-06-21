import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ErrorModel } from 'src/app/Models/ErrorModel';
import { SignUpModel } from 'src/app/Models/SignUpModel';
import { SignUpRequest } from 'src/app/Requests/SignUpRequest';
import { AuthentificationService } from 'src/app/Services/authentificationservice';

@Component({
  selector: 'account-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  providers: [ AuthentificationService]
})
export class SignUpComponent {

  constructor(
    private service : AuthentificationService,
    private router : Router) { }

  model : SignUpModel = new SignUpModel();
  error : ErrorModel = new ErrorModel();

  signUp() {
    if (this.error.exists) {
        return;
    }
    let request = new SignUpRequest(this.model.email!, this.model.password!);
    this.service.signUp(request).subscribe(
      () => {
        this.router.navigate(['']);
      },
      error => {
        this.error.exists = true;
        this.error.text = this.getError(error);
      }
    );
  }

  getError(error : any) : string {
    return error.error.errorText || 
      error.error.errors.Email || 
      error.error.errors.Password || 
      error.error.title;     
  }
}