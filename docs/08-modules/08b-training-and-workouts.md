# Module 08b -- Training & Workouts

## Purpose

The Training & Workouts module provides a complete workout diary and programming system. Users can log individual sessions, follow structured multi-week programs, track progressive overload, and analyze their training history. This module replaces dedicated workout apps like Strong, Hevy, and Stronger.

---

## Feature List

### Workout Logging

| Feature | Phase | Description |
|---------|-------|-------------|
| Quick-start session | MVP | Begin logging a workout immediately with no template |
| Template-based session | MVP | Start a workout from a saved template |
| Program-based session | MVP | Start the next scheduled session from an active program |
| Set logging | MVP | Log sets with weight, reps, duration, distance, or RPE depending on exercise type |
| Set types | MVP | Tag sets as warm-up, working, drop set, or failure |
| Rest timer | MVP | Configurable countdown timer between sets with notification |
| Superset support | MVP | Group 2+ exercises into a superset with shared rest timer |
| Circuit support | MVP | Group exercises into a circuit with round tracking |
| Session notes | MVP | Free-text notes for the overall session |
| Exercise notes | MVP | Free-text notes per exercise within a session |
| Session duration | MVP | Auto-tracked from start to finish with option to edit |
| Previous performance reference | MVP | Display last session's sets/reps/weight for each exercise inline |

### Exercise Library

| Feature | Phase | Description |
|---------|-------|-------------|
| Built-in exercise library | MVP | 500+ exercises with name, primary/secondary muscle groups, equipment type, and category |
| Exercise search | MVP | Search by name, muscle group, or equipment |
| Exercise filter | MVP | Filter by muscle group, equipment available, exercise type |
| Custom exercises | MVP | Create personal exercises with custom name, muscle tags, and equipment |
| Exercise thumbnails | MVP | Static images showing the exercise movement |
| Exercise history | MVP | View all past performances of a specific exercise (sets, reps, weight over time) |
| Exercise video demos | Phase 2 | Short video clips demonstrating proper form |
| Exercise alternatives | Phase 2 | Suggest substitute exercises for the same muscle group |

### Workout Templates

| Feature | Phase | Description |
|---------|-------|-------------|
| Save session as template | MVP | Save any completed session as a reusable template |
| Template management | MVP | Create, edit, duplicate, delete, and organize templates |
| Template folders | MVP | Organize templates into folders (e.g., "Push", "Pull", "Legs") |
| Template sharing | Phase 2 | Share templates with tenant members |
| Free tier limit | MVP | Free tier: 3 saved templates; Premium: unlimited |

### Workout Programs

| Feature | Phase | Description |
|---------|-------|-------------|
| Program builder | MVP | Create a multi-week program with scheduled sessions per day |
| Program assignment | MVP | Assign a program to yourself and follow the schedule |
| Program progress | MVP | Track which sessions are completed and which are upcoming |
| Program duplication | MVP | Clone a program to modify for a new training block |
| Rest day scheduling | MVP | Mark rest days within a program |
| Deload week support | Phase 2 | Built-in deload week suggestions based on training volume |
| Coach program assignment | Phase 2 | Pro tier: assign programs to clients in a coach tenant |
| Program marketplace | Phase 3 | Browse and purchase community-created programs |

### Analytics & Tracking

| Feature | Phase | Description |
|---------|-------|-------------|
| Progressive overload tracking | MVP | Charts showing weight/rep progression per exercise over time |
| Volume tracking | MVP | Total volume (sets x reps x weight) per session, per week, per muscle group |
| Frequency tracking | MVP | How often each muscle group is trained per week |
| Personal records | MVP | Automatic detection and highlighting of new PRs (weight, reps, volume) |
| Session comparison | MVP | Compare the current session to the previous session of the same template |
| 1RM estimation | Phase 2 | Calculate estimated one-rep max from working sets using Epley/Brzycki formulas |
| Muscle group heatmap | Phase 2 | Weekly visual overview showing which muscles were trained and their volume |
| Training volume trends | Phase 2 | Long-term charts of weekly volume by muscle group |
| Strength score | Phase 3 | Composite strength rating based on key lifts relative to bodyweight |

---

## User Stories

### Workout Logging
- As a user, I want to start a workout from my saved "Push Day" template so I don't have to set up exercises every time.
- As a user, I want to see what I lifted last time for each exercise so I know what to beat today.
- As a user, I want to add a set by tapping "+" and entering weight and reps quickly, without navigating away from the exercise.
- As a user, I want a rest timer that starts automatically after I log a set so I don't have to watch the clock.
- As a user, I want to add exercises mid-session if I decide to add something spontaneous.
- As a user, I want to see a session summary when I finish, showing total volume, PRs, and muscle groups hit.

### Exercise Library
- As a user, I want to search for "bench press" and see all variations (flat, incline, decline, dumbbell, barbell).
- As a user, I want to filter exercises by "chest" and "dumbbells only" when I'm training at home.
- As a user, I want to create a custom exercise for an unusual movement my coach taught me.
- As a user, I want to view my entire history for "Barbell Squat" on a chart to see my strength progression over months.

### Programs
- As a user, I want to create a 4-week Push/Pull/Legs program so I have a structured plan to follow.
- As a user, I want the app to tell me which workout is scheduled for today based on my active program.
- As a user, I want to skip a program day and have the schedule adjust (shift forward, not mark as failed).

### Social (Phase 2)
- As a tenant member, I want to see my partner's workout activity in our shared feed so I know they trained today.
- As a coach, I want to assign a program to a client and see their completion rate.
- As a user, I want to share a workout template with my friend so they can try my routine.

---

## Acceptance Criteria

### Set Logging
- Each set must capture at minimum: exercise, weight (or bodyweight), and reps (or duration)
- Weight units are user-configurable (kg or lbs) with automatic conversion display
- Sets can be reordered within an exercise via drag-and-drop
- Deleting a set requires a single swipe gesture with undo option (no confirmation dialog for speed)
- A warm-up set is excluded from volume calculations and PR tracking

### Personal Records
- A new PR is detected when the user logs a weight that exceeds their previous best for the same exercise at the same or higher rep count
- PRs are highlighted immediately after the set is logged (visual badge on the set)
- PR history is maintained per exercise and visible in the exercise history view
- PRs are only tracked for working sets (warm-up sets are excluded)

### Session Summary
- Displayed immediately after the user taps "Complete"
- Shows: total volume, total sets, session duration, exercises performed, muscle groups trained, PRs hit
- Comparison to previous session of the same template (if applicable): volume change, PR improvements

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Workout Session** | A single training event with a start time, end time, list of exercises, and optional notes |
| **Exercise** | A movement from the library (built-in or custom) with muscle group tags and equipment type |
| **Set** | A single effort within an exercise, recording weight, reps, duration, RPE, and set type |
| **Workout Template** | A saved sequence of exercises with target sets/reps that can be reused |
| **Workout Program** | A multi-week schedule of workout sessions with rest days |
| **Personal Record** | The best performance for a given exercise, tracked by weight at each rep range |
| **Training Volume** | Calculated as sets x reps x weight, aggregated per exercise, muscle group, session, or week |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Goals & Habits** | Completing a workout increments workout frequency goals; specific exercise PRs contribute to strength goals |
| **Running & Cardio** | Cardio exercises within a gym session (treadmill, rowing) also count toward cardio goals |
| **Diet & Nutrition** | Training day/rest day awareness for nutrition module (adjusting calorie/macro suggestions) |
| **Progress & Analytics** | Strength progression data feeds into the overall analytics dashboard |
| **Social & Accountability** | Completed sessions appear in tenant feed; templates and programs can be shared |
| **Preparation & Planning** | Training programs link to preparation timelines (e.g., "Strength phase" of marathon prep) |
