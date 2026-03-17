import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { DatePipe, DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { WorkoutsApiService, SessionDto, ExerciseItem } from '../../../core/workouts-api.service';

@Component({
  selector: 'app-workouts',
  imports: [FormsModule, DatePipe, DecimalPipe],
  template: `
    <div class="p-8">
      <div class="flex items-center justify-between mb-8">
        <div>
          <h1 class="text-3xl font-extrabold text-slate-900">Workouts</h1>
          <p class="text-slate-500 mt-1">Log sessions, track progressive overload, and hit PRs.</p>
        </div>
        <div class="flex gap-3">
          <button (click)="showExercises.set(!showExercises())"
                  class="px-5 py-2.5 bg-white border border-slate-200 text-slate-700 font-semibold rounded-xl hover:bg-slate-50 transition-colors flex items-center gap-2">
            <i class="pi pi-list"></i> Exercises
          </button>
          <button (click)="onStartWorkout()"
                  class="px-5 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors flex items-center gap-2">
            <i class="pi pi-play"></i> Start Workout
          </button>
        </div>
      </div>

      @if (showExercises()) {
        <div class="bg-white rounded-xl border border-slate-200 p-6 mb-6">
          <h3 class="text-lg font-bold text-slate-900 mb-4">Exercise Library</h3>
          <div class="flex gap-3 mb-4">
            <input type="text" placeholder="Search exercises..." [(ngModel)]="searchQuery" (ngModelChange)="onSearch()"
                   class="flex-1 px-4 py-2.5 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none text-sm">
            <select [(ngModel)]="muscleFilter" (ngModelChange)="onSearch()" class="px-4 py-2.5 rounded-xl border border-slate-300 text-sm">
              <option value="">All muscles</option>
              @for (m of muscleGroups; track m) {
                <option [value]="m">{{m}}</option>
              }
            </select>
          </div>
          <div class="grid md:grid-cols-2 gap-2 max-h-64 overflow-y-auto">
            @for (ex of exercises(); track ex.id) {
              <div class="flex items-center justify-between p-3 rounded-xl bg-slate-50 border border-slate-100">
                <div>
                  <div class="text-sm font-medium text-slate-900">{{ex.name}}</div>
                  <div class="text-xs text-slate-400">{{ex.primaryMuscle}} &middot; {{ex.equipment}}</div>
                </div>
              </div>
            }
          </div>
        </div>
      }

      <div class="grid lg:grid-cols-3 gap-4 mb-8">
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="text-sm font-medium text-slate-500 mb-1">This Week</div>
          <div class="text-2xl font-bold text-slate-900">{{sessions().length}} sessions</div>
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="text-sm font-medium text-slate-500 mb-1">Total Volume</div>
          <div class="text-2xl font-bold text-slate-900">{{totalVolume() | number:'1.0-0'}} kg</div>
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-5">
          <div class="text-sm font-medium text-slate-500 mb-1">Total Sets</div>
          <div class="text-2xl font-bold text-slate-900">{{totalSets()}}</div>
        </div>
      </div>

      <h2 class="text-lg font-bold text-slate-900 mb-4">Recent Sessions</h2>
      <div class="grid gap-4">
        @for (session of sessions(); track session.id) {
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="flex items-center justify-between mb-3">
              <div>
                <h3 class="text-base font-bold text-slate-900">{{session.name}}</h3>
                <p class="text-xs text-slate-400">{{session.startedAt | date:'mediumDate'}} &middot; {{session.durationMinutes}} min</p>
              </div>
            </div>
            <div class="flex flex-wrap gap-2 mb-2">
              @for (muscle of session.muscles; track muscle) {
                <span class="text-xs px-2 py-1 rounded-full bg-slate-100 text-slate-600">{{muscle}}</span>
              }
            </div>
            <div class="flex items-center gap-4 text-xs text-slate-400">
              <span>{{session.exerciseCount}} exercises</span>
              <span>{{session.setCount}} sets</span>
              <span>{{session.totalVolume | number:'1.0-0'}} kg volume</span>
            </div>
          </div>
        }
        @if (sessions().length === 0) {
          <div class="text-center py-12 text-slate-400">
            <i class="pi pi-heart text-4xl mb-3 block"></i>
            <p>No workouts yet. Start your first session above.</p>
          </div>
        }
      </div>
    </div>
  `,
})
export class WorkoutsPage implements OnInit {
  private readonly api = inject(WorkoutsApiService);
  private readonly destroyRef = inject(DestroyRef);

  readonly sessions = signal<SessionDto[]>([]);
  readonly exercises = signal<ExerciseItem[]>([]);
  readonly showExercises = signal(false);

  searchQuery = '';
  muscleFilter = '';

  readonly muscleGroups = ['Chest', 'Back', 'Shoulders', 'Biceps', 'Triceps', 'Quads', 'Hamstrings', 'Glutes', 'Calves', 'Core'];

  totalVolume = signal(0);
  totalSets = signal(0);

  ngOnInit() {
    this.api.listSessions().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (s) => {
        this.sessions.set(s);
        this.totalVolume.set(s.reduce((sum, sess) => sum + sess.totalVolume, 0));
        this.totalSets.set(s.reduce((sum, sess) => sum + sess.setCount, 0));
      },
      error: () => {
        this.sessions.set([]);
        this.totalVolume.set(0);
        this.totalSets.set(0);
      },
    });
  }

  onSearch() {
    this.api.searchExercises(this.searchQuery, this.muscleFilter).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (e) => this.exercises.set(e),
      error: () => this.exercises.set([]),
    });
  }

  onStartWorkout() {
    const name = `Workout ${new Date().toLocaleDateString()}`;
    this.api.startSession(name).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => {
        this.api.listSessions().pipe(
          takeUntilDestroyed(this.destroyRef)
        ).subscribe({
          next: (s) => this.sessions.set(s),
          error: () => {},
        });
      },
      error: () => {},
    });
  }
}
