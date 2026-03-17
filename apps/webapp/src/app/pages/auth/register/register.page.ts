import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../core/auth.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  template: `
    <div class="min-h-screen bg-slate-50 flex items-center justify-center px-4">
      <div class="w-full max-w-md">
        <div class="text-center mb-8">
          <a routerLink="/" class="text-3xl font-extrabold">
            <span class="text-indigo-600">Goalify</span><span class="text-slate-900">Now</span>
          </a>
          <p class="text-slate-500 mt-2">Create your free account</p>
        </div>
        <div class="bg-white rounded-2xl shadow-sm border border-slate-200 p-8">
          <div class="space-y-4">
            <div>
              <label class="block text-sm font-medium text-slate-700 mb-1">Display Name</label>
              <input type="text" [ngModel]="displayName()" (ngModelChange)="displayName.set($event)"
                     class="w-full px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-500/20 outline-none transition-all"
                     placeholder="Your name">
            </div>
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
                     placeholder="Create a password"
                     (keyup.enter)="onRegister()">
            </div>
            <button (click)="onRegister()" [disabled]="loading()"
                    class="w-full py-3 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors disabled:opacity-50">
              {{ loading() ? 'Creating account...' : 'Create Account' }}
            </button>
          </div>
          @if (error()) {
            <p class="mt-4 text-sm text-center text-red-600">{{ error() }}</p>
          }
          <p class="mt-4 text-xs text-center text-slate-400">
            By signing up, you agree to our Terms of Service and Privacy Policy.
          </p>
          <div class="mt-6 text-center text-sm text-slate-500">
            Already have an account? <a routerLink="/auth/login" class="text-indigo-600 font-medium hover:text-indigo-700">Log in</a>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class RegisterPage {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly displayName = signal('');
  readonly email = signal('');
  readonly password = signal('');
  readonly error = signal('');
  readonly loading = signal(false);

  onRegister() {
    if (!this.email() || !this.password() || !this.displayName()) {
      this.error.set('Please fill in all fields.');
      return;
    }
    this.loading.set(true);
    this.error.set('');
    this.authService.register(this.email(), this.password(), this.displayName()).subscribe({
      next: (res) => {
        if (res.accessToken) {
          this.authService.setToken(res.accessToken);
          this.router.navigateByUrl('/onboarding');
        } else {
          this.error.set('Registration failed. Please try again.');
        }
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Registration failed. Try a different email.');
        this.loading.set(false);
      },
    });
  }
}
