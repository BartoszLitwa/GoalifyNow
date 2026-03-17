import { Component, inject, OnInit, DestroyRef } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-shell-layout',
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="flex h-screen bg-slate-50">
      <aside class="w-64 bg-white border-r border-slate-200 flex flex-col flex-shrink-0">
        <div class="p-6 border-b border-slate-100">
          <a routerLink="/" class="text-xl font-extrabold">
            <span class="text-indigo-600">Goalify</span><span class="text-slate-900">Now</span>
          </a>
        </div>
        <nav class="flex-1 p-4 space-y-1">
          @for (item of navItems; track item.route) {
            <a [routerLink]="item.route" routerLinkActive="bg-indigo-50 text-indigo-700"
               class="flex items-center gap-3 px-4 py-3 rounded-xl text-sm font-medium text-slate-600 hover:bg-slate-50 hover:text-slate-900 transition-all">
              <i class="pi {{item.icon}} text-lg"></i>
              <span>{{item.label}}</span>
            </a>
          }
        </nav>
        <div class="p-4 border-t border-slate-100">
          <div class="flex items-center gap-3 px-4 py-3">
            <div class="w-9 h-9 rounded-full bg-indigo-100 flex items-center justify-center text-indigo-600 font-bold text-sm">
              {{ avatarLetter() }}
            </div>
            <div class="flex-1 min-w-0">
              <div class="text-sm font-medium text-slate-900 truncate">{{ displayName() }}</div>
              <div class="text-xs text-slate-400">{{ subscriptionTier() }}</div>
            </div>
            <button (click)="authService.logout()"
                    class="px-3 py-1.5 text-xs font-medium text-slate-500 hover:text-slate-700 hover:bg-slate-100 rounded-lg transition-colors">
              Logout
            </button>
          </div>
        </div>
      </aside>
      <main class="flex-1 overflow-y-auto">
        <router-outlet />
      </main>
    </div>
  `,
})
export class AppShellLayout implements OnInit {
  readonly authService = inject(AuthService);
  private readonly destroyRef = inject(DestroyRef);

  readonly navItems = [
    { route: '/app/dashboard', icon: 'pi-home', label: 'Dashboard' },
    { route: '/app/goals', icon: 'pi-flag', label: 'Goals' },
    { route: '/app/habits', icon: 'pi-bolt', label: 'Habits' },
    { route: '/app/workouts', icon: 'pi-heart', label: 'Workouts' },
    { route: '/app/nutrition', icon: 'pi-apple', label: 'Nutrition' },
    { route: '/app/progress', icon: 'pi-chart-line', label: 'Progress' },
  ];

  displayName(): string {
    return this.authService.profile()?.displayName ?? 'User';
  }

  subscriptionTier(): string {
    return this.authService.profile()?.subscriptionTier ?? 'Free Plan';
  }

  avatarLetter(): string {
    const name = this.authService.profile()?.displayName;
    return name && name.length > 0 ? name.charAt(0).toUpperCase() : '?';
  }

  ngOnInit() {
    this.authService.getProfile().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (p) => this.authService.profile.set(p),
      error: () => {},
    });
  }
}
