import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../core/models/product/product';
import { ActivatedRoute } from '@angular/router';
import { TabsComponent } from '../../shared/components/tabs/tabs.component';
import { QuantitySelectorComponent } from '../../shared/components/quantity-selector/quantity-selector.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, TabsComponent, QuantitySelectorComponent,FormsModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  private readonly _productService : ProductService = inject(ProductService);
  private _activeRoute = inject(ActivatedRoute);

  
  product : Product | null = null;
  loading : boolean = false;
  errorMessage : string | null = null; 
  slug! : string;
  quantity: number = 1;
  
  ngOnInit(): void {
    this.loading = true;
    this.errorMessage = null;
    this.slug = this._activeRoute.snapshot.params['slug'];
    if (!this.slug) {
      this.errorMessage = "Slug invalide";
      this.loading = false;
      return;
    }
    this._productService.getProductBySlug(this.slug).subscribe({
      next: (data) => {
        this.product = data;
        this.loading = false;
      },
      error : (err) => {
        this.errorMessage = "Produit introuvable";
        this.loading = false;

      }

    })
  }


}
