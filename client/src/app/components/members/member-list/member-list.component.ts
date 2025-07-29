import { Component, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common';
import { Member } from '../../../models/member';
import { MembersService } from '../../../services/userServices/members.service';
import { ToastrService } from 'ngx-toastr';
import { MemberCardComponent } from '../member-card/member-card.component';
import { Observable } from 'rxjs';
import { Pagination } from '../../../models/pagination';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';


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
   pageNumber = 1;
   pageSize = 5;

  constructor(private memberService:MembersService,
              private toastr:ToastrService
  )
  {

  }
  ngOnInit() {
    //this.members$ = this.memberService.getMembers();
    this.loadMembers();
  }

  loadMembers(){
    this.memberService.getMembers(this.pageNumber, this.pageSize).subscribe({
      next: response =>
      {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    })
  }

  pageChanged(page:number)
  {
    this.pageNumber = page;
    this.loadMembers();
  }

}
