import { Component, inject, OnInit } from '@angular/core';
import { PagedResult } from '../../../core/models/product/pagedresult';
import { Product } from '../../../core/models/product/product';
import { ProductQuery } from '../../../core/models/product/productquery';
import { ProductService } from '../../../core/services/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrandService } from '../../../core/services/brand.service';
import { CategoryService } from '../../../core/services/category.service';

@Component({
  selector: 'app-admin-catalogue',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-catalogue.component.html',
  styleUrl: './admin-catalogue.component.scss'
})
export class AdminCatalogueComponent implements OnInit {
  private readonly _productService = inject(ProductService);
  private readonly _categoryService = inject(CategoryService);
  private readonly _brandService = inject(BrandService);

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
    this.loadCategories();
    this.loadBrands();
    this.loadProducts();
  }

  totalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, i) => i + 1);
  }

  loadProducts(): void {
    this.loading = true;
    this._productService.getFilteredAdmin(this.query).subscribe({
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
    this._categoryService.getAllAdmin().subscribe({
      next: (data) => (this.categories = data),
      error: (err) => console.error('Erreur chargement catégories', err)
    });
  }

  loadBrands(): void {
    this._brandService.getAllAdmin().subscribe({
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

  onFilterSubmit(event: Event): void {
    event.preventDefault();
    this.query.page = 1;
    this.loadProducts();
  }

  getCategoryName(id: number | null | undefined): string {
    return this.categories.find(c => c.id === id)?.name ?? '—';
  }

  getBrandName(id: number | null | undefined): string {
    return this.brands.find(b => b.id === id)?.name ?? '—';
  }

  onAddProduct(): void {
    //TODO : rediriger vers la création 
  }

  onViewProduct(id: number): void {
    //TODO rediriger vers le détail
  }

  onEditProduct(id: number): void {
    //TODO rediriger vers le formulaire d’édition
  }

  onDeleteProduct(id: number): void {
    //TODO confirmation puis appel service de suppression
  }
}
