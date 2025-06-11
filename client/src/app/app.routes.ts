import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { authGuard } from './services/authGuard';

export const routes: Routes = [

    {
        path: '',
        component: LoginComponent,
    },
    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path:'register',
        component: RegisterComponent,
    },

    {
        path: 'home',
        component: HomeComponent,
        canActivate: [authGuard],
    }

];
