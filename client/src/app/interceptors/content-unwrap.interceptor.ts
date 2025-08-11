import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs';

export const contentUnwrapInterceptor: HttpInterceptorFn = (req, next) => {

  return next(req).pipe(
    map(response => {
      if(response instanceof HttpResponse)
      {
        const body = response.body;

        //unwarapping the content of the response
        if(body && typeof body === 'object' && 'content' in body)
        {
          return response.clone({body: body.content});
        }
      }
      return response;
    })
  )

};
