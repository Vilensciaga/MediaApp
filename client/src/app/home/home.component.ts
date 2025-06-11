import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import {CommonModule} from '@angular/common'

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  users:any;

  constructor(private userService:UserService)
  {

  }

  ngOnInit()
  {
    this.getUsers();
  }

  getUsers()
  {
    this.userService.getUsers().subscribe({
      next:response=>
      {
        this.users = response;
      },
      error:error=>
      {
        console.log(error);
      }
    });
  }
  

  

}
