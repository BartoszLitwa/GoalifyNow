import { test, expect } from '@playwright/test';
import { loginAsDemoUser } from '../setup/auth-helper';
import { DashboardPage } from '../pages/dashboard-page';
import { AppNav } from '../pages/app-nav';

test.describe('Dashboard @regression', () => {
  test.beforeEach(async ({ page }) => {
    await loginAsDemoUser(page);
  });

  test('dashboard displays stat cards', async ({ page }) => {
    const dashboard = new DashboardPage(page);
    await dashboard.expectStatCards();
  });

  test('dashboard shows active goals', async ({ page }) => {
    const dashboard = new DashboardPage(page);
    await dashboard.expectGoalsList();
  });

  test('dashboard shows today habits', async ({ page }) => {
    const dashboard = new DashboardPage(page);
    await dashboard.expectHabitsList();
  });

  test('sidebar navigation works', async ({ page }) => {
    const nav = new AppNav(page);
    await nav.expectSidebarVisible();

    for (const label of ['Goals', 'Habits', 'Workouts', 'Nutrition', 'Progress'] as const) {
      await nav.navigateTo(label);
      await expect(page.getByRole('heading', { level: 1, name: label })).toBeVisible();
    }
  });

  test('sidebar shows user info', async ({ page }) => {
    const nav = new AppNav(page);
    await nav.expectUserInfo();
  });
});
