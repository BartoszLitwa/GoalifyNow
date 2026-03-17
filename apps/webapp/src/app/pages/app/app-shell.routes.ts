import { Routes } from '@angular/router';

import { AppShellLayout } from './app-shell.layout';

export const APP_SHELL_ROUTES: Routes = [
  {
    path: '',
    component: AppShellLayout,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', loadComponent: () => import('./dashboard/dashboard.page').then(m => m.DashboardPage) },
      { path: 'goals', loadComponent: () => import('./goals/goals.page').then(m => m.GoalsPage) },
      { path: 'habits', loadComponent: () => import('./habits/habits.page').then(m => m.HabitsPage) },
      { path: 'workouts', loadComponent: () => import('./workouts/workouts.page').then(m => m.WorkoutsPage) },
      { path: 'nutrition', loadComponent: () => import('./nutrition/nutrition.page').then(m => m.NutritionPage) },
      { path: 'progress', loadComponent: () => import('./progress/progress.page').then(m => m.ProgressPage) },
    ],
  },
];
