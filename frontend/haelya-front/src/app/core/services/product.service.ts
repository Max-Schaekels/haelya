import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Product } from '../models/product/product';
import { Observable } from 'rxjs';
import { ProductCreate } from '../models/product/product-create';
import { ProductUpdate } from '../models/product/product-update';
import { ProductUpdatePrice } from '../models/product/product-update-price';
import { ProductUpdateMargin } from '../models/product/product-update-margin';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly _apiUrl = environment.apiUrl;
  private readonly _http: HttpClient = inject(HttpClient);

  //Public 

  getAllVisible(): Observable<Product[]> {
    return this._http.get<Product[]>(`${this._apiUrl}/Product`);
  }

  getProductById(id: number): Observable<Product> {
    return this._http.get<Product>(`${this._apiUrl}/Product/${id}`);
  }

  getProductBySlug(slug: string): Observable<Product> {
    return this._http.get<Product>(`${this._apiUrl}/Product/slug/${slug}`);
  }

  //Admin

  getAllAdmin(): Observable<Product[]> {
    return this._http.get<Product[]>(`${this._apiUrl}/Product/admin`);
  }

  createProduct(product: ProductCreate): Observable<Product> {
    return this._http.post<Product>(`${this._apiUrl}/Product`, product);
  }

  updateProduct(update: ProductUpdate, id: number): Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/Product/${id}`, update);
  }

  updateProductPrice(update: ProductUpdatePrice): Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/Product/price`, update);
  }

  updateProductMargin(update: ProductUpdateMargin): Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/Product/margin`, update);
  }

  setActiveProduct(id: number, isActive: boolean): Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/Product/${id}/active`, isActive);
  }

  setFeaturedProduct(id: number, featured: boolean): Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/Product/${id}/featured`, featured);
  }

  setInSlideProduct(id: number, inSlide: boolean): Observable<void> {
    return this._http.put<void>(`${this._apiUrl}/Product/${id}/in-slide`, inSlide);
  }


}
