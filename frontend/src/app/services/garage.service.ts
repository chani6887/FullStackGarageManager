import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Garage } from '../models/garage';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class GarageService {
  private base = `${environment.apiUrl}/api/garages`;

  
  constructor(private http: HttpClient) {}

  getFromGov(limit = 10): Observable<Garage[]> {
    return this.http.get<Garage[]>(`${this.base}/fromgov?limit=${limit}`);
  }


  getSaved(): Observable<Garage[]> {
    return this.http.get<Garage[]>(this.base);
  }

  addMany(garages: Garage[]): Observable<Garage[]> {
    return this.http.post<Garage[]>(`${this.base}/add`, garages);
  }
}
