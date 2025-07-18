import { Component, Input, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common';
import {FileUploadModule} from 'ng2-file-upload';
import {FileUploader} from 'ng2-file-upload';
import { Member } from '../../../models/member';
import { environment } from '../../../../environments/environment';
import { AccountService } from '../../../services/userServices/account.service';
import { MyUploader } from '../../../services/helpers/myUploader';
import { MembersService } from '../../../services/userServices/members.service';
import { Photo } from '../../../models/photo';
import { User } from '../../../models/user';
import { take } from 'rxjs';

@Component({
  selector: 'app-photo-editor',
  imports: [CommonModule, FileUploadModule],
  templateUrl: './photo-editor.component.html',
  styleUrl: './photo-editor.component.css'
})
export class PhotoEditorComponent implements OnInit {
@Input() member!:Member;
uploader!: FileUploader;
hasBaseDropzoneOver = false;
baseUrl = environment.apiUrl;
user!:User | null;

 constructor(private accountService:AccountService, private memberService:MembersService) {
  this.accountService.currentUser$.pipe(take(1)).subscribe({next:user=>{ this.user = user}})
  }

  ngOnInit(): void {
    this.initializeUploader();
  }



setMainPhoto(photo:Photo)
{
  this.memberService.setMainPhoto(photo.id).subscribe(
    {
    next: response=>{
      if(this.user){
        this.user.photoUrl = photo.url;
        this.accountService.setCurrentUser(this.user);
        this.member.photoUrl = photo.url;
        this.member.photos.forEach(p => {
          if(p.isMain) p.isMain = false;
          if(p.id === photo.id) p.isMain = true;
          
        });
      }
      
    }
  })
}

deletePhoto(photoId:number)
{
  this.memberService.deletePhoto(photoId).subscribe({
    next:response=>
    {
      //returns an array of all the photos except the one with that id
      this.member.photos = this.member.photos.filter(x=> x.id !== photoId);   
    }
  })
}



  initializeUploader()
  {
    var token = this.accountService.getToken();
    
    this.uploader = new MyUploader(
      {
        url: this.baseUrl + 'user/add-photo',
        authToken: 'Bearer ' + token,
        isHTML5: true,
        allowedFileType: ['image'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024 * 1024
      }
    );

    this.uploader.onAfterAddingFile =  (file) => {
      file.withCredentials = false;
    }

    this.uploader.onSuccessItem = (item, response, status, headers)=>{
      if(response)
      {
        const photo = JSON.parse(response);
        this.member.photos.push(photo);
      }
    }
  }

  fileOverBase(e:any){
    this.hasBaseDropzoneOver = e;
  }

get isUploaderHTML5(): boolean {
  return (this.uploader as any)?.isHTML5 ?? false;
}


}
