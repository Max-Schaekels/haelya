import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = environment.apiUrl;
  private readonly TOKEN_KEY = 'jwt_token';
  private readonly USER_KEY = 'current_user';

  private _http : HttpClient = inject(HttpClient);
  private _router : Router = inject(Router);

  //Signals : 
  private isConnectedSignal = signal<boolean>(this.hasToken());
  public readonly isConnected = this.isConnectedSignal;

  private isAdminSignal = signal<boolean>(this.hasAdminRole());
  public readonly isAdmin = this.isAdminSignal;

  constructor() { }


    private hasToken(): boolean {
    return typeof window !== 'undefined' && !!localStorage.getItem(this.TOKEN_KEY);
  }

  private hasAdminRole(): boolean {
    if (typeof window === 'undefined') return false;

    const userJson = localStorage.getItem(this.USER_KEY);
    if (!userJson) return false;

    try {
      const user = JSON.parse(userJson);
      return user?.role === 'admin';
    } catch {
      return false;
    }
  }
/*
  getCurrentUser(): User | null {
    const userJson = localStorage.getItem(this.USER_KEY);
    if (userJson) {
      return JSON.parse(userJson);
    }
    return null;
  }
    */
}
