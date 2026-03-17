import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { NutritionApiService, DailySummary, MealDto, FoodDto } from '../../../core/nutrition-api.service';

@Component({
  selector: 'app-nutrition',
  imports: [DecimalPipe, FormsModule],
  template: `
    <div class="p-8">
      <div class="flex items-center justify-between mb-8">
        <div>
          <h1 class="text-3xl font-extrabold text-slate-900">Nutrition</h1>
          <p class="text-slate-500 mt-1">Track meals, macros, and hydration.</p>
        </div>
        <button (click)="showLogMeal.set(!showLogMeal())"
                class="px-5 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors flex items-center gap-2">
          <i class="pi pi-plus"></i> Log Meal
        </button>
      </div>

      @if (showLogMeal()) {
        <div class="bg-white rounded-xl border border-slate-200 p-6 mb-6">
          <h3 class="text-lg font-bold text-slate-900 mb-4">Search Food</h3>
          <input type="text" placeholder="Search foods..." [(ngModel)]="foodQuery" (ngModelChange)="onSearchFoods()"
                 class="w-full px-4 py-2.5 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none text-sm mb-4">
          <div class="grid md:grid-cols-2 gap-2 max-h-48 overflow-y-auto">
            @for (food of foundFoods(); track food.id) {
              <div class="flex items-center justify-between p-3 rounded-xl bg-slate-50 border border-slate-100 cursor-pointer hover:bg-indigo-50"
                   (click)="onQuickLog(food)">
                <div>
                  <div class="text-sm font-medium text-slate-900">{{food.name}}</div>
                  <div class="text-xs text-slate-400">{{food.caloriesPer100g}} cal / 100g &middot; P:{{food.proteinPer100g}}g</div>
                </div>
                <i class="pi pi-plus-circle text-indigo-500"></i>
              </div>
            }
          </div>
        </div>
      }

      @if (summary(); as s) {
        <div class="grid lg:grid-cols-4 gap-4 mb-8">
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="flex items-center justify-between mb-2">
              <span class="text-sm font-medium text-slate-500">Calories</span>
              <span class="text-sm font-bold text-indigo-600">{{s.calories | number:'1.0-0'}} / {{s.targetCalories | number:'1.0-0'}}</span>
            </div>
            <div class="w-full bg-slate-100 rounded-full h-2.5">
              <div class="h-2.5 rounded-full bg-indigo-500 transition-all" [style.width.%]="macPct(s.calories, s.targetCalories)"></div>
            </div>
          </div>
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="flex items-center justify-between mb-2">
              <span class="text-sm font-medium text-slate-500">Protein</span>
              <span class="text-sm font-bold text-red-600">{{s.protein | number:'1.0-0'}}g / {{s.targetProtein | number:'1.0-0'}}g</span>
            </div>
            <div class="w-full bg-slate-100 rounded-full h-2.5">
              <div class="h-2.5 rounded-full bg-red-500 transition-all" [style.width.%]="macPct(s.protein, s.targetProtein)"></div>
            </div>
          </div>
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="flex items-center justify-between mb-2">
              <span class="text-sm font-medium text-slate-500">Carbs</span>
              <span class="text-sm font-bold text-yellow-600">{{s.carbs | number:'1.0-0'}}g / {{s.targetCarbs | number:'1.0-0'}}g</span>
            </div>
            <div class="w-full bg-slate-100 rounded-full h-2.5">
              <div class="h-2.5 rounded-full bg-yellow-500 transition-all" [style.width.%]="macPct(s.carbs, s.targetCarbs)"></div>
            </div>
          </div>
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="flex items-center justify-between mb-2">
              <span class="text-sm font-medium text-slate-500">Fat</span>
              <span class="text-sm font-bold text-green-600">{{s.fat | number:'1.0-0'}}g / {{s.targetFat | number:'1.0-0'}}g</span>
            </div>
            <div class="w-full bg-slate-100 rounded-full h-2.5">
              <div class="h-2.5 rounded-full bg-green-500 transition-all" [style.width.%]="macPct(s.fat, s.targetFat)"></div>
            </div>
          </div>
        </div>

        <div class="grid lg:grid-cols-2 gap-6">
          <div>
            <h2 class="text-lg font-bold text-slate-900 mb-4">Today's Meals</h2>
            <div class="space-y-3">
              @for (meal of meals(); track meal.id) {
                <div class="bg-white rounded-xl border border-slate-200 p-5">
                  <div class="flex items-center justify-between mb-2">
                    <h3 class="font-bold text-slate-900">{{meal.mealType}}</h3>
                    <span class="text-sm text-slate-500">{{meal.totalCalories | number:'1.0-0'}} cal</span>
                  </div>
                  @for (item of meal.items; track item.id) {
                    <div class="text-sm text-slate-600">{{item.foodName}} - {{item.calories | number:'1.0-0'}} cal</div>
                  }
                </div>
              }
              @if (meals().length === 0) {
                <div class="text-center py-8 text-slate-400 text-sm">No meals logged today.</div>
              }
            </div>
          </div>
          <div>
            <h2 class="text-lg font-bold text-slate-900 mb-4">Hydration</h2>
            <div class="bg-white rounded-xl border border-slate-200 p-6 text-center">
              <div class="text-5xl font-extrabold text-blue-500 mb-2">{{(s.waterMl / 1000) | number:'1.1-1'}}L</div>
              <div class="text-sm text-slate-400 mb-4">of 2.0L daily target</div>
              <div class="w-full bg-slate-100 rounded-full h-4 mb-4">
                <div class="h-4 rounded-full bg-blue-500 transition-all" [style.width.%]="macPct(s.waterMl, 2000)"></div>
              </div>
              <button (click)="onLogWater(250)" class="px-6 py-2 bg-blue-500 text-white font-semibold rounded-xl hover:bg-blue-600 transition-colors">
                + 250ml
              </button>
            </div>
          </div>
        </div>
      }
    </div>
  `,
})
export class NutritionPage implements OnInit {
  private readonly api = inject(NutritionApiService);
  private readonly destroyRef = inject(DestroyRef);

  readonly summary = signal<DailySummary | null>(null);
  readonly meals = signal<MealDto[]>([]);
  readonly foundFoods = signal<FoodDto[]>([]);
  readonly showLogMeal = signal(false);
  foodQuery = '';

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.api.getDailySummary().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (s) => this.summary.set(s),
      error: () => this.summary.set(null),
    });
    this.api.getMeals().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (m) => this.meals.set(m),
      error: () => this.meals.set([]),
    });
  }

  macPct(current: number, target: number): number {
    if (target <= 0) return 0;
    return Math.min(100, (current / target) * 100);
  }

  onSearchFoods() {
    if (this.foodQuery.length < 2) return;
    this.api.searchFoods(this.foodQuery).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (f) => this.foundFoods.set(f),
      error: () => this.foundFoods.set([]),
    });
  }

  onQuickLog(food: FoodDto) {
    const serving = food.servingSize || 100;
    const ratio = serving / 100;
    this.api.logMeal({
      mealType: 'Snack',
      items: [{
        foodItemId: food.id,
        servingSize: serving,
        servingUnit: 'g',
        calories: food.caloriesPer100g * ratio,
        protein: food.proteinPer100g * ratio,
        carbs: food.carbsPer100g * ratio,
        fat: food.fatPer100g * ratio,
      }],
    }).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => this.loadData(),
      error: () => {},
    });
  }

  onLogWater(ml: number) {
    this.api.logWater(ml).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (res) => {
        const s = this.summary();
        if (s) this.summary.set({ ...s, waterMl: res.totalMl });
      },
      error: () => {},
    });
  }
}
