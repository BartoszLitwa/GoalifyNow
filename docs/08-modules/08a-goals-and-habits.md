# Module 08a -- Goals & Habits

## Purpose

The Goals & Habits module is the backbone of GoalifyNow. Every other module feeds into it -- workouts, meals, runs, and measurements all contribute to goal progress. This module provides the framework for setting, tracking, and celebrating personal objectives.

---

## Feature List

### Goal Management

| Feature | Phase | Description |
|---------|-------|-------------|
| Goal creation wizard | MVP | Guided flow to define a goal with name, metric, target, and deadline |
| Goal templates | MVP | Pre-built templates for common goals (weight loss, strength milestones, running distances, nutrition targets) |
| Custom goals | MVP | Free-form goals with user-defined metrics and tracking methods |
| Goal categories | MVP | Organize goals into areas: Fitness, Nutrition, Personal, Learning, Financial |
| Goal editing | MVP | Modify goal targets, deadlines, and settings at any time |
| Goal archiving | MVP | Move completed or abandoned goals to archive with full history preserved |
| Goal deletion | MVP | Permanently remove a goal (with confirmation) |
| Goal duplication | MVP | Clone an existing goal as a starting point for a new one |
| Goal dependencies | Phase 2 | Link sequential goals (completing one unlocks/triggers the next) |
| Smart goal suggestions | Phase 3 | AI recommends goals based on user history and patterns |

### Milestone Tracking

| Feature | Phase | Description |
|---------|-------|-------------|
| Milestone markers | MVP | Set intermediate checkpoints within a goal (manual or auto-calculated) |
| Auto-calculated milestones | MVP | System generates evenly distributed milestones based on target and deadline |
| Milestone celebrations | MVP | Visual celebration (animation, badge) when a milestone is reached |
| Milestone reminders | MVP | Notification when a milestone deadline is approaching |

### Habit Tracking

| Feature | Phase | Description |
|---------|-------|-------------|
| Daily habit creation | MVP | Define a yes/no daily habit (e.g., "Meditate", "Take vitamins") |
| Frequency-based habits | MVP | Define habits with custom frequency (3x/week, every other day) |
| Streak tracking | MVP | Count consecutive successful completions |
| Grace days | MVP | Allow 1--2 missed days without breaking a streak |
| Streak recovery | MVP | Option to "protect" a streak using earned grace days |
| Habit calendar view | MVP | Monthly calendar showing completed/missed days per habit |
| Habit reminders | MVP | Configurable push/email reminders per habit |
| Habit statistics | MVP | Completion rate (%), longest streak, current streak, average per week |

### Recurring Goals

| Feature | Phase | Description |
|---------|-------|-------------|
| Daily recurring goals | MVP | Goals that reset daily (e.g., "Drink 2L water") |
| Weekly recurring goals | MVP | Goals that reset weekly (e.g., "Exercise 5 times") |
| Monthly recurring goals | MVP | Goals that reset monthly (e.g., "Run 100km") |
| Recurring goal history | MVP | View past cycles and completion rates |

### Progress Visualization

| Feature | Phase | Description |
|---------|-------|-------------|
| Progress bar | MVP | Visual percentage completion on each goal card |
| Goal dashboard | MVP | Overview of all active goals with status indicators |
| Trend chart | Phase 2 | Historical chart showing progress trajectory vs. ideal pace |
| Burndown/burnup chart | Phase 2 | Visual showing whether user is ahead or behind schedule |

---

## User Stories

### Goal Creation
- As a user, I want to create a weight loss goal with a target weight and deadline so I can track my progress.
- As a user, I want to select from pre-built goal templates so I don't have to figure out how to define my goal from scratch.
- As a user, I want to create a custom goal with my own metric (e.g., "Books read") so I can track anything important to me.
- As a user, I want to categorize my goals so I can see how balanced my life is across different areas.

### Goal Tracking
- As a user, I want my goal progress to update automatically when I log workouts, meals, or measurements so I don't have to manually update two things.
- As a user, I want to manually update goal progress for goals that aren't linked to other modules.
- As a user, I want to see my overall goal progress on a dashboard the moment I open the app.

### Milestones
- As a user, I want to set milestones within a goal so I have intermediate wins to celebrate.
- As a user, I want the system to suggest milestones based on my target and deadline so I don't have to calculate them.
- As a user, I want to receive a celebration notification when I hit a milestone so I feel rewarded.

### Habits
- As a user, I want to track daily habits with a simple yes/no check-in so logging is fast.
- As a user, I want my streaks to survive the occasional missed day so I don't feel punished for being human.
- As a user, I want to see a calendar view of my habit completions so I can spot patterns (e.g., weekends are weak).

### Social (Phase 2)
- As a tenant member, I want to see my partner's active goals so I know what they're working toward.
- As a tenant member, I want to create a shared goal that both of us contribute to.
- As a user, I want to choose which goals are visible to my tenant and which are private.

---

## Acceptance Criteria

### Goal Creation
- A goal must have a name and at least one of: target metric, deadline, or habit frequency
- Goal templates populate all fields, which the user can then modify
- Categories are predefined but users can add custom categories (Phase 2)
- A user can have a maximum of 3 active goals on the Free tier, unlimited on Premium

### Streak Tracking
- A streak increments when the habit is marked complete for the expected day
- A missed day consumes a grace day (if available); the streak continues
- If no grace days remain and a day is missed, the streak resets to 0
- Grace days replenish at a rate of 1 per 7-day streak maintained
- Streak count, longest streak, and current grace days are visible on the habit card

### Goal Progress
- Goals linked to modules (workouts, meals, runs) auto-update within 5 seconds of the source log being saved
- Manual goal updates are timestamped and reversible (edit history preserved)
- Progress percentage is calculated as (current value / target value) x 100, capped at 100%
- Completed goals trigger a celebration and move to "Completed" section (not auto-archived)

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Goal** | A user-defined objective with a name, category, metric, target value, deadline, and progress state |
| **Milestone** | An intermediate checkpoint within a goal with its own target value and optional deadline |
| **Habit** | A recurring action tracked on a daily or weekly frequency with streak logic |
| **Streak** | A consecutive count of successful habit completions, with grace day support |
| **Goal Template** | A pre-configured goal definition that users can instantiate and customize |
| **Goal Category** | A label for organizing goals (Fitness, Nutrition, Personal, Learning, Financial, Custom) |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Training & Workouts** | Workout logs contribute to fitness goals (e.g., "Exercise 5x/week" auto-increments when a workout is logged) |
| **Running & Cardio** | Run logs contribute to distance goals (e.g., "Run 100km this month") |
| **Diet & Nutrition** | Meal logs contribute to nutrition goals (e.g., "Eat 150g protein daily") |
| **Progress & Analytics** | Weight/measurement entries contribute to body composition goals |
| **Social & Accountability** | Goals can be shared with tenant members; shared goals aggregate contributions from multiple users |
| **Preparation & Planning** | Event goals link to preparation timelines |
