import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appStickyHeader]'
})
export class StickyHeaderDirective {

  constructor(private el: ElementRef, private renderer: Renderer2) { }

  @HostListener('window:scroll', [])
  onWindowSroll() {
    const offset = window.scrollY || document.documentElement.scrollTop;
    if (offset > 100) {
      this.renderer.addClass(this.el.nativeElement, 'is-sticky');
    } else {
      this.renderer.removeClass(this.el.nativeElement, 'is-sticky');
    }
  }

}
