import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators'
import { User } from '../models/user';
import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
import { Observable } from 'rxjs/internal/Observable';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

baseUrl = 'https://localhost:5001/api/';
private currentUserSource = new ReplaySubject<User | null>(1);
currentUser$ = this.currentUserSource.asObservable();

constructor(private http:HttpClient) 
{

}

login(model:any)
{
  return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
    map((response:User) =>{
      const user = response;
      if(user){
        this.setCurrentUser(user);
      }
    })
  );
}

register(model: any)
{
return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
  map((user:User )=> {
    if(user)
    {
      this.setCurrentUser(user);
    }

  })
)
}

setCurrentUser(user:User)
{
  localStorage.setItem('user', JSON.stringify(user));
  this.currentUserSource.next(user);
}


//checks if current user is logged in and returns true or false
isUserLoggedIn():Observable<boolean>
{
  return this.currentUser$.pipe(
    map(user => !!user)
  );
}

logout()
{
  localStorage.removeItem('user');
  this.currentUserSource.next(null);
}


}
