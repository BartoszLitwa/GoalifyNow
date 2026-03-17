import { Page, expect } from '@playwright/test';
import { BasePage } from './base-page';
import { TestConfig } from '../config/test-config';

export class LoginPage extends BasePage {
  async goto() {
    await this.page.goto('/auth/login');
    await this.waitForPageReady();
  }

  async expectPageVisible() {
    await expect(this.page.getByText('Welcome back')).toBeVisible();
    await expect(this.page.getByRole('button', { name: 'Log In' })).toBeVisible();
  }

  async fillEmail(email: string) {
    await this.page.getByLabel('Email').fill(email);
  }

  async fillPassword(password: string) {
    await this.page.getByLabel('Password').fill(password);
  }

  async clickLogin() {
    await this.page.getByRole('button', { name: 'Log In' }).click();
  }

  async loginWithDemoUser() {
    await this.fillEmail(TestConfig.credentials.demo.email);
    await this.fillPassword(TestConfig.credentials.demo.password);
    await this.clickLogin();
  }

  async loginAs(email: string, password: string) {
    await this.fillEmail(email);
    await this.fillPassword(password);
    await this.clickLogin();
  }

  async expectError(message?: string) {
    if (message) {
      await expect(this.page.getByText(message)).toBeVisible();
    } else {
      await expect(this.page.locator('.text-red-600')).toBeVisible();
    }
  }

  async expectRegisterLink() {
    await expect(this.page.getByRole('link', { name: 'Sign up' })).toBeVisible();
  }
}
