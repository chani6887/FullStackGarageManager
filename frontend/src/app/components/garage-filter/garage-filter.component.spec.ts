import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GarageFilterComponent } from './garage-filter.component';

describe('GarageFilterComponent', () => {
  let component: GarageFilterComponent;
  let fixture: ComponentFixture<GarageFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GarageFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GarageFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
