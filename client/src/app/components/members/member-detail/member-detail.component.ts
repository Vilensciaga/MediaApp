import { Component, OnInit } from '@angular/core';
import { Member } from '../../../models/member';
import { MembersService } from '../../../services/members.service';
import { ActivatedRoute } from '@angular/router';
import {CommonModule} from '@angular/common'

@Component({
  selector: 'app-member-detail',
  imports: [CommonModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  member!:Member;

  constructor(private memberService:MembersService, private route:ActivatedRoute){}
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember()
  {
    let username = this.route.snapshot.paramMap.get('username');
    if(username)
    {
      this.memberService.getMember(username).subscribe(
            {
              next:member=>{
                this.member = member;
              }
            }
          );
    }
    
  }

}
