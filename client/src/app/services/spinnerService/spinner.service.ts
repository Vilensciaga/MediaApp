import { Injectable } from '@angular/core';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {
  spinnerRequestCount = 0;
  constructor(private spinnerService:NgxSpinnerService) { }

  spinnerCounter()
  {
    this.spinnerRequestCount++;
    this.spinnerService.show(undefined, {
      type:'pacman',
      bdColor:'rgba(255, 255, 255, 0)',
      color:'#333333'

    });
  }

  idle()
  {
    this.spinnerRequestCount--;
    if(this.spinnerRequestCount <= 0)
    {
      this.spinnerRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
