import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-quantity-selector',
  imports: [],
  templateUrl: './quantity-selector.component.html',
  styleUrl: './quantity-selector.component.scss',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => QuantitySelectorComponent),
      multi: true
    }
  ]
})
export class QuantitySelectorComponent implements ControlValueAccessor {

  isDisabled: boolean = false;
  value: number = 1;
  @Input() min: number = 0;
  @Input() max: number = 99;

  private onChange = (value: number) => { };
  private onTouched = () => { };

  writeValue(value: number): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  increment(): void {
    if (this.value < this.max) {
      this.value++;
      this.onChange(this.value);
      this.onTouched();
    }
  }

  decrement(): void {
    if (this.value > this.min) {
      this.value--;
      this.onChange(this.value);
      this.onTouched();
    }
  }

  onInput(event: Event): void {
    const newValue = +(event.target as HTMLInputElement).value;
    if (!isNaN(newValue) && newValue >= this.min && newValue <= this.max) {
      this.value = newValue;
      this.onChange(this.value);
      this.onTouched();
    }
  }

}
