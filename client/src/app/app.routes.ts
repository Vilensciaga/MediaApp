import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { authGuard } from './services/guards/auth.guard';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MemberDetailComponent } from './components/members/member-detail/member-detail.component';
import { ListsComponent } from './components/lists/lists.component';
import { MessagesComponent } from './components/messages/messages.component';

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
        path:'',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            
            {
                path: 'home',
                component: HomeComponent,
                canActivate: [authGuard],
            },

            {
                path:'members',
                component: MemberListComponent,
            },
            {
                path: 'member/:id',
                component: MemberDetailComponent
            },
            {
                path:'lists',
                component: ListsComponent
            },
            {
                path: 'messages',
                component: MessagesComponent
            },
        ]
    },

    {
        path:'**',
        component: HomeComponent,
        pathMatch: 'full'
    }

];
