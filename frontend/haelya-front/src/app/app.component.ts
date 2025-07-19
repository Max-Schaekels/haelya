import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ScrollToTopComponent } from "./shared/components/scroll-to-top/scroll-to-top.component";
import { HeaderComponent } from "./shared/components/header/header.component";
import { FooterComponent } from "./shared/components/footer/footer.component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ScrollToTopComponent, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'haelya-front';
}
