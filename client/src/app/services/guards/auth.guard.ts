import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../account.service';
import {ToastrService} from 'ngx-toastr';
import { map, take } from 'rxjs/operators';

export const authGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const accountService = inject(AccountService);
  const toastrService = inject(ToastrService);

  return accountService.isUserLoggedIn().pipe(
    //take(1),
    map(isLoggedIn=>{
      if(isLoggedIn)
      {
        return true;
      }
      else
      {
        toastrService.error("Please login or register.");
        return false;
      }
    })
  )

  // return accountService.currentUser$.pipe(
  //   map(isLoggedIn=>{
  //     if(isLoggedIn)
  //     {
  //       return true;
  //     }
  //     else{
  //       return false;
  //     }
  //   })
  // )

};
