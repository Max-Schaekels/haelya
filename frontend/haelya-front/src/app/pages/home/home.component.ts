import { Component } from '@angular/core';
import { CarouselComponent } from "../../shared/components/carousel/carousel.component";
import { CountdownComponent } from '../../shared/components/countdown/countdown.component';

@Component({
  selector: 'app-home',
  imports: [CarouselComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  

}
