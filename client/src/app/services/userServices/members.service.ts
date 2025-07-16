import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Member } from '../../models/member';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  //and array to hold the http request responses to limit number of http requests
  members: Member[] = [];

  
  constructor(private http:HttpClient) { }

  getMembers()
  {
    //checking the array to see if there is data to query from to limit http requests
    if(this.members.length > 0) return of(this.members);


    return this.http.get<Member[]>(this.baseUrl + 'user').pipe(
      map(members=> {
        this.members= members;
        return members;
      })
    )
  }

  getMember(username:string)
  {
    //querying array
    const member = this.members.find(x=> x.username === username);


    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'user/' + username)
  }

  editMember(member:Member)
  {
    return this.http.put<Member>(this.baseUrl + 'user/', member).pipe(
      map(()=>{
        //updating the array
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }
}
