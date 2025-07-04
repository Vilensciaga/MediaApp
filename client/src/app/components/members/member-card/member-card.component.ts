import { Component, Input } from '@angular/core';
import { Member } from '../../../models/member';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-member-card',
  imports: [RouterModule],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
  @Input() member!:Member;
  
}
