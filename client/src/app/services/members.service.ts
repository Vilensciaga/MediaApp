import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  
  constructor(private http:HttpClient) { }

  getMembers()
  {
    return this.http.get<Member[]>(this.baseUrl + 'user')
  }

  getMember(username:string)
  {
    return this.http.get<Member>(this.baseUrl + 'user/' + username)
  }
}
