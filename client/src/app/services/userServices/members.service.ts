import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Member } from '../../models/member';
import { map, of } from 'rxjs';
import { PaginatedResult } from '../../models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  //and array to hold the http request responses to limit number of http requests
  members: Member[] = [];
  paginatedResult:PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  
  constructor(private http:HttpClient) { }

  getMembers(page?:number, itemsPerPage?:number)
  {
    // //checking the array to see if there is data to query from to limit http requests
    // //if(this.members.length > 0) return of(this.members);


    // return this.http.get<Member[]>(this.baseUrl + 'user').pipe(
    //   map(members=> {
    //     this.members= members;
    //     return members;
    //   })
    // )
    let params = new HttpParams();
    if(page != null && itemsPerPage != null)
    {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    //observe make this return the whole response rather than the respose body,
    //so we need to grab the body
    return this.http.get<Member[]>(this.baseUrl + 'user', {observe: 'response', params}).pipe(
      map(response =>{
        this.paginatedResult.result = response.body ?? [];
        if(response.headers.get('Pagination') != null)
        {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') ?? '');
        }
        return this.paginatedResult;
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

setMainPhoto(photoId:number)
{
  return this.http.put(this.baseUrl + 'user/set-main-photo/' + photoId, {});
}

deletePhoto(photoId:number)
{
  return this.http.delete(this.baseUrl + 'user/delete-photo/' + photoId);
}




}
