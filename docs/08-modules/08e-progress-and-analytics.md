# Module 08e -- Progress & Analytics

## Purpose

The Progress & Analytics module is GoalifyNow's "proof it's working" layer. It aggregates data from all other modules into visual dashboards, trend charts, progress photos, and exportable reports. This module creates emotional investment (transformation photos are hard to abandon) and provides the data-driven feedback loop that keeps users motivated.

---

## Feature List

### Progress Photos

| Feature | Phase | Description |
|---------|-------|-------------|
| Progress photo capture | MVP | In-app camera with pose guide overlay (front, side, back) |
| Private photo storage | MVP | Photos stored securely in the app, not in the phone's camera roll (unless user opts in) |
| Photo tagging | MVP | Tag each photo with pose type, date, and optional notes |
| Photo gallery | MVP | Chronological gallery of all progress photos, filterable by pose type |
| Side-by-side comparison | Phase 2 | Select two photos of the same pose from different dates for comparison |
| Time-lapse view | Phase 3 | Animated slideshow of progress photos over time |
| Photo privacy controls | MVP | Progress photos are never shared unless the user explicitly selects specific photos to share |

### Body Measurements

| Feature | Phase | Description |
|---------|-------|-------------|
| Weight logging | MVP | Log body weight with date/time |
| Weight trend algorithm | MVP | Smoothed trend line that filters out daily fluctuations (weighted moving average) |
| Body measurement tracking | MVP | Track waist, chest, hips, biceps (L/R), thighs (L/R), calves (L/R), neck, shoulders |
| Body fat percentage | MVP | Log body fat % (manual entry, or from smart scale sync in Phase 2) |
| Measurement history | MVP | View all historical entries for any measurement |
| Measurement trend charts | MVP | Line charts for each measurement over time |
| Carry-forward defaults | MVP | When logging, pre-fill fields with last recorded values; user only updates what changed |

### Dashboards

| Feature | Phase | Description |
|---------|-------|-------------|
| Goal progress dashboard | MVP | All active goals with progress bars, streaks, and milestone indicators |
| Today's summary | MVP | What was done today: workouts, meals logged, goals progressed, habits checked |
| Weekly overview | MVP | Summary of the week: workouts completed, average macros, goals advanced, habit streaks |
| Training dashboard | MVP | Workout frequency, volume trends, PRs this month |
| Nutrition dashboard | MVP | Average daily macros, calorie trends, adherence to targets |
| Custom dashboards | Phase 3 | Drag-and-drop widget builder for personalized dashboard layouts |

### Analytics & Charts

| Feature | Phase | Description |
|---------|-------|-------------|
| Workout volume trends | MVP | Weekly/monthly total volume charts (sets x reps x weight) |
| Strength progression | MVP | Per-exercise weight progression over time |
| Workout frequency | MVP | Days trained per week/month |
| Nutrition trends | MVP | Daily/weekly calorie and macro averages |
| Calorie adherence | MVP | Percentage of days within target calorie range |
| Weight vs calorie chart | Phase 2 | Overlay weight trend with calorie intake trend to show correlation |
| Running pace trends | Phase 2 | Average pace per run over time, filterable by run type |
| Running volume | Phase 2 | Weekly/monthly distance totals |
| Habit completion rates | MVP | Per-habit completion percentage and trends |
| Cross-module correlation | Phase 3 | AI-driven charts showing relationships between training, diet, sleep, and progress |
| Body composition estimation | Phase 3 | AI-estimated body composition from photos combined with measurements |

### Reports

| Feature | Phase | Description |
|---------|-------|-------------|
| Weekly digest | Phase 2 | Automated end-of-week summary covering all modules |
| Monthly report | Phase 2 | Comprehensive monthly review with key metrics and trends |
| Exportable PDF | Phase 2 | Download any report as a formatted PDF |
| Coach reports | Phase 2 | Pro tier: generate client-specific reports for review sessions |
| Year in review | Phase 3 | Annual summary of all achievements, total stats, and transformation |

---

## User Stories

### Progress Photos
- As a user, I want to take a progress photo with a pose guide so my photos are consistent and comparable.
- As a user, I want my progress photos stored privately in the app, not visible in my phone's photo library.
- As a user, I want to select two photos from different months and see them side-by-side to see my transformation.
- As a user, I want to share a specific progress photo comparison to my tenant feed to celebrate my progress (without sharing all photos).

### Body Measurements
- As a user, I want to log my weight every morning and see a smoothed trend line that ignores daily water weight fluctuations.
- As a user, I want to quickly log just my weight without having to fill in all body measurements.
- As a user, I want to see my waist measurement declining over 3 months on a chart so I can see that my cut is working.
- As a user, I want last week's measurements pre-filled so I only need to update the fields that changed.

### Dashboards
- As a user, I want to open the app and immediately see how today is going: meals logged, workout done, goals on track.
- As a user, I want a weekly view that tells me at a glance whether this was a good week or not.
- As a coach, I want to see a dashboard summarizing all my clients' adherence this week.

### Analytics
- As a user, I want to see that my bench press has gone from 60kg to 80kg over the past 4 months on a clear chart.
- As a user, I want to see my average daily protein intake for the past month to check if I'm hitting targets.
- As a user, I want to see whether my running pace has improved since I started following a training plan.

### Reports
- As a user, I want a weekly email digest summarizing my workouts, nutrition, and goal progress.
- As a user, I want to download a PDF of my 3-month progress to show my doctor.
- As a coach, I want to generate a monthly report for a client before our check-in meeting.

---

## Acceptance Criteria

### Weight Trend Algorithm
- The trend line uses an exponentially weighted moving average (EWMA) with a smoothing factor of 0.1
- Daily weight is plotted as individual data points; the trend line is a continuous curve
- The trend line updates within 2 seconds of a new weight entry
- Users can toggle between "raw data" and "trend only" views
- The algorithm handles missing days gracefully (gaps in data don't break the trend)

### Progress Photos
- Photos are captured at a minimum resolution of 1080px on the longest side
- Pose guide overlay shows a semi-transparent silhouette of the target pose (front/side/back)
- Photos are stored encrypted at rest
- Side-by-side comparison allows pinch-to-zoom and swipe-to-toggle between photos
- Sharing a photo to the tenant feed requires explicit confirmation on each share

### Dashboards
- Dashboard loads within 2 seconds of app open
- "Today's summary" updates in real-time as the user logs activities throughout the day
- Weekly overview data is pre-calculated nightly to avoid slow queries
- All charts support date range selection (last 7 days, last 30 days, last 90 days, last year, all time)

### Reports
- Weekly digest is generated every Sunday at midnight (user's timezone) and delivered by Monday morning
- Reports include data from all active modules (only sections with data are included)
- PDF export supports all charts as rendered images + data tables
- Coach reports include only the selected client's data, never other clients' data

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Weight Entry** | A single body weight recording with timestamp and optional notes |
| **Weight Trend** | A calculated smoothed value derived from raw weight entries using EWMA |
| **Body Measurement** | A set of circumference measurements (waist, chest, etc.) recorded at a point in time |
| **Progress Photo** | A timestamped body photo with pose type tag, stored privately |
| **Dashboard Widget** | A visual component displaying a specific metric or chart on the dashboard |
| **Analytics Chart** | A time-series chart derived from data in one or more modules |
| **Report** | A generated document (weekly/monthly/annual) aggregating metrics across all modules |
| **Digest** | An automated notification or email summarizing recent activity and progress |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Goals & Habits** | Goal progress visualized on dashboards; habit completion rates in analytics |
| **Training & Workouts** | Workout volume, frequency, and strength data power training analytics |
| **Running & Cardio** | Pace, distance, and frequency data power running analytics |
| **Diet & Nutrition** | Calorie and macro data power nutrition analytics; weight trend correlates with diet |
| **Social & Accountability** | Weekly digests can be shared with tenant; coach reports summarize client data |
| **Preparation & Planning** | Preparation progress visualized on timelines; post-event analytics for review |
