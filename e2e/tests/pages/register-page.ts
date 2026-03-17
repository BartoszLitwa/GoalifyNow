import { expect } from '@playwright/test';
import { BasePage } from './base-page';

export class RegisterPage extends BasePage {
  async goto() {
    await this.page.goto('/auth/register');
    await this.waitForPageReady();
  }

  async expectPageVisible() {
    await expect(this.page.getByText('Create your free account')).toBeVisible();
    await expect(this.page.getByRole('button', { name: 'Create Account' })).toBeVisible();
  }

  async fill(displayName: string, email: string, password: string) {
    await this.page.getByLabel('Display Name').fill(displayName);
    await this.page.getByLabel('Email').fill(email);
    await this.page.getByLabel('Password').fill(password);
  }

  async clickRegister() {
    await this.page.getByRole('button', { name: 'Create Account' }).click();
  }

  async expectError() {
    await expect(this.page.locator('.text-red-600')).toBeVisible();
  }

  async expectLoginLink() {
    await expect(this.page.getByRole('link', { name: 'Log in' })).toBeVisible();
  }
}
