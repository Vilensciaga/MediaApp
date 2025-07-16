import { Component, OnInit } from '@angular/core';
import { Member } from '../../../models/member';
import { MembersService } from '../../../services/userServices/members.service';
import { ActivatedRoute } from '@angular/router';
import {CommonModule} from '@angular/common';
import { GalleryModule, GalleryItem, ImageItem, GalleryConfig, ThumbnailsPosition, VideoItem, YoutubeItem, IframeItem, } from 'ng-gallery';
import { map, Observable } from 'rxjs';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
@Component({
  selector: 'app-member-detail',
  imports: [CommonModule, GalleryModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent implements OnInit{
  member!:Member;
  images: GalleryItem[] = [];
   galleryConfig$!: Observable<GalleryConfig>;



  constructor(private memberService:MembersService, private route:ActivatedRoute, private breakpointObserver: BreakpointObserver)
  {
    this.galleryConfig$ = breakpointObserver.observe([
      Breakpoints.HandsetPortrait
    ]).pipe(
      map(res => {
        if (res.matches) {
          return {
            thumbPosition: ThumbnailsPosition.Top,
            thumbWidth: 80,
            thumbHeight: 80
          };
        }
        return {
          thumbPosition: ThumbnailsPosition.Left,
          thumbWidth: 120,
          thumbHeight: 90
        };
      })
    );

  }
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
                this.loadImages();

              }
            }
          );
    }
  }

  loadImages()
  {
    for(const photo of this.member.photos)
    {
      console.log(this.member.photos)
      this.images.push(
        new ImageItem({
          src: photo.url,
          thumb: photo.url
        })
      );
    }
  }

}
