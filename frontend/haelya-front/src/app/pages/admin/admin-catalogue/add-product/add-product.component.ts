import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../../../core/services/product.service';
import { Router } from '@angular/router';
import { ToastService } from '../../../../core/services/toast.service';
import { ProductCreate } from '../../../../core/models/product/product-create';
import { CategoryService } from '../../../../core/services/category.service';
import { BrandService } from '../../../../core/services/brand.service';

@Component({
  selector: 'app-add-product',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent implements OnInit {
  private readonly _fb: FormBuilder = inject(FormBuilder);
  private readonly _productService: ProductService = inject(ProductService);
  private readonly _router: Router = inject(Router);
  private readonly _toastService: ToastService = inject(ToastService);
  private readonly _categoryService: CategoryService = inject(CategoryService);
  private readonly _brandService: BrandService = inject(BrandService);

  categories: { id: number; name: string }[] = [];
  brands: { id: number; name: string }[] = [];

  isSubmitting: boolean = false;
  errorMessage: string | null = null;

  productForm: FormGroup;

  constructor() {
    this.productForm = this._fb.group({
      name: [null, [Validators.required, Validators.maxLength(255)]],
      description: [null, [Validators.maxLength(1024)]],
      imageUrl: [null, [Validators.required, Validators.maxLength(1024)]],
      supplierPrice: [0, [Validators.required]],
      margin: [0, [Validators.required]],
      stock: [0, [Validators.required]],
      categoryId: [null, [Validators.required]],
      brandId: [null, [Validators.required]]
    })
  }

  ngOnInit(): void {
    this.loadCategories();
    this.loadBrands();
  }

  onSubmit(): void {
    if (this.productForm.invalid) return;

    this.isSubmitting = true;
    this.errorMessage = null;

    const v = this.productForm.value;

    const dto: ProductCreate = {
      name: v.name,
      description: v.description,
      imageUrl: v.imageUrl,
      supplierPrice: Number(v.supplierPrice),
      margin: Number(v.margin),
      stock: Number(v.stock),
      categoryId: Number(v.categoryId),
      brandId: Number(v.brandId)

    };

    this._productService.createProduct(dto).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.productForm.reset();
        this.errorMessage = null;
        this._router.navigate(['admin/catalogue']);
        this._toastService.showToast('Produit ajouté avec succès');
      },
      error: (err) => {
        this.isSubmitting = false;
        this.errorMessage = err.error?.message || 'Une erreur est survenue lors de la création du produit';
      }
    })
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

  backCatalogue(): void{
    this._router.navigate(['/admin/catalogue']);
  }

}
