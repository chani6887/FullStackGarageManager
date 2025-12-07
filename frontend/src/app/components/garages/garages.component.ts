import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { trigger, transition, style, animate } from '@angular/animations';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableModule } from '@angular/material/table';
import { GarageService } from '../../services/garage.service';
import { Garage } from '../../models/garage';
import { finalize } from 'rxjs/operators';
import { MatCardModule } from '@angular/material/card';
import { GarageFilterComponent } from '../garage-filter/garage-filter.component';

@Component({
  selector: 'app-garages',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
    MatButtonModule,
    MatProgressBarModule,
    MatTableModule,
    MatCardModule,
    GarageFilterComponent
  ],
  templateUrl: './garages.component.html',
  styleUrls: ['./garages.component.scss'],
  animations: [
    trigger('fadeSlide', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(10px)' }),
        animate('250ms ease-out',
          style({ opacity: 1, transform: 'translateY(0)' })
        )
      ]),
      transition(':leave', [
        animate('200ms ease-in',
          style({ opacity: 0, transform: 'translateY(5px)' })
        )
      ])
    ])
  ]
  
})
export class GaragesComponent implements OnInit {
  govList: Garage[] = [];
  savedList: Garage[] = [];

  filteredList: Garage[] = [];  
  cityCtrl = new FormControl('');

  selCtrl = new FormControl([]);
  loading = false;

  constructor(private garageService: GarageService) {}

  ngOnInit(): void {
    this.loading = true;

    this.garageService.getFromGov(20)
      .pipe(finalize(() => this.loading = false))
      .subscribe(gov => this.govList = gov);

      this.garageService.getSaved()
      .subscribe(saved => {
        this.savedList = saved;
        this.filteredList = saved;
      });
  }

  
  onAdd() {
    const selected: Garage[] = this.selCtrl.value || [];

    const toAdd = selected.filter(s =>
      !this.savedList.some(x =>
        (x.externalId && s.externalId && x.externalId === s.externalId) ||
        (x.name === s.name && x.address === s.address)
      )
    );

    if (!toAdd.length) return;

    this.loading = true;

    this.garageService.addMany(toAdd)
      .pipe(finalize(() => this.loading = false))
      .subscribe(updated => {
        this.savedList = updated;
        this.selCtrl.setValue([]);
      });
  }
  onCityFilterChange(city: string | null) {
    this.filteredList = city
      ? this.savedList.filter(g => g.city === city)
      : this.savedList;
  }
  
}
