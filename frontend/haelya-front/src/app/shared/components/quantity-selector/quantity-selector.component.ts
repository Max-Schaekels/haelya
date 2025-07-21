import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-quantity-selector',
  imports: [],
  templateUrl: './quantity-selector.component.html',
  styleUrl: './quantity-selector.component.scss'
})
export class QuantitySelectorComponent {
  @Input() quantity: number = 1;
  @Input() min : number = 0;
  @Input() max : number = 99;

  @Output() quantityChange = new EventEmitter<number>();

  increment(): void {
    if (this.quantity < this.max) {
      this.quantity++;
      this.quantityChange.emit(this.quantity);
    }
  }

  decrement(): void {
    if (this.quantity > this.min) {
      this.quantity--;
      this.quantityChange.emit(this.quantity);      
    }
  }

  onInput(event: Event): void{
    const value = +(event.target as HTMLInputElement).value;
    if (!isNaN(value) && value >= this.min && value <= this.max) {
      this.quantity = value;
      this.quantityChange.emit(this.quantity);
    }
  }

}
