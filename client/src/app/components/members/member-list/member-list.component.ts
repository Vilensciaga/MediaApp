import { Component, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common';
import { Member } from '../../../models/member';
import { MembersService } from '../../../services/userServices/members.service';
import { ToastrService } from 'ngx-toastr';
import { MemberCardComponent } from '../member-card/member-card.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-member-list',
  imports: [CommonModule, MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{

  members$!: Observable<Member[]>;

  constructor(private memberService:MembersService,
              private toastr:ToastrService
  )
  {

  }
  ngOnInit() {
    this.members$ = this.memberService.getMembers();
  }

}
