import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';

export interface MealDto {
  id: string;
  mealType: string;
  date: string;
  totalCalories: number;
  totalProtein: number;
  totalCarbs: number;
  totalFat: number;
  items: MealItemDto[];
}

export interface MealItemDto {
  id: string;
  foodItemId: string;
  foodName: string;
  servingSize: number;
  calories: number;
  protein: number;
  carbs: number;
  fat: number;
}

export interface FoodDto {
  id: string;
  name: string;
  brand: string | null;
  caloriesPer100g: number;
  proteinPer100g: number;
  carbsPer100g: number;
  fatPer100g: number;
  servingSize: number;
}

export interface DailySummary {
  date: string;
  calories: number;
  protein: number;
  carbs: number;
  fat: number;
  waterMl: number;
  targetCalories: number;
  targetProtein: number;
  targetCarbs: number;
  targetFat: number;
}

export interface RecipeDto {
  id: string;
  name: string;
  servings: number;
  caloriesPerServing: number;
  proteinPerServing: number;
  carbsPerServing: number;
  fatPerServing: number;
}

@Injectable({ providedIn: 'root' })
export class NutritionApiService {
  private readonly api = `${environment.apiBaseUrl}/api`;

  constructor(private readonly http: HttpClient) {}

  getMeals(date?: string) {
    let params = new HttpParams();
    if (date) params = params.set('date', date);
    return this.http.get<MealDto[]>(`${this.api}/meals`, { params });
  }

  logMeal(data: { mealType: string; items?: any[] }) {
    return this.http.post<{ id: string }>(`${this.api}/meals`, data);
  }

  searchFoods(q: string) {
    return this.http.get<FoodDto[]>(`${this.api}/foods/search`, { params: { q } });
  }

  getDailySummary(date?: string) {
    let params = new HttpParams();
    if (date) params = params.set('date', date);
    return this.http.get<DailySummary>(`${this.api}/nutrition/daily`, { params });
  }

  logWater(amountMl: number) {
    return this.http.post<{ totalMl: number }>(`${this.api}/nutrition/water`, { amountMl });
  }

  setTargets(data: { calories: number; protein: number; carbs: number; fat: number }) {
    return this.http.put(`${this.api}/nutrition/targets`, data);
  }

  listRecipes() {
    return this.http.get<RecipeDto[]>(`${this.api}/recipes`);
  }

  createRecipe(data: any) {
    return this.http.post<{ id: string }>(`${this.api}/recipes`, data);
  }
}
