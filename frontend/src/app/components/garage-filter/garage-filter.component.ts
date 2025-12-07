import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { Garage } from '../../models/garage';

@Component({
  selector: 'app-garage-filter',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule
  ],
  template: `
    <mat-form-field appearance="outline">
      <mat-label>סינון לפי יישוב</mat-label>
      <mat-select [formControl]="cityCtrl">
        <mat-option value="">הכול</mat-option>
        <mat-option *ngFor="let c of cities" [value]="c">
          {{ c }}
        </mat-option>
      </mat-select>
    </mat-form-field>
  `
})
export class GarageFilterComponent implements OnInit {

  @Input() garages: Garage[] = [];
  @Output() filterChanged = new EventEmitter<string>();

  cityCtrl = new FormControl('');
  cities: string[] = [];

  ngOnInit() {
    this.cities = [...new Set(this.garages.map(g => g.city))];

    this.cityCtrl.valueChanges.subscribe(value => {
      this.filterChanged.emit(value ?? '');
    });
  }
}
