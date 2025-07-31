import { Component, Input } from '@angular/core';
import { Product } from '../../../core/models/product/product';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-productcard',
  imports: [CommonModule, RouterLink],
  templateUrl: './productcard.component.html',
  styleUrl: './productcard.component.scss'
})
export class ProductcardComponent  {
  @Input() product! : Product;

  addToCart(event : MouseEvent): void {
    event.stopPropagation(); // éviter la propagation vers la balise a
    event.preventDefault(); // empeche l'execution du routerlink (redirection)
    // TODO : faire l'ajout du produit au panier quand celui ci sera créer. 
    //log de test tempo : 
    console.log(`Produit ajouté au panier : ${this.product.name}`)
  }


}
