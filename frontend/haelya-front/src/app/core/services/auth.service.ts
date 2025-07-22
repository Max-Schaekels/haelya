import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { Login } from '../models/login';
import { Register } from '../models/register';
import { UserRole } from '../models/user-role';
import {jwtDecode} from 'jwt-decode';

interface TokenPayload{
  role : UserRole
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

  login(login : Login){
    return this._http.post<{ token: string}>(`${this._apiUrl}/Auth/Login`, login)
  }

  register( register : Register){
    return this._http.post<void>(`${this._apiUrl}/Auth/Register`, register)
  }

    saveAuth(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
    this.isConnectedSignal.set(true);
    this.isAdminSignal.set(this.decodeToken()?.role=== UserRole.Admin);
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

  decodeToken() : TokenPayload | null{
    const token = this.getToken();
    if (!token){
      return null;
    }
    try {
      return jwtDecode<TokenPayload>(token);
    } catch {
      return null;
    }
  }


  getRole(): UserRole | null {
    return this.decodeToken()?.role ?? null;
  }


  private hasToken(): boolean {
    return typeof window !== 'undefined' && !!localStorage.getItem(this.TOKEN_KEY);
  }

  private hasAdminRole(): boolean {
    return this.getRole() === UserRole.Admin;
  }


}

