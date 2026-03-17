import { Page, expect } from '@playwright/test';

export class AppNav {
  constructor(private readonly page: Page) {}

  async expectSidebarVisible() {
    await expect(this.page.getByText('GoalifyNow').first()).toBeVisible();
  }

  async navigateTo(label: 'Dashboard' | 'Goals' | 'Habits' | 'Workouts' | 'Nutrition' | 'Progress') {
    await this.page.getByRole('link', { name: label }).click();
    await this.page.waitForLoadState('networkidle');
  }

  async expectNavItem(label: string) {
    await expect(this.page.getByRole('link', { name: label })).toBeVisible();
  }

  async expectUserInfo() {
    const sidebar = this.page.locator('aside');
    await expect(sidebar).toBeVisible();
  }

  async logout() {
    await this.page.getByRole('button', { name: /log\s?out|sign\s?out/i }).click();
  }
}
