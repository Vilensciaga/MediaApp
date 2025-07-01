import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { User } from '../../../models/user';
import {CommonModule} from '@angular/common'



@Component({
  selector: 'app-test-errors',
  imports: [CommonModule,],
  templateUrl: './test-errors.component.html',
  styleUrl: './test-errors.component.css'
})
export class TestErrorsComponent {

baseUrl = 'https://localhost:5001/api/';
validationErrors: string[] = [];

constructor(private http: HttpClient)
{

}

get404Error()
{
  this.http.get<any>(this.baseUrl + 'buggy/not-found').subscribe({
    next:response =>
    {
      console.log(response);
    },
    error:error =>
    {
      console.log(error);
    }
  })
}

get400Error()
{
  this.http.get<any>(this.baseUrl + 'buggy/bad-request').subscribe({
    next:response =>
    {
      console.log(response);
    },
    error:error =>
    {
      console.log(error);
    }
  })
}

get500Error()
{
  this.http.get<any>(this.baseUrl + 'buggy/server-error').subscribe({
    next:response =>
    {
      console.log(response);
    },
    error:error =>
    {
      console.log(error);
    }
  })
}

get401Error()
{
  this.http.get<any>(this.baseUrl + 'buggy/auth').subscribe({
    next:response =>
    {
      console.log(response);
    },
    error:error =>
    {
      console.log(error);
    }
  })
}

get400ValidationError()
{
  this.http.post<User>(this.baseUrl + 'account/register', {}).subscribe({
    next:response =>
    {
      console.log(response);
    },
    error:error =>
    {
      //console.log(error);
      console.log(error.error)
      this.validationErrors = error;
    }
  })
}

}
