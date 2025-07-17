import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { UpdateUser } from '../models/update-user';
import { ChangePassword } from '../models/change-password';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly _apiUrl = environment.apiUrl;
  
  private _http : HttpClient = inject(HttpClient);
  

  constructor() { }
 
  //User :

  getprofile() : Observable<User> {
    return this._http.get<User>(`${this._apiUrl}/user/profil`)
  }

  updateProfil( update : UpdateUser) : Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/user`, update)
  }

  changePassword(payload : ChangePassword) : Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/user/password`, payload)
  }

  deleteAccount() : Observable<void> {
    return this._http.delete<void>(`${this._apiUrl}/user`)
  }

  //Admin only :
  getAllUsers(): Observable<User[]> {
    return this._http.get<User[]>(`${this._apiUrl}/user`)
  }

  getUserById(id : number) : Observable<User> {
    return this._http.get<User>(`${this._apiUrl}/user/${id}`)
  }
}
