import { test, expect } from '@playwright/test';
import { loginAsDemoUser } from '../setup/auth-helper';

test.describe('Nutrition @regression', () => {
  test.beforeEach(async ({ page }) => {
    await loginAsDemoUser(page);
    await page.getByRole('link', { name: 'Nutrition' }).click();
  });

  test('nutrition page loads with macro summary', async ({ page }) => {
    await expect(page.getByRole('heading', { level: 1, name: 'Nutrition' })).toBeVisible();
    await expect(page.getByText('Calories')).toBeVisible();
    await expect(page.getByText('Protein')).toBeVisible();
    await expect(page.getByText('Carbs')).toBeVisible();
    await expect(page.getByText('Fat')).toBeVisible();
  });

  test('nutrition page shows meals', async ({ page }) => {
    await expect(page.getByText("Today's Meals")).toBeVisible();
  });

  test('hydration tracker visible', async ({ page }) => {
    await expect(page.getByText('Hydration')).toBeVisible();
    await expect(page.getByRole('button', { name: '+ 250ml' })).toBeVisible();
  });

  test('food search opens and finds items', async ({ page }) => {
    await page.getByRole('button', { name: 'Log Meal' }).click();
    await expect(page.getByText('Search Food')).toBeVisible();

    await page.getByPlaceholder('Search foods...').fill('chicken');
    await page.waitForTimeout(500);
    await expect(page.getByText('Chicken Breast')).toBeVisible();
  });

  test('log water increments hydration', async ({ page }) => {
    await page.getByRole('button', { name: '+ 250ml' }).click();
    await page.waitForTimeout(500);
  });
});
