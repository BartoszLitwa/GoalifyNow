import { Page } from '@playwright/test';
import { TestConfig } from '../config/test-config';

export async function loginAsDemoUser(page: Page) {
  await page.goto('/auth/login');
  await page.getByLabel('Email').fill(TestConfig.credentials.demo.email);
  await page.getByLabel('Password').fill(TestConfig.credentials.demo.password);
  await page.getByRole('button', { name: 'Log In' }).click();
  await page.waitForURL('**/app/**', { timeout: TestConfig.timeouts.navigation });
}

export async function ensureAuthenticated(page: Page) {
  const currentUrl = page.url();
  if (!currentUrl.includes('/app/')) {
    await loginAsDemoUser(page);
  }
}
