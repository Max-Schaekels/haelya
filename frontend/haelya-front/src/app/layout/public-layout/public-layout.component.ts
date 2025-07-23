import { Component } from '@angular/core';
import { ScrollToTopComponent } from "../../shared/components/scroll-to-top/scroll-to-top.component";
import { ToastComponent } from "../../shared/components/toast/toast.component";
import { FooterComponent } from "../../shared/components/footer/footer.component";
import { RouterModule } from "@angular/router";
import { HeaderComponent } from "../../shared/components/header/header.component";

@Component({
  selector: 'app-public-layout',
  imports: [ScrollToTopComponent, ToastComponent, FooterComponent, RouterModule, HeaderComponent],
  templateUrl: './public-layout.component.html',
  styleUrl: './public-layout.component.scss'
})
export class PublicLayoutComponent {

}
