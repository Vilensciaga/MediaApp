import { FormsModule } from '@angular/forms';
import {CommonModule} from '@angular/common'
import {AccountService} from '../../services/userServices/account.service'
import {SharedServicesService} from '../../services/sharedServices/shared-services.service'
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  model:any = {};
  loggedIn:boolean = false;

  registerMode:boolean = false;
  
  

  constructor(private accountService:AccountService, 
    private sharedServices: SharedServicesService, 
    private router:Router,
    private toastr:ToastrService)
  {

  }
  ngOnInit(): void {
    this.navigateHome();
  }

  navigateHome()
  {
    this.accountService.isUserLoggedIn().subscribe({
      next: response=>
      {
        this.loggedIn = response;
      }
    });

    if(this.loggedIn)
    {
      this.router.navigate(['/home']);
    }
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: response =>
      {
        //setTimeout(()=>{
          this.router.navigate(["/home"]);
        //}, 500)
        //this.loggedIn = true;
        //this.sharedServices.updateLoginStatus(true);
        
      },
      error: error =>
      {
        console.log(error);
        this.toastr.error(error.error);
        //this.loggedIn = false;
        //this.sharedServices.updateLoginStatus(false);

      }
    });
  }

}
