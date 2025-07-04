import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  /*
  Intecept http errors with a switch and perform actions accordingly
  will register it in app.config.ts
  */

  return next(req).pipe(
    catchError(error=>{
      if(error)
      {
        switch(error.status)
        {
          case 400:
            if(error.error.errors)
            {
              //flattening the errors recieved and putting them in an array to use toast
              const modalStateErrors: string[] = [];
              for(const key in error.error.errors)
              {
                if(error.error?.errors[key])
                {
                  modalStateErrors.push(...error.error.errors[key]);
                }
              }
              console.log(modalStateErrors.flat())
             
              //error.modalErrors = modalStateErrors.flat();
              throw modalStateErrors.flat();
            
              //return throwError(() =>  error);
            }
            else
            {
              toastr.error(error.statusText, error.status);
            }

        break;

        case 401:
          toastr.error(error.statusText, error.status);
          break;

        case 500:
          const navExtras: NavigationExtras = {state: {error: error.error}};
          router.navigate(['/server-error'], navExtras);
          break;

        case 404:
          router.navigate(['/not-found']);
          break;


        default:
          toastr.error('Something Unexpected went wrong');
          console.log(error);
          break;
        }
      }
      throw error;
      //return throwError(() => new Error(error));
    })
  );
};
