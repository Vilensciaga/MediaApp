import { Component, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common'
import {FormsModule} from '@angular/forms'
import { Router, RouterModule } from '@angular/router';
import { AccountService } from '../services/account.service';
@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  model:any = {};

  constructor(private accountService:AccountService, private router:Router)
  {

  }

  ngOnInit()
  {

  }

  register()
  {
    this.accountService.register(this.model).subscribe({
      next: response =>{
        //console.log(response);
        setTimeout(()=>{
          this.router.navigate(["/home"]);
        }, 500)
      },
      error: error =>
      {
        console.log(error);
      }
    });
  }



}
