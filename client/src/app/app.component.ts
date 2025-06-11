import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NavComponent } from "./nav/nav.component";
import { User } from './models/user';
import { AccountService } from './services/account.service';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule, NavComponent],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';
  users:any;
  
  constructor(private accountService:AccountService) {
    // Constructor logic can go here if needed
  } 


  ngOnInit():void {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    const user:User =  userString? JSON.parse(userString):null;
    this.accountService.setCurrentUser(user);
  }


}
