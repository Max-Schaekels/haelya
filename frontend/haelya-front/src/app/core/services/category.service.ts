import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category/category';
import { CategoryCreate } from '../models/category/category-create';
import { CategoryUpdate } from '../models/category/category-update';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private readonly _apiUrl = environment.apiUrl;
  private readonly _http : HttpClient = inject(HttpClient);

  //Public

  getAllVisible(): Observable<Category[]>{
    return this._http.get<Category[]>(`${this._apiUrl}/Category`)
  }

  getCategoryById(id : number): Observable<Category>{
    return this._http.get<Category>(`${this._apiUrl}/Category/${id}`)
  }

  //Admin

  getAllAdmin(): Observable<Category[]>{
    return this._http.get<Category[]>(`${this._apiUrl}/Category/admin`)
  }

  createCategory(category : CategoryCreate ) : Observable<Category>{
    return this._http.post<Category>(`${this._apiUrl}/Category`, category)
  }

  updateCategory(update : CategoryUpdate, id : number) : Observable<void>{
    return this._http.put<void>(`${this._apiUrl}/Category/${id}`, update)
  }

  setActiveCategory(id : number, isActive : boolean) : Observable<void>{
    return this._http.put<void>(`${this._apiUrl}/Category/${id}/active`, isActive)
  }


}
