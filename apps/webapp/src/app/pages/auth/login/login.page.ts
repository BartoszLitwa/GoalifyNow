import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  template: `
    <div class="min-h-screen bg-slate-50 flex items-center justify-center px-4">
      <div class="w-full max-w-md">
        <div class="text-center mb-8">
          <a routerLink="/" class="text-3xl font-extrabold">
            <span class="text-indigo-600">Goalify</span><span class="text-slate-900">Now</span>
          </a>
          <p class="text-slate-500 mt-2">Welcome back</p>
        </div>
        <div class="bg-white rounded-2xl shadow-sm border border-slate-200 p-8">
          <div class="space-y-4">
            <div>
              <label class="block text-sm font-medium text-slate-700 mb-1">Email</label>
              <input type="email" [ngModel]="email()" (ngModelChange)="email.set($event)"
                     class="w-full px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-500/20 outline-none transition-all"
                     placeholder="you@example.com">
            </div>
            <div>
              <label class="block text-sm font-medium text-slate-700 mb-1">Password</label>
              <input type="password" [ngModel]="password()" (ngModelChange)="password.set($event)"
                     class="w-full px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-500/20 outline-none transition-all"
                     placeholder="Enter your password"
                     (keyup.enter)="onLogin()">
            </div>
            <button (click)="onLogin()" [disabled]="loading()"
                    class="w-full py-3 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors disabled:opacity-50">
              {{ loading() ? 'Logging in...' : 'Log In' }}
            </button>
          </div>
          @if (error()) {
            <p class="mt-4 text-sm text-center text-red-600">{{ error() }}</p>
          }
          <div class="mt-6 text-center text-sm text-slate-500">
            Don't have an account? <a routerLink="/auth/register" class="text-indigo-600 font-medium hover:text-indigo-700">Sign up</a>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class LoginPage {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly email = signal('');
  readonly password = signal('');
  readonly error = signal('');
  readonly loading = signal(false);

  onLogin() {
    this.loading.set(true);
    this.error.set('');
    this.authService.login(this.email(), this.password()).subscribe({
      next: (res) => {
        if (res.accessToken) {
          this.authService.setToken(res.accessToken);
          this.router.navigateByUrl('/app/dashboard');
        } else {
          this.error.set(res.message || 'Login failed');
        }
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Login failed. Check your credentials.');
        this.loading.set(false);
      },
    });
  }
}
