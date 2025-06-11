import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:5001/api/';
  constructor(private http:HttpClient) { }

  getUsers()
  {
    return this.http.get<User>(this.baseUrl + 'user');
  }
}
