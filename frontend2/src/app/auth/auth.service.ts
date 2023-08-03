import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Token } from '../models/token';

@Injectable()
export class AuthService {
  constructor(private jwtHelper: JwtHelperService, private router: Router) {}

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  saveToken(tokenObj: Token) {
    localStorage.setItem('token', tokenObj.token);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['login']);
  }
}
