import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate {
  constructor(private _router: Router) {}

  canActivate(): boolean {
    if (!localStorage.getItem('user')) {
      this._router.navigate(['/']);
      return false;
    }

    return true;
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }

  getUserName(): string {
    return localStorage.getItem('userName');
  }

  getToken(): string {
    return localStorage.getItem('token');
  }

  getUserEmail(): string {
    return localStorage.getItem('userEmail');
  }

  logout(): void {
    localStorage.clear();
    this._router.navigate(['/']);
  }

  login(login: string, password: string): void {
    localStorage.setItem('token', '7d34fa7e-c6a9-406a-9313-800b952f1b56');
    localStorage.setItem('userName', 'Giovani Decusati');
    localStorage.setItem('userEmail', 'usuario@email.com');
    this._router.navigate(['/']);
  }
}
