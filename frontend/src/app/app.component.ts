import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GaragesComponent } from './components/garages/garages.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, GaragesComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {}
