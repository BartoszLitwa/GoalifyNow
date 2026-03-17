import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface SessionDto {
  id: string;
  name: string;
  startedAt: string;
  completedAt: string | null;
  durationMinutes: number;
  exerciseCount: number;
  setCount: number;
  totalVolume: number;
  muscles: string[];
}

export interface ExerciseItem {
  id: string;
  name: string;
  primaryMuscle: string;
  secondaryMuscles: string;
  equipment: string;
  isCustom: boolean;
}

export interface TemplateDto {
  id: string;
  name: string;
  exercisesJson: string;
}

@Injectable({ providedIn: 'root' })
export class WorkoutsApiService {
  private readonly api = `${environment.apiBaseUrl}/api`;

  constructor(private readonly http: HttpClient) {}

  listSessions() {
    return this.http.get<SessionDto[]>(`${this.api}/workouts`);
  }

  getSession(id: string) {
    return this.http.get<any>(`${this.api}/workouts/${id}`);
  }

  startSession(name: string, templateId?: string) {
    return this.http.post<{ id: string }>(`${this.api}/workouts`, { name, templateId });
  }

  updateSession(sessionId: string, data: any) {
    return this.http.put(`${this.api}/workouts/${sessionId}`, { sessionId, ...data });
  }

  completeSession(sessionId: string) {
    return this.http.post<any>(`${this.api}/workouts/${sessionId}/complete`, { sessionId });
  }

  searchExercises(q?: string, muscle?: string) {
    let params = new HttpParams();
    if (q) params = params.set('q', q);
    if (muscle) params = params.set('muscle', muscle);
    return this.http.get<ExerciseItem[]>(`${this.api}/exercises`, { params });
  }

  createExercise(data: { name: string; primaryMuscle: string; equipment?: string }) {
    return this.http.post<{ id: string }>(`${this.api}/exercises`, data);
  }

  listTemplates() {
    return this.http.get<TemplateDto[]>(`${this.api}/workout-templates`);
  }

  saveTemplate(name: string, exercises: any[]) {
    return this.http.post<{ id: string }>(`${this.api}/workout-templates`, { name, exercises });
  }
}
