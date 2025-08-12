import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Member } from '../../models/member';
import { map, of } from 'rxjs';
import { PaginatedResult } from '../../models/pagination';
import { UserParams } from '../../models/userParams';
import { Result } from '../../models/Result';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  //and array to hold the http request responses to limit number of http requests
  members: Member[] = [];

  
  constructor(private http:HttpClient) { }


  getMember(username:string)
  {
    //querying array
    const member = this.members.find(x=> x.username === username);


    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'getMember/' + username)
  }

  editMember(member:Member)
  {
    return this.http.put<Member>(this.baseUrl + 'updateMember/', member).pipe(
      map(()=>{
        //updating the array
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }

setMainPhoto(photoId:number)
{
  return this.http.put(this.baseUrl + 'setmainphoto/' + photoId, {});
}

deletePhoto(photoId:number)
{
  return this.http.delete(this.baseUrl + 'deletephoto/' + photoId);
}


getMembers(userParams:UserParams)
  {
    let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('minAge', userParams.minAge.toString());
    params = params.append('maxAge', userParams.maxAge.toString());
    params = params.append('gender', userParams.gender);

    //observe make this return the whole response rather than the respose body,
    //so we need to grab the body
    return this.getPaginatedResult<Member[]>(this.baseUrl + 'user', params)
  }

  private getPaginationHeaders(pageNumber: number, pageSize:number)
  {
      let params = new HttpParams();

        params = params.append('pageNumber', pageNumber.toString());
        params = params.append('pageSize', pageSize.toString());

        return params;
  }



  private getPaginatedResult<T>(url:string, params:HttpParams) 
  {
    const paginatedResult:PaginatedResult<T> = new PaginatedResult<T>();

    return this.http.get<T>(url, { observe: 'response', params}).pipe(
      map(response => {
        paginatedResult.result = response.body ?? [] as T;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') ?? '');
          console.log('here', paginatedResult)
        }
        return paginatedResult;
      })

    );
  }

}
