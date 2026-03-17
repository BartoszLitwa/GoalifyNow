import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ProgressApiService, DashboardData } from '../../../core/progress-api.service';
import { GoalsApiService, GoalDto, HabitDto } from '../../../core/goals-api.service';

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink],
  template: `
    <div class="p-8">
      <div class="mb-8">
        <h1 class="text-3xl font-extrabold text-slate-900">{{ greeting() }}</h1>
        <p class="text-slate-500 mt-1">Here's your day at a glance.</p>
      </div>

      <div class="grid md:grid-cols-2 lg:grid-cols-4 gap-4 mb-8">
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="flex items-center justify-between mb-3">
            <span class="text-sm font-medium text-slate-500">Active Goals</span>
            <div class="w-8 h-8 rounded-lg bg-indigo-50 flex items-center justify-center">
              <i class="pi pi-flag text-indigo-600" style="font-size: 0.85rem"></i>
            </div>
          </div>
          <div class="text-2xl font-bold text-slate-900">{{dashboard()?.activeGoals ?? 0}}</div>
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="flex items-center justify-between mb-3">
            <span class="text-sm font-medium text-slate-500">Longest Streak</span>
            <div class="w-8 h-8 rounded-lg bg-orange-50 flex items-center justify-center">
              <i class="pi pi-bolt text-orange-500" style="font-size: 0.85rem"></i>
            </div>
          </div>
          <div class="text-2xl font-bold text-slate-900">{{dashboard()?.longestHabitStreak ?? 0}} days</div>
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="flex items-center justify-between mb-3">
            <span class="text-sm font-medium text-slate-500">Workouts This Week</span>
            <div class="w-8 h-8 rounded-lg bg-red-50 flex items-center justify-center">
              <i class="pi pi-heart text-red-500" style="font-size: 0.85rem"></i>
            </div>
          </div>
          <div class="text-2xl font-bold text-slate-900">{{dashboard()?.weekWorkouts ?? 0}}</div>
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="flex items-center justify-between mb-3">
            <span class="text-sm font-medium text-slate-500">Calories Today</span>
            <div class="w-8 h-8 rounded-lg bg-green-50 flex items-center justify-center">
              <i class="pi pi-apple text-green-600" style="font-size: 0.85rem"></i>
            </div>
          </div>
          <div class="text-2xl font-bold text-slate-900">{{dashboard()?.todayCalories ?? 0}}</div>
        </div>
      </div>

      <div class="grid lg:grid-cols-2 gap-6">
        <div class="bg-white rounded-xl border border-slate-200 p-6">
          <h2 class="text-lg font-bold text-slate-900 mb-4">Active Goals</h2>
          <div class="space-y-4">
            @for (goal of goals(); track goal.id) {
              <div>
                <div class="flex items-center justify-between mb-1">
                  <span class="text-sm font-medium text-slate-900">{{goal.name}}</span>
                  <span class="text-sm text-slate-500">{{pct(goal)}}%</span>
                </div>
                <div class="w-full bg-slate-100 rounded-full h-2">
                  <div class="h-2 rounded-full bg-indigo-500 transition-all" [style.width.%]="pct(goal)"></div>
                </div>
              </div>
            }
            @if (goals().length === 0) {
              <p class="text-sm text-slate-400">No active goals.</p>
            }
          </div>
          <a routerLink="/app/goals" class="inline-block mt-4 text-sm font-medium text-indigo-600 hover:text-indigo-700">View all goals &rarr;</a>
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-6">
          <h2 class="text-lg font-bold text-slate-900 mb-4">Today's Habits</h2>
          <div class="space-y-3">
            @for (habit of habits(); track habit.id) {
              <div class="flex items-center justify-between p-3 rounded-xl border border-slate-100"
                   [class.bg-green-50]="habit.todayDone" [class.border-green-200]="habit.todayDone">
                <div class="flex items-center gap-3">
                  <div class="w-8 h-8 rounded-lg flex items-center justify-center"
                       [class.bg-green-100]="habit.todayDone" [class.text-green-600]="habit.todayDone"
                       [class.bg-slate-100]="!habit.todayDone" [class.text-slate-400]="!habit.todayDone">
                    <i class="pi" [class.pi-check]="habit.todayDone" [class.pi-circle]="!habit.todayDone"></i>
                  </div>
                  <span class="text-sm font-medium text-slate-900">{{habit.name}}</span>
                </div>
                <span class="text-xs font-medium px-2 py-1 rounded-full"
                      [class.bg-orange-100]="habit.currentStreak > 0" [class.text-orange-700]="habit.currentStreak > 0"
                      [class.bg-slate-100]="habit.currentStreak === 0" [class.text-slate-500]="habit.currentStreak === 0">
                  {{habit.currentStreak}} day streak
                </span>
              </div>
            }
            @if (habits().length === 0) {
              <p class="text-sm text-slate-400">No habits yet.</p>
            }
          </div>
          <a routerLink="/app/habits" class="inline-block mt-4 text-sm font-medium text-indigo-600 hover:text-indigo-700">View all habits &rarr;</a>
        </div>
      </div>
    </div>
  `,
})
export class DashboardPage implements OnInit {
  private readonly progressApi = inject(ProgressApiService);
  private readonly goalsApi = inject(GoalsApiService);
  private readonly destroyRef = inject(DestroyRef);

  readonly dashboard = signal<DashboardData | null>(null);
  readonly goals = signal<GoalDto[]>([]);
  readonly habits = signal<HabitDto[]>([]);
  readonly greeting = signal('Good morning!');

  ngOnInit() {
    const hour = new Date().getHours();
    if (hour < 12) this.greeting.set('Good morning!');
    else if (hour < 18) this.greeting.set('Good afternoon!');
    else this.greeting.set('Good evening!');

    this.progressApi.getDashboard().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (d) => this.dashboard.set(d),
      error: () => this.dashboard.set(null),
    });
    this.goalsApi.listGoals('Active').pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (g) => this.goals.set(g),
      error: () => this.goals.set([]),
    });
    this.goalsApi.listHabits().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (habits) => this.habits.set(habits),
      error: () => this.habits.set([]),
    });
  }

  pct(goal: GoalDto): number {
    if (goal.targetValue <= 0) return 0;
    return Math.min(100, Math.round((goal.currentValue / goal.targetValue) * 100));
  }
}
