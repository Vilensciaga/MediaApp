import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {CommonModule} from '@angular/common'
import {SharedServicesService} from '../../services/sharedServices/shared-services.service'
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountService } from '../../services/userServices/account.service';
import { Observable } from 'rxjs/internal/Observable';
import { User } from '../../models/user';
import { Router, RouterModule } from '@angular/router';


@Component({
  selector: 'app-nav',
  imports: [FormsModule, CommonModule, NgbDropdownModule, RouterModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit{

currentUser$!: Observable<User|null>;

constructor(private sharedServices:SharedServicesService,
  private accountService:AccountService,
  private router:Router,)
{
  
}  

ngOnInit()
{
  this.currentUser$ = this.accountService.currentUser$;
  this.currentUser$.subscribe({
    next:user=>
    {
      //accessing the value of the observable
      //console.log('current User:',user);
    }
  })
  //this.isUserLoggedIn();
}



logout()
{
  this.accountService.logout();
  this.router.navigate(['/']);
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
