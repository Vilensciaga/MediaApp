import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';
  users: any;

  constructor(private http: HttpClient)
  {
  }

  ngOnInit():void
  {
    this.getUser();
  }

  getUser()
  {
    this.http.get('https://localhost:5001/api/user').subscribe({
      next: response=>
      {
        this.users = response;
        console.log(this.users);
      },
      error: error=>
      {
        console.error('Error fetching users:', error);
      }
    })
  }
}
