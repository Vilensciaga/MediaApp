import { HttpInterceptorFn } from '@angular/common/http';
import { AccountService } from '../services/account.service';
import { inject } from '@angular/core';
import { User } from '../models/user';
import { take } from 'rxjs';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);
  let currentUser!:User | null;

  /*
  Intercepter to add authorization to every required request sent to the server when a use is logged in
  we will register in it app.config.ts
  */

  
  //take(1) ensures we unsubscribe from something ny just taking one in case we do need to unsubscribe
  accountService.currentUser$.pipe(
    take(1)).subscribe({
      next:user=>{
        currentUser = user;
      }
    });

    if(currentUser){
      req = req.clone({
        setHeaders:{
          Authorization: `Bearer ${currentUser.token}`
        }
      })
    }


  
  return next(req);

};
