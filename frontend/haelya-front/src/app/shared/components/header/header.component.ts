import { Component, ElementRef, HostListener, inject, ViewChild } from '@angular/core';
import { StickyHeaderDirective } from '../../directives/sticky-header.directive';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';
import { LoginComponent } from "../../../pages/auth/login/login.component";

@Component({
  selector: 'app-header',
  imports: [StickyHeaderDirective, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isMenuOpen: boolean = false;
  isSearchActive: boolean = false;
  isDropdownOpen: string | null = null;
  public authService : AuthService = inject(AuthService);

  @ViewChild('searchInput') searchInput!: ElementRef;

  toggleSearch(event: Event): void {
    event.preventDefault();
    this.isSearchActive = !this.isSearchActive;

    if (this.isSearchActive) {
      setTimeout(() => {
        this.searchInput.nativeElement.focus();
      }, 0);
      console.log(this.searchInput);
    }
  }


  @HostListener('document:click', ['$event'])
  @HostListener('document:touchstart', ['$event'])
  onDocumentClick(event: Event): void {
    const target = event.target as HTMLElement;

    const clickedInside =
      target.closest('.search-toggle') || target.closest('.search-box');

    if (!clickedInside) {
      this.isSearchActive = false;
    }
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  showDropdown(id: string) {
    this.isDropdownOpen = id;
  }

  hideDropdown(id: string) {
    if (this.isDropdownOpen === id) {
      this.isDropdownOpen = null;
    }
  }

  logout():void{
    this.authService.logout();
  }

}
