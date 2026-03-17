import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

export interface UserProfile {
  id: string;
  email: string;
  displayName: string;
  avatarUrl: string | null;
  fitnessLevel: string;
  dateOfBirth: string | null;
  onboardingCompleted: boolean;
  subscriptionTier: string;
  weightUnit: string;
  distanceUnit: string;
  darkMode: boolean;
  selectedGoals: string;
  enabledModules: string;
}

interface LoginResponse {
  accessToken: string;
  message: string;
}

interface RegisterResponse {
  accessToken: string;
  userId: string;
  displayName: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly api = `${environment.apiBaseUrl}/api/auth`;
  private readonly tokenKey = 'goalify_token';

  readonly token = signal<string | null>(this.getStoredToken());
  readonly isAuthenticated = computed(() => !!this.token());
  readonly profile = signal<UserProfile | null>(null);

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
  ) {}

  login(email: string, password: string) {
    return this.http.post<LoginResponse>(`${this.api}/login`, { email, password });
  }

  register(email: string, password: string, displayName: string) {
    return this.http.post<RegisterResponse>(`${this.api}/register`, { email, password, displayName });
  }

  getProfile() {
    return this.http.get<UserProfile>(`${this.api}/me`);
  }

  updateProfile(data: Record<string, unknown>) {
    return this.http.put(`${this.api}/profile`, data);
  }

  completeOnboarding(data: { fitnessLevel: string; goals: string[]; weightUnit: string; distanceUnit: string }) {
    return this.http.post(`${this.api}/onboarding`, data);
  }

  changePassword(currentPassword: string, newPassword: string) {
    return this.http.post(`${this.api}/change-password`, { currentPassword, newPassword });
  }

  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
    this.token.set(token);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.token.set(null);
    this.profile.set(null);
    this.router.navigateByUrl('/auth/login');
  }

  private getStoredToken(): string | null {
    if (typeof window === 'undefined') return null;
    return localStorage.getItem(this.tokenKey);
  }
}
