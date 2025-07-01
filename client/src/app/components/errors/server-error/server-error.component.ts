import { Component } from '@angular/core';
import {Router, RouterModule} from '@angular/router'
import {CommonModule} from '@angular/common'

@Component({
  selector: 'app-server-error',
  imports: [RouterModule, CommonModule],
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {
  error: any;
  constructor(private router:Router)
  {
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.['error'];
  }
}
