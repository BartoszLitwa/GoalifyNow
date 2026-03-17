import { expect } from '@playwright/test';
import { BasePage } from './base-page';

export class LandingPage extends BasePage {
  async goto() {
    await this.page.goto('/');
    await this.waitForPageReady();
  }

  async expectHeroVisible() {
    await expect(this.page.getByText('GoalifyNow').first()).toBeVisible();
  }

  async expectPricingSectionVisible() {
    await expect(this.page.getByText('Pricing', { exact: false }).first()).toBeVisible();
  }

  async clickGetStarted() {
    await this.page.getByRole('link', { name: /get started|sign up/i }).first().click();
  }

  async clickLogin() {
    await this.page.getByRole('link', { name: /log in|sign in/i }).first().click();
  }
}
