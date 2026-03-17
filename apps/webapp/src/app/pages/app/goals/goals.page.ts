import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { GoalsApiService, GoalDto } from '../../../core/goals-api.service';

@Component({
  selector: 'app-goals',
  imports: [FormsModule],
  template: `
    <div class="p-8">
      <div class="flex items-center justify-between mb-8">
        <div>
          <h1 class="text-3xl font-extrabold text-slate-900">Goals</h1>
          <p class="text-slate-500 mt-1">Track progress toward your objectives.</p>
        </div>
        <button (click)="showCreate.set(!showCreate())"
                class="px-5 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors flex items-center gap-2">
          <i class="pi pi-plus"></i> New Goal
        </button>
      </div>

      @if (showCreate()) {
        <div class="bg-white rounded-xl border border-slate-200 p-6 mb-6">
          <h3 class="text-lg font-bold text-slate-900 mb-4">Create a Goal</h3>
          <div class="grid md:grid-cols-2 gap-4 mb-4">
            <input type="text" placeholder="Goal name" [(ngModel)]="newName"
                   class="px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none">
            <select [(ngModel)]="newCategory" class="px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none">
              <option value="Fitness">Fitness</option>
              <option value="Running">Running</option>
              <option value="Strength">Strength</option>
              <option value="Weight">Weight</option>
              <option value="Nutrition">Nutrition</option>
              <option value="Custom">Custom</option>
            </select>
            <input type="number" placeholder="Target value" [(ngModel)]="newTarget"
                   class="px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none">
            <input type="text" placeholder="Unit (kg, km, reps...)" [(ngModel)]="newUnit"
                   class="px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none">
          </div>
          <button (click)="onCreate()" class="px-6 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700">
            Create Goal
          </button>
        </div>
      }

      <div class="grid gap-4">
        @for (goal of goals(); track goal.id) {
          <div class="bg-white rounded-xl border border-slate-200 p-6">
            <div class="flex items-start justify-between mb-4">
              <div>
                <span class="text-xs font-medium px-2 py-1 rounded-full bg-indigo-50 text-indigo-700">
                  {{goal.category}}
                </span>
                <h3 class="text-lg font-bold text-slate-900 mt-2">{{goal.name}}</h3>
                @if (goal.deadline) {
                  <p class="text-sm text-slate-500">Due {{goal.deadline}}</p>
                }
              </div>
              <div class="text-right">
                <div class="text-2xl font-extrabold text-slate-900">{{ pct(goal) }}%</div>
                <div class="text-xs text-slate-400">{{goal.currentValue}} / {{goal.targetValue}} {{goal.metricUnit}}</div>
              </div>
            </div>
            <div class="w-full bg-slate-100 rounded-full h-3">
              <div class="h-3 rounded-full bg-indigo-500 transition-all" [style.width.%]="pct(goal)"></div>
            </div>
            @if (goal.milestones.length > 0) {
              <div class="mt-4 flex items-center gap-2 flex-wrap">
                @for (m of goal.milestones; track m.id) {
                  <span class="text-xs px-2 py-1 rounded-full border"
                        [class.bg-green-50]="m.isReached" [class.border-green-200]="m.isReached" [class.text-green-700]="m.isReached"
                        [class.border-slate-200]="!m.isReached" [class.text-slate-500]="!m.isReached">
                    @if (m.isReached) { <i class="pi pi-check mr-1" style="font-size: 0.6rem"></i> }
                    {{m.name}}
                  </span>
                }
              </div>
            }
          </div>
        }
        @if (goals().length === 0) {
          <div class="text-center py-12 text-slate-400">
            <i class="pi pi-flag text-4xl mb-3 block"></i>
            <p>No goals yet. Create your first one above.</p>
          </div>
        }
      </div>
    </div>
  `,
})
export class GoalsPage implements OnInit {
  private readonly api = inject(GoalsApiService);
  private readonly destroyRef = inject(DestroyRef);

  readonly goals = signal<GoalDto[]>([]);
  readonly showCreate = signal(false);

  newName = '';
  newCategory = 'Fitness';
  newTarget = 0;
  newUnit = '';

  ngOnInit() {
    this.loadGoals();
  }

  loadGoals() {
    this.api.listGoals().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (goals) => this.goals.set(goals),
      error: () => this.goals.set([]),
    });
  }

  pct(goal: GoalDto): number {
    if (goal.targetValue <= 0) return 0;
    return Math.min(100, Math.round((goal.currentValue / goal.targetValue) * 100));
  }

  onCreate() {
    if (!this.newName || !this.newTarget) return;
    this.api.createGoal({
      name: this.newName,
      category: this.newCategory,
      metricUnit: this.newUnit,
      targetValue: this.newTarget,
    }).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => {
        this.showCreate.set(false);
        this.newName = '';
        this.newTarget = 0;
        this.newUnit = '';
        this.loadGoals();
      },
      error: () => {},
    });
  }
}
