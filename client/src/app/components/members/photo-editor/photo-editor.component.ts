import { Component, Input, OnInit } from '@angular/core';
import {CommonModule} from '@angular/common';
import {FileUploadModule} from 'ng2-file-upload';
import {FileUploader} from 'ng2-file-upload';
import { Member } from '../../../models/member';
import { environment } from '../../../../environments/environment';
import { AccountService } from '../../../services/userServices/account.service';
import { MyUploader } from '../../../services/helpers/myUploader';

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

 constructor(private accountService:AccountService) {

  }

  ngOnInit(): void {
    this.initializeUploader();
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
  console.log('Uploader instance:', this.uploader);
console.log('isHTML5?', (this.uploader as any).isHTML5);
  return (this.uploader as any)?.isHTML5 ?? false;
}


}
