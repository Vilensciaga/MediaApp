import { Component, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../../models/member';
import { MembersService } from '../../../services/userServices/members.service';
import { AccountService } from '../../../services/userServices/account.service';
import {CommonModule} from '@angular/common';
import {FormsModule, NgForm} from '@angular/forms'
import { User } from '../../../models/user';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { HostListener } from '@angular/core';
import { PhotoEditorComponent } from "../photo-editor/photo-editor.component";

@Component({
  selector: 'app-member-edit',
  imports: [CommonModule, FormsModule, PhotoEditorComponent],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit{
  @ViewChild('editForm') editForm!: NgForm;
  member!:Member;
  user!:User;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any)
  {
    if(this.editForm.dirty)
    {
      $event.returnValue = true;
    }
  }

  constructor(private memberService:MembersService, private accountService:AccountService, private toastr:ToastrService)
  {
   this.accountService.currentUser$.pipe(take(1)).subscribe({
    next:user=>
    {
      if(user)
      {
        this.user = user;
      }
      
    }
   })
  }
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.memberService.getMember(this.user.userName).subscribe({
      next:member=>{
        this.member = member;
      }
    })
  }

  updateMember()
  {
    this.memberService.editMember(this.member).subscribe({
      next:response=>{
        this.toastr.success('Profile Updated Successfully');
        this.editForm.reset(this.member);
      },
      error:error=>{
        this.toastr.error(error, 'Error while processing request');
      }
    })

  }

}


