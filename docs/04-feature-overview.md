# 04 -- Feature Overview

This document provides the complete feature map for GoalifyNow, organized by module. Each feature is tagged with a delivery phase:

- **MVP** -- Included in the first public release
- **Phase 2** -- Delivered in the second major release (social, running, integrations)
- **Phase 3** -- Advanced features (AI, marketplace, enterprise)

For detailed specifications of each module, see the [08-modules/](08-modules/) directory.

---

## Module 1: Goals & Habits

| Feature | Phase | Description |
|---------|-------|-------------|
| Goal creation with targets | MVP | Create goals with measurable targets (e.g., "Run 100km this month", "Lose 5kg by June") |
| Habit tracking with streaks | MVP | Define daily/weekly habits and track streaks |
| Goal categories | MVP | Organize goals by area: Fitness, Nutrition, Personal, Financial, Learning |
| Milestone markers | MVP | Set intermediate milestones within a goal |
| Streak recovery (grace days) | MVP | Allow 1--2 missed days without breaking a streak |
| Goal templates | MVP | Pre-built goal templates for common objectives |
| Recurring goals | MVP | Auto-resetting daily, weekly, monthly goals |
| Goal archiving & history | MVP | View completed/abandoned goals and their history |
| Smart goal suggestions | Phase 3 | AI-suggested goals based on user activity patterns |
| Goal dependencies | Phase 2 | Link goals (e.g., "Complete Couch-to-5K" feeds into "Run a half marathon") |
| Custom goal metrics | Phase 2 | Define custom units and tracking methods |

---

## Module 2: Training & Workouts

| Feature | Phase | Description |
|---------|-------|-------------|
| Workout logging | MVP | Log exercises with sets, reps, weight, duration, RPE |
| Exercise library | MVP | Searchable library of 500+ exercises with muscle group tags |
| Custom exercises | MVP | Create and save personal exercises |
| Workout templates | MVP | Save and reuse workout templates |
| Rest timer | MVP | Configurable rest timer between sets |
| Progressive overload tracking | MVP | Track weight/rep progression over time per exercise |
| Workout programs | MVP | Multi-week structured programs with scheduled sessions |
| Session notes | MVP | Add notes to individual sessions |
| Superset & circuit support | MVP | Group exercises into supersets, circuits, or drop sets |
| 1RM estimation | Phase 2 | Calculate estimated one-rep max from working sets |
| Muscle group heatmap | Phase 2 | Visual weekly overview of which muscles were trained |
| Workout sharing | Phase 2 | Share workouts with tenant members |
| Program marketplace | Phase 3 | Community-created workout programs |
| AI workout suggestions | Phase 3 | Recommendations based on training history and goals |
| Wearable heart rate integration | Phase 2 | Pull heart rate data during workouts from wearables |

---

## Module 3: Running & Cardio

| Feature | Phase | Description |
|---------|-------|-------------|
| GPS run tracking | Phase 2 | Real-time GPS tracking with pace, distance, elevation |
| Manual cardio logging | MVP | Log cardio sessions (type, duration, distance, heart rate) without GPS |
| Running plans | Phase 2 | Structured plans for 5K, 10K, half marathon, marathon |
| Pace zones | Phase 2 | Define and track training in heart rate / pace zones |
| Route history | Phase 2 | Save and revisit past routes |
| Interval training | Phase 2 | Structured interval sessions with audio cues |
| Race calendar | Phase 2 | Plan upcoming races with countdown and prep milestones |
| Split tracking | Phase 2 | Per-kilometer/mile splits with elevation data |
| Cadence & stride tracking | Phase 3 | Advanced running form metrics (with wearable) |
| Virtual races | Phase 3 | Compete with friends on the same route asynchronously |
| Weather integration | Phase 3 | Log weather conditions with runs, correlate with performance |

---

## Module 4: Diet & Nutrition

| Feature | Phase | Description |
|---------|-------|-------------|
| Meal logging | MVP | Log meals with food items, portions, and timing |
| Food database search | MVP | Search from a comprehensive food database |
| Barcode scanner | MVP | Scan packaged food barcodes for instant logging |
| Macro tracking (calories, protein, carbs, fat) | MVP | Daily macro targets with visual progress |
| Micro-nutrient tracking | Phase 2 | Track vitamins, minerals, fiber, sodium |
| Meal templates (favorites) | MVP | Save frequently eaten meals for quick logging |
| Recipe creation | MVP | Create recipes with ingredient lists and per-serving nutrition |
| Water intake tracking | MVP | Daily hydration logging and reminders |
| Meal plans | Phase 2 | Create or follow structured weekly meal plans |
| Photo meal logging | Phase 2 | Take a photo and auto-identify food items (AI) |
| Grocery list generation | Phase 2 | Generate shopping lists from meal plans |
| Dietary preference profiles | MVP | Set dietary preferences (vegan, keto, gluten-free, halal, etc.) |
| Restaurant menu lookup | Phase 3 | Search nutrition data for popular restaurant chains |
| AI macro adjustment | Phase 3 | Dynamic macro targets based on activity level and goals |
| Recipe sharing | Phase 2 | Share recipes with tenant members |

---

## Module 5: Progress & Analytics

| Feature | Phase | Description |
|---------|-------|-------------|
| Progress photos | MVP | Take and store timestamped body photos (front, side, back) |
| Body measurements | MVP | Track weight, body fat %, waist, chest, arms, legs, etc. |
| Weight trend chart | MVP | Smoothed weight trend line (like MacroFactor's algorithm) |
| Goal progress dashboard | MVP | Visual dashboard showing progress toward all active goals |
| Workout analytics | MVP | Volume, frequency, and strength progression charts |
| Nutrition analytics | MVP | Average macros, adherence to targets, trends over time |
| Photo comparison (side-by-side) | Phase 2 | Compare progress photos from different dates |
| Weekly/monthly summary reports | Phase 2 | Automated digest of the week's/month's activity |
| Exportable reports (PDF) | Phase 2 | Download or share progress reports |
| Cross-module correlation | Phase 3 | AI-driven insights connecting diet, training, sleep, and goals |
| Body composition estimation | Phase 3 | AI-estimated body composition from photos + measurements |
| Custom dashboards | Phase 3 | Build personalized dashboards with preferred widgets |

---

## Module 6: Social & Accountability

| Feature | Phase | Description |
|---------|-------|-------------|
| Tenant (shared space) creation | Phase 2 | Create a shared space and invite people by email/link |
| Permission controls | Phase 2 | Control what each tenant member can see (per module) |
| Activity feed within tenant | Phase 2 | See tenant members' recent activities |
| Group challenges | Phase 2 | Create time-bound challenges with rules and leaderboards |
| Streak sharing | Phase 2 | Visible streaks within tenant (social pressure) |
| Motivational nudges | Phase 2 | Send encouragement/reminders to tenant members |
| Coach-client relationship | Phase 2 | Coaches see client data with elevated permissions |
| Challenge templates | Phase 2 | Pre-built challenge templates (30-day challenges, weekly step goals, etc.) |
| Leaderboards | Phase 2 | Ranked standings within challenges and tenants |
| Achievements & badges | Phase 2 | Unlock badges for milestones (100 workouts, 30-day streak, etc.) |
| In-app messaging | Phase 3 | Direct messaging within tenants |
| Community challenges (public) | Phase 3 | Join global challenges with the broader GoalifyNow community |
| Cheering / kudos system | Phase 2 | React to tenant members' activities |

---

## Module 7: Preparation & Planning

| Feature | Phase | Description |
|---------|-------|-------------|
| Event creation with countdown | Phase 2 | Create a target event (race, competition, photoshoot) with a date |
| Preparation timeline | Phase 2 | Auto-generate a preparation timeline with milestones |
| Training block planning | Phase 2 | Define training phases (base, build, peak, taper) |
| Nutrition periodization | Phase 3 | Adjust nutrition targets based on training phase |
| Race-day checklist | Phase 2 | Customizable checklist for race/event day |
| Equipment tracking | Phase 2 | Track shoe mileage, gear condition, replacement reminders |
| Tapering plans | Phase 2 | Pre-built taper strategies for different race distances |
| Post-event review | Phase 2 | Structured reflection on what went well and what to improve |
| Competition history | Phase 2 | Log past races/competitions with results and notes |
| Multi-event season planning | Phase 3 | Plan an entire season of races/events with periodization |

---

## Platform & Infrastructure Features

| Feature | Phase | Description |
|---------|-------|-------------|
| Responsive web app | MVP | Full-featured web application (desktop, tablet, mobile browser) |
| Mobile app (iOS & Android) | MVP | Native-feel mobile app (PWA or native wrapper) |
| User registration & authentication | MVP | Email/password, Google, Apple sign-in |
| Profile setup & onboarding | MVP | Guided onboarding flow with goal selection |
| Notification system | MVP | Push notifications, email reminders, in-app notifications |
| Dark mode | MVP | Full dark mode support |
| Offline support | Phase 2 | Core logging features work offline and sync when connected |
| Data export | Phase 2 | Export all user data (GDPR compliance) |
| Wearable integrations | Phase 2 | Apple Health, Google Fit, Garmin, Fitbit, Whoop sync |
| Multi-language support | Phase 2 | i18n framework with English, German, Polish at minimum |
| Accessibility (WCAG 2.1 AA) | MVP | Accessible to users with disabilities |
| API for third-party integrations | Phase 3 | Public API for developers and integrations |

---

## Feature Count Summary

| Phase | Feature Count |
|-------|--------------|
| MVP | ~45 features |
| Phase 2 | ~50 features |
| Phase 3 | ~20 features |
| **Total** | **~115 features** |
