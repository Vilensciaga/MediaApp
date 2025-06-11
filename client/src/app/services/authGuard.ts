import { Injectable } from '@angular/core';
import { inject } from '@angular/core';
import { AccountService } from './account.service';
import { Router, CanActivateFn } from '@angular/router';
import { map, take } from 'rxjs/operators';

export const authGuard: CanActivateFn = (route, state) => {
    const router = inject(Router);
    const accountService = inject(AccountService);
    let isLoggedIn:boolean = false;

    //isUserLoggedIn is returning tru or false, but because im using pipe and map
    //to access the value and perform an action i have to add return true or false because authguard is expecting a boolean 
    return accountService.isUserLoggedIn().pipe(
    take(1),
    map(isLoggedIn => {
        if(isLoggedIn)
        {
            return true;
        }
        else{
            return false;
        }
    })
    );


    // return accountService.currentUser$.pipe(
    //     take(1),
    //     map(user => {
    //         if(user)
    //         {
    //             return true
    //         }
    //         else
    //         {
    //             return false
    //         }
    //     })
    // )


}