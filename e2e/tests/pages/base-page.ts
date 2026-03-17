import { Page, expect } from '@playwright/test';

export abstract class BasePage {
  constructor(protected readonly page: Page) {}

  async waitForPageReady() {
    await this.page.waitForLoadState('networkidle');
  }

  async expectUrl(pattern: string | RegExp) {
    await expect(this.page).toHaveURL(pattern);
  }

  async expectVisible(text: string) {
    await expect(this.page.getByText(text, { exact: false }).first()).toBeVisible();
  }
}
