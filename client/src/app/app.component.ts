import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'client';
  users:any;
  
  constructor(private http: HttpClient) {
    // Constructor logic can go here if needed
  } 


  ngOnInit():void {
    this.getUsers()
  }

  getUsers()
  {
    this.http.get('https://localhost:5001/api/user').subscribe({
      next: response =>
      {
        this.users = response;
      },
      error: error =>
      {
        console.error('Error fetching users:', error);
      }
    }
     
    )
  }


}
