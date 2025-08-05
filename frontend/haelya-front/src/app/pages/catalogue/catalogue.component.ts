import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../core/services/product.service';
import { CategoryService } from '../../core/services/category.service';
import { BrandService } from '../../core/services/brand.service';
import { Product } from '../../core/models/product/product';
import { ProductcardComponent } from './productcard/productcard.component';
import { ProductQuery } from '../../core/models/product/productquery';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-catalogue',
  imports: [ProductcardComponent, CommonModule, FormsModule],
  templateUrl: './catalogue.component.html',
  styleUrl: './catalogue.component.scss'
})
export class CatalogueComponent implements OnInit {
  private readonly _productService: ProductService = inject(ProductService);
  private readonly _categoryService: CategoryService = inject(CategoryService);
  private readonly _brandService: BrandService = inject(BrandService);

  products: Product[] = [];
  totalCount: number = 0;
  loading: boolean = true;
  categories: { id: number; name: string }[] = [];
  brands: { id: number; name: string }[] = [];

  query: ProductQuery = {
    page: 1,
    pageSize: 20,
    sortBy: 'name',
    sortDirection: 'asc'
  };

  get totalPages(): number {
    return Math.ceil(this.totalCount / (this.query.pageSize ?? 20));
  }


  ngOnInit(): void {
    this.applyFilters();

  }

  totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }


  loadProducts(): void {
    this.loading = true;

    this._productService.getFilteredVisible(this.query).subscribe({
      next: (res) => {
        this.products = res.items;
        this.totalCount = res.totalCount;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erreur lors du chargement des produits', err);
        this.loading = false;
      }
    });
  }

  loadCategories(): void {
    this._categoryService.getAllVisible().subscribe({
      next: (data) => (this.categories = data),
      error: (err) => console.error('Erreur chargement catégories', err)
    });
  }

  loadBrands(): void {
    this._brandService.getAllVisible().subscribe({
      next: (data) => (this.brands = data),
      error: (err) => console.error('Erreur chargement marques', err)
    });
  }

  onPageChange(page: number): void {
    this.query.page = page;
    this.loadProducts();
  }

  applyFilters(): void {
  this.query.page = 1;
  this.loadProducts();
}

onResetFilters(): void {
  this.query = {
    page: 1,
    pageSize: 20,
    sortBy: 'name',
    sortDirection: 'asc'
  };
  this.loadProducts();
}


}
