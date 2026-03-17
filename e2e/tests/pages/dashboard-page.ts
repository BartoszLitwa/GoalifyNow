import { expect } from '@playwright/test';
import { BasePage } from './base-page';

export class DashboardPage extends BasePage {
  async expectPageVisible() {
    await expect(this.page.getByText('Active Goals')).toBeVisible();
  }

  async expectStatCards() {
    await expect(this.page.getByText('Active Goals').first()).toBeVisible();
    await expect(this.page.getByText('Longest Streak')).toBeVisible();
    await expect(this.page.getByText('Workouts This Week')).toBeVisible();
    await expect(this.page.getByText('Calories Today')).toBeVisible();
  }

  async expectGoalsList() {
    await expect(this.page.locator('text=Active Goals').first()).toBeVisible();
  }

  async expectHabitsList() {
    await expect(this.page.getByText("Today's Habits")).toBeVisible();
  }
}
