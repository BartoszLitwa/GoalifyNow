import { test, expect } from '@playwright/test';
import { LandingPage } from '../pages/landing-page';
import { LoginPage } from '../pages/login-page';
import { RegisterPage } from '../pages/register-page';
import { DashboardPage } from '../pages/dashboard-page';

test.describe('Smoke Tests @smoke', () => {
  test('landing page loads', async ({ page }) => {
    const landing = new LandingPage(page);
    await landing.goto();
    await landing.expectHeroVisible();
  });

  test('login page loads', async ({ page }) => {
    const login = new LoginPage(page);
    await login.goto();
    await login.expectPageVisible();
    await login.expectRegisterLink();
  });

  test('register page loads', async ({ page }) => {
    const register = new RegisterPage(page);
    await register.goto();
    await register.expectPageVisible();
    await register.expectLoginLink();
  });

  test('unauthenticated user is redirected to login', async ({ page }) => {
    await page.goto('/app/dashboard');
    await expect(page).toHaveURL(/auth\/login/);
  });

  test('health endpoint responds', async ({ request }) => {
    const apiUrl = process.env.API_URL || 'http://localhost:5090';
    const response = await request.get(`${apiUrl}/health`);
    expect(response.status()).toBe(200);
  });
});
