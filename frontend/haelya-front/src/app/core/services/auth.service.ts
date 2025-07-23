import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { Login } from '../models/login';
import { Register } from '../models/register';
import { UserRole } from '../models/user-role';
import { jwtDecode } from 'jwt-decode';

interface TokenPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role': string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress': string;
  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname': string;
  [key: string]: unknown;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly _apiUrl = environment.apiUrl;
  private readonly TOKEN_KEY = 'jwt_token';

  private _http: HttpClient = inject(HttpClient);
  private _router: Router = inject(Router);

  //Signals : 
  private isConnectedSignal = signal<boolean>(this.hasToken());
  public readonly isConnected = this.isConnectedSignal;

  private isAdminSignal = signal<boolean>(this.hasAdminRole());
  public readonly isAdmin = this.isAdminSignal;

  constructor() { }

  login(login: Login) {
    return this._http.post<{ token: string; }>(`${this._apiUrl}/Auth/Login`, login);
  }

  register(register: Register) {
    return this._http.post<void>(`${this._apiUrl}/Auth/Register`, register);
  }

  saveAuth(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);

    const decoded = this.decodeToken();
    const role = this.getRole();

    this.isConnectedSignal.set(true);
    this.isAdminSignal.set(role === UserRole.Admin);
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
    this.isConnectedSignal.set(false);
    this.isAdminSignal.set(false);
    this._router.navigate(['login']);
  }

  getToken(): string | null {
    return typeof window !== 'undefined' ? localStorage.getItem(this.TOKEN_KEY) : null;
  }

  decodeToken(): TokenPayload | null {
    const token = this.getToken();
    if (!token) {
      return null;
    }
    try {
      return jwtDecode<TokenPayload>(token);
    } catch {
      return null;
    }
  }


  getRole(): UserRole | null {
    const decoded = this.decodeToken();
    if (!decoded) return null;

    const rawRole = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]?.toLowerCase();
    if (!rawRole) return null;

    switch (rawRole) {
      case 'admin':
        return UserRole.Admin;
      case 'customer':
        return UserRole.Customer;
      default:
        return null;
    }
  }

  getFirstName(): string | null {
  const token = this.decodeToken();
  return token?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'] ?? null;
}

getEmail(): string | null {
  const token = this.decodeToken();
  return token?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] ?? null;
}


  private hasToken(): boolean {
    return typeof window !== 'undefined' && !!localStorage.getItem(this.TOKEN_KEY);
  }

  private hasAdminRole(): boolean {
    return this.getRole() === UserRole.Admin;
  }


}

