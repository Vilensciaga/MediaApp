import { ApplicationConfig, importProvidersFrom, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import {provideToastr} from 'ngx-toastr';
import { routes } from './app.routes';
import {errorInterceptor} from './interceptors/error.interceptor';
import { provideAnimations } from '@angular/platform-browser/animations';
import { jwtInterceptor } from './interceptors/jwt.interceptor';
//gallery
import { GalleryModule } from 'ng-gallery';
import {NgxSpinnerModule} from 'ngx-spinner';
import { loadingInterceptor } from './interceptors/loading.interceptor';


export const appConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes), 
    provideHttpClient(
      withInterceptors([errorInterceptor, jwtInterceptor, loadingInterceptor]),
    ), 
    provideToastr({positionClass: 'toast-bottom-right'}),
   provideAnimations(),
   importProvidersFrom(GalleryModule),
   importProvidersFrom(NgxSpinnerModule),
  ],
    
    
    
};
