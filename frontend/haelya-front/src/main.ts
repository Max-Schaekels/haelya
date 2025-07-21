import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import 'aos/dist/aos.css';
import AOS from 'aos';

bootstrapApplication(AppComponent, appConfig)
  .then(() => {
    AOS.init({
      duration: 1000,
      once: true,
    });
  })
  .catch((err) => console.error(err));


