import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface WeightPoint {
  date: string;
  weightKg: number;
  trendWeightKg: number | null;
}

export interface MeasurementDto {
  id: string;
  date: string;
  waist: number | null;
  chest: number | null;
  hips: number | null;
  bicepsL: number | null;
  bicepsR: number | null;
  thighL: number | null;
  thighR: number | null;
  bodyFatPct: number | null;
}

export interface PhotoDto {
  id: string;
  date: string;
  poseType: string;
  notes: string | null;
}

export interface DashboardData {
  activeGoals: number;
  completedGoals: number;
  weekWorkouts: number;
  currentWeight: number | null;
  trendWeight: number | null;
  todayCalories: number;
  longestHabitStreak: number;
}

@Injectable({ providedIn: 'root' })
export class ProgressApiService {
  private readonly api = `${environment.apiBaseUrl}/api/progress`;

  constructor(private readonly http: HttpClient) {}

  logWeight(weightKg: number) {
    return this.http.post<{ weightKg: number; trendWeightKg: number }>(`${this.api}/weight`, { weightKg });
  }

  getWeightHistory(range = '30d') {
    return this.http.get<WeightPoint[]>(`${this.api}/weight`, { params: { range } });
  }

  logMeasurements(data: Record<string, number | null>) {
    return this.http.post<{ id: string }>(`${this.api}/measurements`, data);
  }

  getMeasurements() {
    return this.http.get<MeasurementDto[]>(`${this.api}/measurements`);
  }

  uploadPhoto(poseType: string, notes?: string) {
    return this.http.post<{ id: string; storagePath: string }>(`${this.api}/photos`, { poseType, notes });
  }

  listPhotos() {
    return this.http.get<PhotoDto[]>(`${this.api}/photos`);
  }

  getDashboard() {
    return this.http.get<DashboardData>(`${this.api}/dashboard`);
  }
}
