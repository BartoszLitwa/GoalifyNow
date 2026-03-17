# Module 08c -- Running & Cardio

## Purpose

The Running & Cardio module serves runners, cyclists, and anyone doing cardiovascular training. It provides GPS tracking, structured running plans, race preparation tools, and cardio logging. This module competes with Strava and Nike Run Club while offering tight integration with GoalifyNow's other modules.

---

## Feature List

### Cardio Logging

| Feature | Phase | Description |
|---------|-------|-------------|
| Manual cardio logging | MVP | Log any cardio session with type, duration, distance, and average heart rate |
| Cardio types | MVP | Running, cycling, swimming, rowing, walking, hiking, elliptical, stair climber, jump rope, other |
| Effort rating | MVP | Rate session effort on a 1--10 RPE scale |
| Session notes | MVP | Free-text notes about the session (conditions, how it felt, etc.) |
| Indoor/outdoor toggle | MVP | Mark whether the session was indoor or outdoor |

### GPS Tracking

| Feature | Phase | Description |
|---------|-------|-------------|
| Real-time GPS run tracking | Phase 2 | Live GPS tracking with current pace, distance, and elapsed time |
| Route map | Phase 2 | Map view of the completed route with color-coded pace segments |
| Live pace display | Phase 2 | Current pace, average pace, and target pace displayed during tracking |
| Audio cues | Phase 2 | Configurable audio updates at each kilometer/mile (pace, distance, time) |
| Auto-pause | Phase 2 | Automatically pause tracking when the user stops moving |
| Elevation tracking | Phase 2 | Track elevation gain/loss throughout the route |
| Split tracking | Phase 2 | Per-kilometer or per-mile split times with elevation data |
| Route history | Phase 2 | Save, name, and revisit past routes |
| Route sharing | Phase 2 | Share a route with tenant members |

### Running Plans

| Feature | Phase | Description |
|---------|-------|-------------|
| Pre-built running plans | Phase 2 | Structured plans for Couch-to-5K, 5K, 10K, half marathon, and marathon |
| Plan customization | Phase 2 | Adjust plan start date, race date, and weekly run days |
| Plan schedule view | Phase 2 | Calendar view showing planned runs with type (easy, tempo, interval, long run) |
| Run type tagging | Phase 2 | Tag runs as easy, tempo, interval, fartlek, long run, recovery, or race |
| Pace zones | Phase 2 | Define personal pace zones (recovery, easy, tempo, threshold, interval, repetition) |
| Heart rate zones | Phase 2 | Define and track training by heart rate zones (if wearable connected) |
| Plan adherence tracking | Phase 2 | Track completion rate and whether actual paces matched planned paces |

### Interval Training

| Feature | Phase | Description |
|---------|-------|-------------|
| Interval workout builder | Phase 2 | Create custom interval sessions (e.g., 8x400m with 90s rest) |
| Pre-built interval templates | Phase 2 | Common interval workouts (Yasso 800s, 12x400, tempo intervals) |
| Audio interval cues | Phase 2 | Audio alerts for "GO" and "REST" phases during interval runs |
| Interval tracking | Phase 2 | Track each interval individually with split time, pace, and heart rate |

### Race Management

| Feature | Phase | Description |
|---------|-------|-------------|
| Race calendar | Phase 2 | Add upcoming races with date, distance, location, and goal time |
| Race countdown | Phase 2 | Countdown timer on dashboard to next race |
| Race result logging | Phase 2 | Log race results with finish time, overall/age group placement, and notes |
| Race history | Phase 2 | View all past races with results and trend |
| Race prediction | Phase 3 | Estimate race time based on training data (using Riegel formula or similar) |
| Virtual races | Phase 3 | Complete a route asynchronously and compare times with friends |

### Advanced Features

| Feature | Phase | Description |
|---------|-------|-------------|
| Cadence tracking | Phase 3 | Steps per minute tracking (requires wearable with accelerometer) |
| Stride length estimation | Phase 3 | Calculate average stride length from cadence and pace |
| Weather logging | Phase 3 | Auto-log weather conditions (temperature, humidity, wind) for each outdoor run |
| Shoe tracking | Phase 2 | Track mileage on specific shoes and get replacement reminders |
| Wearable sync | Phase 2 | Import runs from Garmin, Apple Watch, Fitbit, and other devices |

---

## User Stories

### Cardio Logging
- As a user, I want to quickly log a 30-minute treadmill run with distance and heart rate without needing GPS.
- As a user, I want to log a swimming session by duration and effort level since GPS doesn't work in pools.
- As a user, I want my cardio sessions to count toward my weekly exercise goal automatically.

### GPS Tracking
- As a runner, I want to start GPS tracking with one tap and see my current pace and distance in real-time.
- As a runner, I want to hear audio updates at each kilometer telling me my split time and average pace.
- As a runner, I want to view my route on a map after the run, color-coded by pace (green for fast, red for slow).
- As a runner, I want the app to auto-pause when I stop at traffic lights so my average pace isn't diluted.

### Running Plans
- As a beginner, I want to follow a Couch-to-5K plan that tells me exactly what to do each day.
- As a marathon trainee, I want an 18-week marathon plan that includes easy runs, tempo runs, intervals, and long runs.
- As a runner, I want to see which runs I completed this week versus what was planned.
- As a runner, I want to adjust my plan if I miss a week (shift the schedule rather than showing 7 missed days).

### Race Management
- As a runner, I want to add my upcoming half marathon to a race calendar with a countdown on my dashboard.
- As a runner, I want to log my race result (1:52:34, 847th overall, 123rd in age group) and see it in my history.
- As a runner, I want to see my race times improving over the same distance across multiple events.

### Social (Phase 2)
- As a tenant member, I want to see my partner's run on a map in our activity feed.
- As a tenant member, I want to challenge my friend to a "most kilometers this month" competition.
- As a runner, I want to share my race result with my tenant with a single tap.

---

## Acceptance Criteria

### GPS Tracking
- GPS tracking must start within 5 seconds of the user tapping "Start"
- Pace display updates at minimum every 5 seconds
- Auto-pause activates when speed drops below 1.5 km/h for 10+ seconds
- Route is saved even if the app crashes or phone restarts (background persistence)
- Battery usage notification if tracking exceeds 2 hours
- GPS data is stored locally if offline and synced when connection is restored

### Running Plans
- Each plan run specifies: type (easy/tempo/interval/long), target distance or duration, and target pace range
- Completing a GPS-tracked run automatically matches it to the planned run for that day
- Missed runs are marked as "Skipped" with an option to reschedule
- Plan adherence percentage = (completed runs / scheduled runs) x 100

### Split Tracking
- Splits are recorded at each full kilometer (or mile, based on user preference)
- Each split shows: time, pace, elevation change, and heart rate (if available)
- Best split and worst split are highlighted in the session summary
- Splits are visible on the route map as markers

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Cardio Session** | Any cardiovascular training event with type, duration, distance, heart rate, and effort rating |
| **GPS Track** | A series of GPS coordinates with timestamps forming a route, plus derived data (pace, elevation, splits) |
| **Split** | A single kilometer/mile segment within a GPS-tracked session |
| **Running Plan** | A multi-week schedule of runs with type, distance/duration, and pace targets |
| **Planned Run** | A single scheduled run within a running plan |
| **Race** | An event entry with date, distance, location, goal time, and (after completion) actual result |
| **Pace Zone** | A named pace range (e.g., "Easy: 5:30--6:00/km") for categorizing effort |
| **Shoe** | A tracked piece of footwear with name, purchase date, and cumulative mileage |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Goals & Habits** | Run distance contributes to distance goals; run frequency contributes to exercise habits |
| **Training & Workouts** | Treadmill/rowing machine sessions in the gym count as both workout and cardio entries |
| **Diet & Nutrition** | Long run days can trigger higher carbohydrate targets; calorie expenditure feeds into net calorie calculations |
| **Progress & Analytics** | Running pace trends, volume trends, and race time progression appear in analytics |
| **Social & Accountability** | GPS routes and race results shareable in tenant feed; distance challenges between members |
| **Preparation & Planning** | Running plans link to race preparation timelines; tapering integrates with event countdown |
