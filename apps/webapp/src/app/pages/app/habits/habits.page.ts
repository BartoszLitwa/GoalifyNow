import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { GoalsApiService, HabitDto } from '../../../core/goals-api.service';

@Component({
  selector: 'app-habits',
  imports: [FormsModule],
  template: `
    <div class="p-8">
      <div class="flex items-center justify-between mb-8">
        <div>
          <h1 class="text-3xl font-extrabold text-slate-900">Habits</h1>
          <p class="text-slate-500 mt-1">Build consistency with daily and weekly habits.</p>
        </div>
        <button (click)="showCreate.set(!showCreate())"
                class="px-5 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors flex items-center gap-2">
          <i class="pi pi-plus"></i> New Habit
        </button>
      </div>

      @if (showCreate()) {
        <div class="bg-white rounded-xl border border-slate-200 p-6 mb-6">
          <div class="flex gap-4 mb-4">
            <input type="text" placeholder="Habit name" [(ngModel)]="newName"
                   class="flex-1 px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none">
            <select [(ngModel)]="newFreq" class="px-4 py-3 rounded-xl border border-slate-300">
              <option value="Daily">Daily</option>
              <option value="Weekly">Weekly</option>
            </select>
          </div>
          <button (click)="onCreateHabit()" class="px-6 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700">
            Create Habit
          </button>
        </div>
      }

      <div class="grid gap-4">
        @for (habit of habits(); track habit.id) {
          <div class="bg-white rounded-xl border border-slate-200 p-5 flex items-center justify-between">
            <div class="flex items-center gap-4">
              <button (click)="onCheckIn(habit)"
                      class="w-10 h-10 rounded-xl border-2 flex items-center justify-center transition-all"
                      [class.bg-green-500]="habit.todayDone"
                      [class.border-green-500]="habit.todayDone"
                      [class.text-white]="habit.todayDone"
                      [class.border-slate-300]="!habit.todayDone"
                      [class.text-slate-300]="!habit.todayDone">
                <i class="pi pi-check"></i>
              </button>
              <div>
                <h3 class="text-base font-bold text-slate-900">{{habit.name}}</h3>
                <p class="text-xs text-slate-400">{{habit.frequency}}</p>
              </div>
            </div>
            <div class="flex items-center gap-6">
              <div class="text-center">
                <div class="text-xl font-extrabold" [class.text-orange-500]="habit.currentStreak > 0" [class.text-slate-300]="habit.currentStreak === 0">
                  {{habit.currentStreak}}
                </div>
                <div class="text-xs text-slate-400">streak</div>
              </div>
              <div class="text-center">
                <div class="text-xl font-extrabold text-slate-900">{{habit.completionRate}}%</div>
                <div class="text-xs text-slate-400">rate</div>
              </div>
              <div class="flex gap-1">
                @for (day of habit.lastWeek; track $index) {
                  <div class="w-5 h-5 rounded"
                       [class.bg-green-400]="day" [class.bg-slate-100]="!day"></div>
                }
              </div>
            </div>
          </div>
        }
        @if (habits().length === 0) {
          <div class="text-center py-12 text-slate-400">
            <i class="pi pi-bolt text-4xl mb-3 block"></i>
            <p>No habits yet. Create your first one above.</p>
          </div>
        }
      </div>
    </div>
  `,
})
export class HabitsPage implements OnInit {
  private readonly api = inject(GoalsApiService);
  private readonly destroyRef = inject(DestroyRef);

  readonly habits = signal<HabitDto[]>([]);
  readonly showCreate = signal(false);

  newName = '';
  newFreq = 'Daily';

  ngOnInit() {
    this.loadHabits();
  }

  loadHabits() {
    this.api.listHabits().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (h) => this.habits.set(h),
      error: () => this.habits.set([]),
    });
  }

  onCheckIn(habit: HabitDto) {
    this.api.checkIn(habit.id).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (res) => {
        const updated = this.habits().map(h =>
          h.id === habit.id ? { ...h, todayDone: res.completed, currentStreak: res.currentStreak, longestStreak: res.longestStreak } : h
        );
        this.habits.set(updated);
      },
      error: () => {},
    });
  }

  onCreateHabit() {
    if (!this.newName) return;
    this.api.createHabit({ name: this.newName, frequency: this.newFreq }).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => {
        this.showCreate.set(false);
        this.newName = '';
        this.loadHabits();
      },
      error: () => {},
    });
  }
}
