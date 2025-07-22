import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private _message = signal<string | null>(null);
  private _visible = signal(false);
  private timeoutHandle: any;

  showToast(message: string, duration: number = 3000): void {

    if (this.timeoutHandle) {
      clearTimeout(this.timeoutHandle);
    }

    this._message.set(message);
    this._visible.set(true);

    this.timeoutHandle = setTimeout(() => {
      this._visible.set(false);
      this._message.set(null);
    }, duration);
  }

  message = () => this._message();
  show = () => this._visible();
}

