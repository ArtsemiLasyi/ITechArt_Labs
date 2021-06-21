import { Component } from '@angular/core';
import { UserModel } from 'src/app/Models/UserModel';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  user : UserModel = new UserModel();

  
}
