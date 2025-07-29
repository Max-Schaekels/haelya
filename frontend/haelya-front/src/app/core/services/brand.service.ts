import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Brand } from '../models/brand/brand';
import { BrandCreate } from '../models/brand/brand-create';
import { BrandUpdate } from '../models/brand/brand-update';
import { Observable } from 'rxjs';
import { Category } from '../models/category/category';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  private readonly _apiUrl = environment.apiUrl;
  private readonly _http : HttpClient = inject(HttpClient);

    //Public
  
    getAllVisible(): Observable<Brand[]>{
      return this._http.get<Brand[]>(`${this._apiUrl}/Brand`)
    }
  
    getBrandById(id : number): Observable<Brand>{
      return this._http.get<Brand>(`${this._apiUrl}/Brand/${id}`)
    }
  
    //Admin
  
    getAllAdmin(): Observable<Brand[]>{
      return this._http.get<Brand[]>(`${this._apiUrl}/Brand/admin`)
    }
  
    createBrand(brand : BrandCreate ) : Observable<Brand>{
      return this._http.post<Brand>(`${this._apiUrl}/Brand`, brand)
    }
  
    updateBrand(update : BrandUpdate, id : number) : Observable<void>{
      return this._http.put<void>(`${this._apiUrl}/Brand/${id}`, update)
    }
  
    setActiveBrand(id : number, isActive : boolean) : Observable<void>{
      return this._http.put<void>(`${this._apiUrl}/Brand/${id}/active`, isActive)
    }
}
