import { test, expect } from '@playwright/test';
import { loginAsDemoUser } from '../setup/auth-helper';

test.describe('Progress @regression', () => {
  test.beforeEach(async ({ page }) => {
    await loginAsDemoUser(page);
    await page.getByRole('link', { name: 'Progress' }).click();
  });

  test('progress page loads with weight data', async ({ page }) => {
    await expect(page.getByRole('heading', { level: 1, name: 'Progress' })).toBeVisible();
    await expect(page.getByText('Current Weight')).toBeVisible();
    await expect(page.getByText('Trend Weight')).toBeVisible();
    await expect(page.getByText('Entries')).toBeVisible();
  });

  test('weight chart visible', async ({ page }) => {
    await expect(page.getByText('Weight Trend')).toBeVisible();
  });

  test('progress photos section visible', async ({ page }) => {
    await expect(page.getByText('Progress Photos')).toBeVisible();
  });

  test('body measurements section visible', async ({ page }) => {
    await expect(page.getByText('Body Measurements')).toBeVisible();
    await expect(page.getByText('Waist')).toBeVisible();
    await expect(page.getByText('Chest')).toBeVisible();
  });

  test('log weight form opens', async ({ page }) => {
    await page.getByRole('button', { name: 'Log Weight' }).click();
    await expect(page.getByLabel(/Weight/)).toBeVisible();
  });

  test('log measurement form opens', async ({ page }) => {
    await page.getByText('+ Log Measurement').click();
    await expect(page.getByLabel(/Waist/)).toBeVisible();
    await expect(page.getByLabel(/Chest/)).toBeVisible();
    await expect(page.getByRole('button', { name: 'Save Measurement' })).toBeVisible();
  });
});
