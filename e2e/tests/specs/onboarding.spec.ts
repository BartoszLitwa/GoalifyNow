import { test, expect } from '@playwright/test';
import { RegisterPage } from '../pages/register-page';
import { TestConfig } from '../config/test-config';

test.describe('Onboarding @critical', () => {
  test('complete onboarding flow', async ({ page }) => {
    const register = new RegisterPage(page);
    await register.goto();

    const uniqueEmail = `onboard-${Date.now()}@goalifynow.local`;
    await register.fill('Onboard User', uniqueEmail, 'TestPass123!');
    await register.clickRegister();

    await page.waitForURL('**/onboarding', { timeout: TestConfig.timeouts.navigation });

    await expect(page.getByText('What are your goals?')).toBeVisible();
    await page.getByText('Lose weight').click();
    await page.getByText('Build muscle').click();
    await page.getByRole('button', { name: 'Continue' }).click();

    await expect(page.getByText('Your fitness level')).toBeVisible();
    await page.getByText('Intermediate').click();
    await page.getByRole('button', { name: 'Continue' }).click();

    await expect(page.getByText('Your preferences')).toBeVisible();
    await page.getByRole('button', { name: 'Get Started' }).click();

    await page.waitForURL('**/app/dashboard', { timeout: TestConfig.timeouts.navigation });
  });

  test('step navigation works', async ({ page }) => {
    const register = new RegisterPage(page);
    await register.goto();

    const uniqueEmail = `onboard-nav-${Date.now()}@goalifynow.local`;
    await register.fill('Nav User', uniqueEmail, 'TestPass123!');
    await register.clickRegister();

    await page.waitForURL('**/onboarding', { timeout: TestConfig.timeouts.navigation });

    await page.getByRole('button', { name: 'Continue' }).click();

    await expect(page.getByText('Your fitness level')).toBeVisible();
    await page.getByText('Back').click();

    await expect(page.getByText('What are your goals?')).toBeVisible();
  });
});
