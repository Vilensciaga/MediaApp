import { Component, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common';
import { Member } from '../../../models/member';
import { MembersService } from '../../../services/userServices/members.service';
import { ToastrService } from 'ngx-toastr';
import { MemberCardComponent } from '../member-card/member-card.component';
import { Observable, take } from 'rxjs';
import { Pagination } from '../../../models/pagination';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { UserParams } from '../../../models/userParams';
import { AccountService } from '../../../services/userServices/account.service';
import { User } from '../../../models/user';


@Component({
  selector: 'app-member-list',
  imports: [CommonModule, MemberCardComponent, NgbPaginationModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{

  //members$!: Observable<Member[]>;
   members!: Member[];
   pagination!:Pagination;
   userParams!:UserParams;
   user!:User;

  constructor(private memberService:MembersService,
              private toastr:ToastrService,
              private accountService:AccountService
  )
  {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user =>{
        if(user)
        {
          this.user = user;
          this.userParams = new UserParams(user);
        }
      }
    })
  }
  ngOnInit() {
    //this.members$ = this.memberService.getMembers();
    this.loadMembers();
  }

  loadMembers(){
    this.memberService.getMembers(this.userParams).subscribe({
      next: response =>
      {
        this.members = response.result;
        this.pagination = response.pagination; 
      }
    })
  }

  pageChanged(page:number)
  {
    this.userParams.pageNumber = page;
    this.loadMembers();
  }

}
