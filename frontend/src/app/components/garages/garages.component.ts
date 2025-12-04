import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { GarageService } from '../../services/garage.service';
import { Garage } from '../../models/garage';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MatSelectModule } from '@angular/material/select';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-garages',
  templateUrl: './garages.component.html',
  styleUrls: ['./garages.component.scss']
})
export class GaragesComponent implements OnInit {
  govList: Garage[] = [];
  savedList: Garage[] = [];
  selCtrl = new FormControl([]);
  loading = false;

  constructor(private garageService: GarageService) {}

  ngOnInit(): void {
    this.loading = true;
    // קראי לשני השירותים — fromgov & saved
    this.garageService.getFromGov(20)
      .pipe(finalize(() => this.loading = false))
      .subscribe(gov => this.govList = gov, err => { this.loading = false; console.error(err); });

    this.garageService.getSaved().subscribe(saved => this.savedList = saved, err => console.error(err));
  }

  onAdd() {
    const selected: Garage[] = this.selCtrl.value || [];
    // סינון מקומי לפני שליחה: אם כבר קיים בחיבור שמור (savedList)
    const toAdd = selected.filter(s =>
      !this.savedList.some(x =>
        (x.externalId && s.externalId && x.externalId === s.externalId)
        || (x.name === s.name && x.address === s.address)
      )
    );

    if (!toAdd.length) {
      // אפשר להראות snackBar: "אין פריטים להוסיף"
      return;
    }

    this.loading = true;
    this.garageService.addMany(toAdd)
      .pipe(finalize(() => this.loading = false))
      .subscribe(updated => {
        this.savedList = updated;
        this.selCtrl.setValue([]);
      }, err => {
        console.error(err);
      });
  }
}
