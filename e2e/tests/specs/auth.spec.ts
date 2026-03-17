import { test, expect } from '@playwright/test';
import { LoginPage } from '../pages/login-page';
import { RegisterPage } from '../pages/register-page';
import { DashboardPage } from '../pages/dashboard-page';
import { TestConfig } from '../config/test-config';

test.describe('Authentication @critical', () => {
  test('demo user can log in successfully', async ({ page }) => {
    const login = new LoginPage(page);
    await login.goto();
    await login.loginWithDemoUser();

    await page.waitForURL('**/app/dashboard', { timeout: TestConfig.timeouts.navigation });
    const dashboard = new DashboardPage(page);
    await dashboard.expectPageVisible();
  });

  test('login fails with invalid credentials', async ({ page }) => {
    const login = new LoginPage(page);
    await login.goto();
    await login.loginAs('wrong@email.com', 'wrongpassword');

    await login.expectError();
    await expect(page).toHaveURL(/auth\/login/);
  });

  test('login fails with empty fields', async ({ page }) => {
    const login = new LoginPage(page);
    await login.goto();
    await login.clickLogin();

    await login.expectError();
  });

  test('register page navigates from login', async ({ page }) => {
    const login = new LoginPage(page);
    await login.goto();
    await page.getByRole('link', { name: 'Sign up' }).click();

    await expect(page).toHaveURL(/auth\/register/);
    const register = new RegisterPage(page);
    await register.expectPageVisible();
  });

  test('register new user succeeds', async ({ page }) => {
    const register = new RegisterPage(page);
    await register.goto();

    const uniqueEmail = `test-${Date.now()}@goalifynow.local`;
    await register.fill('Test User', uniqueEmail, 'TestPass123!');
    await register.clickRegister();

    await page.waitForURL('**/onboarding', { timeout: TestConfig.timeouts.navigation });
    await expect(page.getByText('What are your goals?')).toBeVisible();
  });

  test('register with existing email fails', async ({ page }) => {
    const register = new RegisterPage(page);
    await register.goto();
    await register.fill('Demo', TestConfig.credentials.demo.email, 'Demo123!');
    await register.clickRegister();

    await register.expectError();
  });
});
