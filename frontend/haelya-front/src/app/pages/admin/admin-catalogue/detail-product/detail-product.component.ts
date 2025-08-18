import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../../core/services/product.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Product } from '../../../../core/models/product/product';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-detail-product',
  imports: [CommonModule, RouterModule],
  templateUrl: './detail-product.component.html',
  styleUrl: './detail-product.component.scss'
})
export class DetailProductComponent implements OnInit {
  private readonly _productService : ProductService = inject(ProductService);
  private readonly _route : ActivatedRoute = inject(ActivatedRoute);
  product!: Product | null;
  loading : boolean =  false;
  errorMessage: string | null =  null; 

  ngOnInit(): void {
    this.loading = true;
    this.errorMessage = null;
    const id = Number(this._route.snapshot.paramMap.get('id'));

    if(!id || isNaN(id)) {
      this.errorMessage = 'ID du produit invalide.';
      this.loading = false;
      return;
    }

    this._productService.getProductById(id).subscribe({
      next : (data) => {
        this.product = data;
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = 'Impossible de charger le produit.';
        this.loading = false; 
      }
    });
  }

}
