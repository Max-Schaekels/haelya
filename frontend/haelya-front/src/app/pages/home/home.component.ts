import { Component, inject, OnInit, PLATFORM_ID } from '@angular/core';
import { CarouselComponent } from "../../shared/components/carousel/carousel.component";
import { AuthService } from '../../core/services/auth.service';



@Component({
  selector: 'app-home',
  imports: [CarouselComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent  {

  constructor(private authService: AuthService) {}

  testRefresh() {
    this.authService.refreshToken().subscribe({
      next: (res) => {
        console.log('[Test Refresh] Nouveau token :', res.accessToken);
      },
      error: (err) => {
        console.error('[Test Refresh] Erreur', err);
      },
    });
  }

}
