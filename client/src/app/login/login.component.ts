import { FormsModule } from '@angular/forms';
import {AccountService} from '../services/account.service'
import {SharedServicesService} from '../services/shared-services.service'
import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  model:any = {};
  loggedIn!:boolean;
  

  constructor(private accountService:AccountService, private sharedServices: SharedServicesService)
  {

  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: response =>
      {
        console.log(response);
        this.loggedIn = true;
        this.sharedServices.updateLoginStatus(true);
        
      },
      error: error =>
      {
        console.log(error);
        this.loggedIn = false;
        this.sharedServices.updateLoginStatus(false);

      }
    });
  }

  logout()
  {
    
  }
}
