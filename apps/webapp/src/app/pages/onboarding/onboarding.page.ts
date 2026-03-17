import { Component, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-onboarding',
  imports: [RouterLink],
  template: `
    <div class="min-h-screen bg-slate-50 flex items-center justify-center px-4 py-12">
      <div class="w-full max-w-lg">
        <div class="text-center mb-8">
          <a routerLink="/" class="text-2xl font-extrabold">
            <span class="text-indigo-600">Goalify</span><span class="text-slate-900">Now</span>
          </a>
          <div class="flex items-center justify-center gap-2 mt-4">
            @for (s of [1,2,3]; track s) {
              <div class="h-1.5 rounded-full transition-all duration-300"
                   [class.w-12]="s === step()"
                   [class.w-6]="s !== step()"
                   [class.bg-indigo-600]="s <= step()"
                   [class.bg-slate-300]="s > step()"></div>
            }
          </div>
        </div>

        <div class="bg-white rounded-2xl shadow-sm border border-slate-200 p-8">
          @switch (step()) {
            @case (1) {
              <h2 class="text-2xl font-bold text-slate-900 mb-2">What are your goals?</h2>
              <p class="text-slate-500 mb-6">Select everything that applies. You can change this later.</p>
              <div class="grid grid-cols-2 gap-3">
                @for (goal of goalOptions; track goal.id) {
                  <button (click)="toggleGoal(goal.id)"
                          class="p-4 rounded-xl border-2 text-left transition-all"
                          [class.border-indigo-600]="selectedGoals().includes(goal.id)"
                          [class.bg-indigo-50]="selectedGoals().includes(goal.id)"
                          [class.border-slate-200]="!selectedGoals().includes(goal.id)">
                    <div class="text-xl mb-1">{{ goal.emoji }}</div>
                    <div class="text-sm font-semibold text-slate-900">{{ goal.label }}</div>
                  </button>
                }
              </div>
            }
            @case (2) {
              <h2 class="text-2xl font-bold text-slate-900 mb-2">Your fitness level</h2>
              <p class="text-slate-500 mb-6">This helps us personalize your experience.</p>
              <div class="space-y-3">
                @for (level of fitnessLevels; track level.id) {
                  <button (click)="fitnessLevel.set(level.id)"
                          class="w-full p-4 rounded-xl border-2 text-left transition-all"
                          [class.border-indigo-600]="fitnessLevel() === level.id"
                          [class.bg-indigo-50]="fitnessLevel() === level.id"
                          [class.border-slate-200]="fitnessLevel() !== level.id">
                    <div class="font-semibold text-slate-900">{{ level.label }}</div>
                    <div class="text-sm text-slate-500">{{ level.description }}</div>
                  </button>
                }
              </div>
            }
            @case (3) {
              <h2 class="text-2xl font-bold text-slate-900 mb-2">Your preferences</h2>
              <p class="text-slate-500 mb-6">We'll use these throughout the app.</p>
              <div class="space-y-4">
                <div>
                  <label class="block text-sm font-medium text-slate-700 mb-2">Weight unit</label>
                  <div class="flex gap-3">
                    <button (click)="weightUnit.set('kg')"
                            class="flex-1 py-3 rounded-xl border-2 font-semibold transition-all"
                            [class.border-indigo-600]="weightUnit() === 'kg'"
                            [class.bg-indigo-50]="weightUnit() === 'kg'"
                            [class.text-indigo-600]="weightUnit() === 'kg'"
                            [class.border-slate-200]="weightUnit() !== 'kg'">kg</button>
                    <button (click)="weightUnit.set('lbs')"
                            class="flex-1 py-3 rounded-xl border-2 font-semibold transition-all"
                            [class.border-indigo-600]="weightUnit() === 'lbs'"
                            [class.bg-indigo-50]="weightUnit() === 'lbs'"
                            [class.text-indigo-600]="weightUnit() === 'lbs'"
                            [class.border-slate-200]="weightUnit() !== 'lbs'">lbs</button>
                  </div>
                </div>
                <div>
                  <label class="block text-sm font-medium text-slate-700 mb-2">Distance unit</label>
                  <div class="flex gap-3">
                    <button (click)="distanceUnit.set('km')"
                            class="flex-1 py-3 rounded-xl border-2 font-semibold transition-all"
                            [class.border-indigo-600]="distanceUnit() === 'km'"
                            [class.bg-indigo-50]="distanceUnit() === 'km'"
                            [class.text-indigo-600]="distanceUnit() === 'km'"
                            [class.border-slate-200]="distanceUnit() !== 'km'">km</button>
                    <button (click)="distanceUnit.set('mi')"
                            class="flex-1 py-3 rounded-xl border-2 font-semibold transition-all"
                            [class.border-indigo-600]="distanceUnit() === 'mi'"
                            [class.bg-indigo-50]="distanceUnit() === 'mi'"
                            [class.text-indigo-600]="distanceUnit() === 'mi'"
                            [class.border-slate-200]="distanceUnit() !== 'mi'">miles</button>
                  </div>
                </div>
              </div>
            }
          }

          @if (error()) {
            <p class="mt-4 text-sm text-center text-red-600">{{ error() }}</p>
          }

          <div class="flex items-center justify-between mt-8">
            @if (step() > 1) {
              <button (click)="step.set(step() - 1)" class="text-sm font-medium text-slate-500 hover:text-slate-700">Back</button>
            } @else {
              <div></div>
            }
            <button (click)="onNext()"
                    class="px-8 py-3 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors">
              {{ step() === 3 ? 'Get Started' : 'Continue' }}
            </button>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class OnboardingPage {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly step = signal(1);
  readonly selectedGoals = signal<string[]>([]);
  readonly fitnessLevel = signal('beginner');
  readonly weightUnit = signal('kg');
  readonly distanceUnit = signal('km');
  readonly error = signal('');

  readonly goalOptions = [
    { id: 'lose-weight', label: 'Lose weight', emoji: '⚖️' },
    { id: 'build-muscle', label: 'Build muscle', emoji: '💪' },
    { id: 'run-race', label: 'Run a race', emoji: '🏃' },
    { id: 'eat-healthier', label: 'Eat healthier', emoji: '🥗' },
    { id: 'build-habits', label: 'Build habits', emoji: '🔥' },
    { id: 'track-progress', label: 'Track progress', emoji: '📈' },
  ];

  readonly fitnessLevels = [
    { id: 'beginner', label: 'Beginner', description: 'New to exercise or getting back after a long break' },
    { id: 'intermediate', label: 'Intermediate', description: 'Exercise regularly, know the basics' },
    { id: 'advanced', label: 'Advanced', description: 'Serious training, looking for optimization' },
  ];

  toggleGoal(id: string) {
    const current = this.selectedGoals();
    if (current.includes(id)) {
      this.selectedGoals.set(current.filter(g => g !== id));
    } else {
      this.selectedGoals.set([...current, id]);
    }
  }

  onNext() {
    if (this.step() < 3) {
      this.step.set(this.step() + 1);
    } else {
      this.error.set('');
      this.authService.completeOnboarding({
        fitnessLevel: this.fitnessLevel(),
        goals: this.selectedGoals(),
        weightUnit: this.weightUnit(),
        distanceUnit: this.distanceUnit(),
      }).subscribe({
        next: () => this.router.navigateByUrl('/app/dashboard'),
        error: () => this.error.set('Failed to save preferences. Please try again.'),
      });
    }
  }
}
