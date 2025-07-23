import { Component, inject, OnInit, PLATFORM_ID } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { ScrollToTopComponent } from "./shared/components/scroll-to-top/scroll-to-top.component";
import { HeaderComponent } from "./shared/components/header/header.component";
import { FooterComponent } from "./shared/components/footer/footer.component";
import { isPlatformBrowser } from '@angular/common';
import { filter } from 'rxjs';
import { ToastComponent } from "./shared/components/toast/toast.component";


@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'haelya-front';
  private router = inject(Router);
  private platformId = inject(PLATFORM_ID);

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.router.events
        .pipe(filter(event => event instanceof NavigationEnd))
        .subscribe(() => {
          window.scrollTo({ top: 0, behavior: 'auto' });
        });
    }
  }

}
