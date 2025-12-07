import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideAnimations } from '@angular/platform-browser/animations';


bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),
    provideAnimationsAsync(), provideAnimationsAsync() ,  provideAnimations()// מייצר HttpClient לשימוש באמצעות ההזרקה
  ]
}).catch(err => console.error(err));
