import { HttpInterceptorFn } from '@angular/common/http';
import { delay, finalize, tap } from 'rxjs';
import { SpinnerService } from '../services/spinnerService/spinner.service';
import { inject } from '@angular/core';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const spinnerService = inject(SpinnerService);
  //go register in app.config

spinnerService.spinnerCounter();
  return next(req).pipe(

    delay(1000),
    finalize(()=>{
      spinnerService.idle();
    })
  );
};
