import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';

@Injectable({
  providedIn: 'root'
})
export class SharedServicesService {

  constructor() { }
  
  private isLoggedInSource = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSource.asObservable();

  updateLoginStatus(newValue: boolean) {
    this.isLoggedInSource.next(newValue);
  }
}
