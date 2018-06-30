import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate {
  constructor(private _router: Router) {}

  canActivate(): boolean {
    if (!localStorage.getItem('northwind.token')) {
      this._router.navigate(['/']);
      return false;
    }

    return true;
  }
}
