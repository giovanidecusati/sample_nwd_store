import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthGuardService } from '../../../services/auth-guard.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
})
export class TopbarComponent implements OnInit {

  constructor(
    private _router: Router,
    public authService: AuthGuardService
  ) {}

  ngOnInit() {}

  login(): void {
    this._router.navigate(['login']);
  }

  logout(): void {    
    this.authService.logout();
  }
}
