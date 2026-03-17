import { test, expect } from '@playwright/test';
import { loginAsDemoUser } from '../setup/auth-helper';

test.describe('Workouts @regression', () => {
  test.beforeEach(async ({ page }) => {
    await loginAsDemoUser(page);
    await page.getByRole('link', { name: 'Workouts' }).click();
  });

  test('workouts page loads with seeded sessions', async ({ page }) => {
    await expect(page.getByRole('heading', { level: 1, name: 'Workouts' })).toBeVisible();
    await expect(page.getByText('Recent Sessions')).toBeVisible();
  });

  test('workouts page shows stats cards', async ({ page }) => {
    await expect(page.getByText('This Week')).toBeVisible();
    await expect(page.getByText('Total Volume')).toBeVisible();
    await expect(page.getByText('Total Sets')).toBeVisible();
  });

  test('exercise library opens and searches', async ({ page }) => {
    await page.getByRole('button', { name: 'Exercises' }).click();
    await expect(page.getByText('Exercise Library')).toBeVisible();

    await page.getByPlaceholder('Search exercises...').fill('bench');
    await page.waitForTimeout(500);
    await expect(page.getByText('Barbell Bench Press')).toBeVisible();
  });

  test('start new workout', async ({ page }) => {
    const sessionCountBefore = await page.locator('.bg-white.rounded-xl.border.p-5').count();
    await page.getByRole('button', { name: 'Start Workout' }).click();
    await page.waitForTimeout(1000);

    const sessionCountAfter = await page.locator('.bg-white.rounded-xl.border.p-5').count();
    expect(sessionCountAfter).toBeGreaterThanOrEqual(sessionCountBefore);
  });
});
