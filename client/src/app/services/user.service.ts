import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { Member } from '../models/member';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getUsers(headers:HttpHeaders)
  {
    return this.http.get<Member[]>(this.baseUrl + 'user', {headers});
  }
}
