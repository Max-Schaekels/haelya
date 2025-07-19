import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-scroll-to-top',
  imports: [],
  templateUrl: './scroll-to-top.component.html',
  styleUrl: './scroll-to-top.component.scss'
})
export class ScrollToTopComponent {
  isVisible : boolean = false; 

  @HostListener('window:scroll', [])
  onWindowsScroll(): void{
    this.isVisible = window.pageYOffset > 100;
  }

  scrollToTop() : void{
    window.scrollTo({top: 0, behavior: 'smooth'});
  }

}
