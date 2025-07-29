import { Component, Input } from '@angular/core';
import { Product } from '../../../core/models/product/product';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-productcard',
  imports: [CommonModule],
  templateUrl: './productcard.component.html',
  styleUrl: './productcard.component.scss'
})
export class ProductcardComponent  {
  @Input() product! : Product;


}
