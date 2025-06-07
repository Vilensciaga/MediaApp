import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {CommonModule} from '@angular/common'
import {SharedServicesService} from '../services/shared-services.service'
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountService } from '../services/account.service';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../models/user';


@Component({
  selector: 'app-nav',
  imports: [FormsModule, CommonModule, NgbDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit{

currentUser$!: Observable<User|null>;

constructor(private sharedServices:SharedServicesService, private accountService:AccountService)
{
  
}  

ngOnInit()
{
  this.currentUser$ = this.accountService.currentUser$;
  this.isUserLoggedIn();
}



logout()
{
  this.accountService.logout();
  //this.sharedServices.updateLoginStatus(false);
}


isUserLoggedIn()
{
  this.sharedServices.isLoggedIn$.subscribe(
    {
      next:value=>
      {
        //this.isLoggedIn = value;
        console.log("login Status", value);
      }
    }
  )
}

}
