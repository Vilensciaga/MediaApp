import { HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { map } from 'rxjs';

export const dynamicContentUnwrapInterceptor: HttpInterceptorFn = (req, next) => {
  
  return next(req).pipe(
    map(response => {
      if(response instanceof HttpResponse)
      {
        const unwrapped = unwrap(response.body)
        return response.clone({body: unwrapped});
      }
      return response;
    })
  )
  
};


function unwrap(body: any): any {
    if (!body || typeof body !== 'object') return body;

    // 1) Unwrap "content" if present, ex { "content": { "profile": { "username": "bob" } } } 
    let payload = ('content' in body) ? body.content : body;

    if (Array.isArray(payload)) return payload;

    // unwrapping furter passed content and just returning the inner object if there is 1 key inside the object
    if (payload && typeof payload === 'object') {
      const keys = Object.keys(payload);
      if (keys.length === 1) {
        return payload[keys[0]];
      }
    }

    // Otherwise, return whatever we have after the first unwrap
    return payload;
  }
