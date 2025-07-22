import { AfterViewInit, Component, Inject, PLATFORM_ID } from '@angular/core';
import SwiperCore from 'swiper';
import { Navigation, Pagination, Autoplay } from 'swiper/modules';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';

SwiperCore.use([Navigation, Pagination, Autoplay]);


@Component({
  selector: 'app-carousel',
  imports: [CommonModule],
  templateUrl: './carousel.component.html',
  styleUrl: './carousel.component.scss',
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  host:{
    'ngSkipHydration' :''
  }
})
export class CarouselComponent  {
  isBrowser: boolean = false;


  constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

  slides = [
    {
      title: 'Life of the Wild',
      text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit...',
      img: '/assets/images/main-banner1.jpg'
    },
    {
      title: 'Birds gonna be Happy',
      text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit...',
      img: '/assets/images/main-banner2.jpg'
    }
    
  ];



}


