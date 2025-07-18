import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { authGuard } from './services/guards/auth.guard';
import { MemberListComponent } from './components/members/member-list/member-list.component';
import { MemberDetailComponent } from './components/members/member-detail/member-detail.component';
import { ListsComponent } from './components/lists/lists.component';
import { MessagesComponent } from './components/messages/messages.component';
import { TestErrorsComponent } from './components/errors/test-errors/test-errors.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { MemberEditComponent } from './components/members/member-edit/member-edit.component';
import { preventUnsavedChangesGuard } from './services/guards/prevent-unsaved-changes.guard';

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
        //all these paths are protected by the authGuard we created
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
                path: 'members/:username',
                component: MemberDetailComponent
            },
            {
                path: 'member/edit',
                component: MemberEditComponent,
                canDeactivate: [preventUnsavedChangesGuard]
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
        path: 'error',
        component: TestErrorsComponent,
    
    },
    {
        path: 'not-found',
        component: NotFoundComponent,
    
    },
    {
        path: 'server-error',
        component: ServerErrorComponent,
    
    },
    {
        path:'**',
        component: NotFoundComponent,
        pathMatch: 'full'
    },

];
