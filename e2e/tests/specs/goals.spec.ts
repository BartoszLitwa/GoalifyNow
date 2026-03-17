import { test, expect } from '@playwright/test';
import { loginAsDemoUser } from '../setup/auth-helper';

test.describe('Goals & Habits @regression', () => {
  test.beforeEach(async ({ page }) => {
    await loginAsDemoUser(page);
  });

  test('goals page shows seeded goals', async ({ page }) => {
    await page.getByRole('link', { name: 'Goals' }).click();
    await expect(page.getByRole('heading', { level: 1, name: 'Goals' })).toBeVisible();
    await expect(page.getByText('Lose 10kg by June')).toBeVisible();
    await expect(page.getByText('Run 100km this month')).toBeVisible();
    await expect(page.getByText('Hit 100kg bench press')).toBeVisible();
  });

  test('create new goal', async ({ page }) => {
    await page.getByRole('link', { name: 'Goals' }).click();
    await page.getByRole('button', { name: 'New Goal' }).click();

    await page.getByPlaceholder('Goal name').fill('Run a marathon');
    await page.getByPlaceholder('Target value').fill('42');
    await page.getByPlaceholder(/Unit/).fill('km');
    await page.getByRole('button', { name: 'Create Goal' }).click();

    await expect(page.getByText('Run a marathon')).toBeVisible();
  });

  test('goals show progress bars', async ({ page }) => {
    await page.getByRole('link', { name: 'Goals' }).click();
    const progressBars = page.locator('.bg-indigo-500.rounded-full');
    await expect(progressBars.first()).toBeVisible();
  });

  test('goals show milestones', async ({ page }) => {
    await page.getByRole('link', { name: 'Goals' }).click();
    await expect(page.getByText('3kg lost')).toBeVisible();
    await expect(page.getByText('5kg lost')).toBeVisible();
  });

  test('habits page shows seeded habits', async ({ page }) => {
    await page.getByRole('link', { name: 'Habits' }).click();
    await expect(page.getByRole('heading', { level: 1, name: 'Habits' })).toBeVisible();
    await expect(page.getByText('Morning workout')).toBeVisible();
    await expect(page.getByText('Log all meals')).toBeVisible();
    await expect(page.getByText('Drink 2L water')).toBeVisible();
  });

  test('create new habit', async ({ page }) => {
    await page.getByRole('link', { name: 'Habits' }).click();
    await page.getByRole('button', { name: 'New Habit' }).click();

    await page.getByPlaceholder('Habit name').fill('Evening stretch');
    await page.getByRole('button', { name: 'Create Habit' }).click();

    await expect(page.getByText('Evening stretch')).toBeVisible();
  });

  test('habits show streak information', async ({ page }) => {
    await page.getByRole('link', { name: 'Habits' }).click();
    await expect(page.getByText('streak').first()).toBeVisible();
    await expect(page.getByText('rate').first()).toBeVisible();
  });
});
