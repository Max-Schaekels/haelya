import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { CategoryService } from '../../core/services/category.service';
import { BrandService } from '../../core/services/brand.service';
import { Product } from '../../core/models/product/product';
import { ProductcardComponent } from './productcard/productcard.component';

@Component({
  selector: 'app-catalogue',
  imports: [ProductcardComponent],
  templateUrl: './catalogue.component.html',
  styleUrl: './catalogue.component.scss'
})
export class CatalogueComponent implements OnInit {
  private readonly _productService: ProductService = inject(ProductService);
  private readonly _categoryService: CategoryService = inject(CategoryService);
  private readonly _brandService: BrandService = inject(BrandService);

  products: Product[] = [];
  loading: boolean = true;

  ngOnInit(): void {
    this._productService.getAllVisible().subscribe({
      next: (data) => {
        this.products = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur lors du chargement des produits', err);
        this.loading = false;
      }
    });
  }


}
