import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface GoalDto {
  id: string;
  name: string;
  category: string;
  metricUnit: string;
  targetValue: number;
  currentValue: number;
  deadline: string | null;
  status: string;
  createdAt: string;
  milestones: MilestoneDto[];
}

export interface MilestoneDto {
  id: string;
  name: string;
  targetValue: number;
  isReached: boolean;
  reachedAt: string | null;
}

export interface HabitDto {
  id: string;
  name: string;
  frequency: string;
  currentStreak: number;
  longestStreak: number;
  todayDone: boolean;
  completionRate: number;
  lastWeek: boolean[];
}

@Injectable({ providedIn: 'root' })
export class GoalsApiService {
  private readonly api = `${environment.apiBaseUrl}/api`;

  constructor(private readonly http: HttpClient) {}

  listGoals(status?: string) {
    let params = new HttpParams();
    if (status) params = params.set('status', status);
    return this.http.get<GoalDto[]>(`${this.api}/goals`, { params });
  }

  getGoal(id: string) {
    return this.http.get<GoalDto>(`${this.api}/goals/${id}`);
  }

  createGoal(data: { name: string; category: string; metricUnit: string; targetValue: number; deadline?: string; milestones?: { name: string; targetValue: number }[] }) {
    return this.http.post<{ id: string }>(`${this.api}/goals`, data);
  }

  updateGoal(id: string, data: Record<string, unknown>) {
    return this.http.put(`${this.api}/goals/${id}`, data);
  }

  archiveGoal(id: string) {
    return this.http.delete(`${this.api}/goals/${id}`);
  }

  recordProgress(goalId: string, value: number) {
    return this.http.post<{ currentValue: number; status: string; milestonesReached: string[] }>(
      `${this.api}/goals/${goalId}/progress`, { goalId, value }
    );
  }

  listHabits() {
    return this.http.get<HabitDto[]>(`${this.api}/habits`);
  }

  createHabit(data: { name: string; frequency: string }) {
    return this.http.post<{ id: string }>(`${this.api}/habits`, data);
  }

  checkIn(habitId: string) {
    return this.http.post<{ currentStreak: number; longestStreak: number; completed: boolean }>(
      `${this.api}/habits/${habitId}/check-in`, { habitId }
    );
  }

  getHabitHistory(habitId: string) {
    return this.http.get<{ date: string; completed: boolean }[]>(`${this.api}/habits/${habitId}/history`);
  }
}
