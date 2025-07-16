import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/userServices/user.service';
import {CommonModule} from '@angular/common'
import { HttpHeaders } from '@angular/common/http';
import { Member } from '../../models/member';
import { AccountService } from '../../services/userServices/account.service';
import { MembersService } from '../../services/userServices/members.service';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  users:any;
  

  constructor(private userService:UserService,
              private accountService:AccountService,
              private memberService:MembersService
  )
  {

  }

  ngOnInit()
  {
    this.getUsers();
  }

  getUsers()
  {
    this.memberService.getMembers().subscribe({
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
