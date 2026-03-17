# 09 -- MVP Roadmap

## Phased Delivery Strategy

GoalifyNow is delivered in three major phases. Each phase is a complete, usable product -- not a partial build waiting for the next phase. Users can get value from Phase 1 alone; each subsequent phase adds depth and differentiation.

---

## Phase 1: MVP -- Core Self-Management

**Theme:** "The best personal tracking app you've ever used."

**Goal:** Deliver a polished single-user experience that covers goals, workouts, nutrition, and progress tracking well enough that a user would switch from their current apps. No social features yet -- this phase proves the core value.

**Target timeline:** 12--16 weeks from development start.

### Included Modules & Features

#### Goals & Habits (Full MVP Feature Set)
- Goal creation with targets, deadlines, and categories
- Goal templates for common objectives
- Habit tracking with streaks and grace days
- Recurring goals (daily, weekly, monthly)
- Milestone markers with celebrations
- Goal dashboard and progress bars
- Goal archiving and history

#### Training & Workouts (Full MVP Feature Set)
- Workout session logging (sets, reps, weight, duration, RPE)
- Exercise library (500+ exercises)
- Custom exercises
- Workout templates (3 on Free, unlimited on Premium)
- Workout programs (multi-week schedules)
- Rest timer
- Superset and circuit support
- Progressive overload tracking and PR detection
- Session summary with volume and muscle group overview
- Previous performance reference

#### Diet & Nutrition (Full MVP Feature Set)
- Meal logging with food database search
- Barcode scanner
- Daily macro tracking (calories, protein, carbs, fat)
- Portion adjustment with real-time macro recalculation
- Recent foods and favorites
- Meal templates
- Recipe creation with per-serving nutrition
- Water intake tracking with reminders
- Dietary preference profiles
- Macro target profiles (balanced, high-protein, keto, low-carb, custom)

#### Running & Cardio (Manual Logging Only)
- Manual cardio session logging (type, duration, distance, heart rate, RPE)
- Cardio types: running, cycling, swimming, rowing, walking, hiking, etc.
- Session notes and indoor/outdoor toggle
- No GPS tracking in Phase 1

#### Progress & Analytics (Core Features)
- Progress photo capture with pose guide
- Private photo storage
- Weight logging with smoothed trend algorithm
- Body measurement tracking (waist, chest, arms, etc.)
- Body fat percentage logging
- Goal progress dashboard
- Today's summary and weekly overview
- Workout volume and strength progression charts
- Nutrition trend charts (daily/weekly averages, adherence)
- Habit completion rate stats

#### Platform
- Responsive web app (desktop, tablet, mobile browser)
- Mobile app (iOS & Android -- PWA or native wrapper)
- User registration (email/password, Google, Apple)
- Guided onboarding with goal selection
- Push notifications and email reminders
- Dark mode
- Accessibility (WCAG 2.1 AA)
- Free and Premium subscription tiers via Stripe (web) and App Store/Play Store (mobile)

### MVP Success Criteria

| Metric | Target |
|--------|--------|
| Onboarding completion rate | > 70% |
| Day-7 retention | > 40% |
| Day-30 retention | > 25% |
| Free-to-Premium conversion (within 30 days) | > 5% |
| Average daily opens (active users) | > 2 |
| Features used per user (of 5 modules) | > 2.5 modules |

### What is Intentionally Excluded from MVP

- GPS run tracking (Phase 2)
- Running plans (Phase 2)
- Social features / tenants (Phase 2)
- Challenges and leaderboards (Phase 2)
- Photo side-by-side comparison (Phase 2)
- Weekly/monthly report generation (Phase 2)
- Meal plans (Phase 2)
- Micro-nutrient tracking (Phase 2)
- Wearable integrations (Phase 2)
- Offline support (Phase 2)
- Pro/Coach tier (Phase 2)
- All AI features (Phase 3)

---

## Phase 2: Social & Depth -- Multi-Tenant Collaboration

**Theme:** "Now bring your people."

**Goal:** Introduce the multi-tenant collaboration system, GPS running, advanced analytics, and the features that create social lock-in and retention differentiation. This phase is where GoalifyNow becomes something no competitor offers.

**Target timeline:** 10--14 weeks after Phase 1 launch.

### New Features

#### Social & Accountability (Full Phase 2 Feature Set)
- Tenant creation (Partner, Friend Group, Coach/Client)
- Invite system (email, link, QR code)
- Per-module permission controls
- Tenant activity feed with kudos/cheering
- Group challenges with automatic tracking and leaderboards
- Challenge templates
- Achievements and badge system
- Motivational nudges
- Coach dashboard (Pro tier)
- Program and meal plan assignment for coaches

#### Running & Cardio (Full GPS & Plans)
- Real-time GPS run tracking with live pace
- Route map with pace-colored segments
- Audio cues at each km/mile
- Auto-pause
- Elevation and split tracking
- Running plans (Couch-to-5K through marathon)
- Interval training with audio cues
- Race calendar and countdown
- Race result logging and history
- Shoe mileage tracking

#### Preparation & Planning (Full Phase 2 Feature Set)
- Event creation with countdown
- Auto-generated preparation timelines
- Training block planning
- Race-day checklists
- Tapering plans
- Equipment tracking
- Post-event review
- Competition history

#### Enhanced Analytics
- Photo side-by-side comparison
- Weight vs calorie correlation chart
- Running pace and volume trends
- 1RM estimation
- Muscle group heatmap
- Weekly and monthly digest reports
- Exportable PDF reports
- Coach client reports

#### Diet & Nutrition Enhancements
- Micro-nutrient tracking
- Training-day vs rest-day macro targets
- Meal plans and grocery list generation
- Photo meal logging (AI)
- Recipe sharing within tenants

#### Platform Enhancements
- Offline support for core logging features
- Wearable integrations (Apple Health, Google Fit, Garmin, Fitbit)
- Multi-language support (English, German, Polish)
- Data export (GDPR compliance)
- Pro subscription tier

### Phase 2 Success Criteria

| Metric | Target |
|--------|--------|
| Tenant creation rate (Premium users) | > 30% |
| Users in at least one tenant | > 40% |
| Day-30 retention (users in tenant) | > 45% |
| Day-30 retention (solo users) | > 30% |
| Challenge participation rate | > 20% of tenant members |
| Pro tier adoption (coaches) | > 500 subscribers by month 3 |
| GPS run sessions per active runner | > 3/week |

---

## Phase 3: Intelligence & Scale -- AI & Ecosystem

**Theme:** "GoalifyNow knows you better than you know yourself."

**Goal:** Add AI-powered recommendations, a content marketplace, enterprise features, and the advanced capabilities that make GoalifyNow an indispensable platform rather than a replaceable tool.

**Target timeline:** 14--20 weeks after Phase 2 launch.

### New Features

#### AI-Powered Features
- Smart goal suggestions based on user patterns
- AI workout recommendations based on training history
- AI macro adjustment based on weight trends and activity
- Cross-module correlation insights ("Your running improves when protein is above 140g")
- Body composition estimation from photos + measurements
- Voice meal logging
- Recipe import from URL
- Race time prediction

#### Marketplace & Community
- Community workout program marketplace
- Community meal plan sharing
- Public community challenges
- Sponsored challenges (brand partnerships)

#### Advanced Planning
- Nutrition periodization (auto-adjust macros by training phase)
- Multi-event season planning
- AI peaking strategy recommendations
- Cross-event analytics
- Weather integration for outdoor activities

#### Advanced Analytics
- Custom dashboards with drag-and-drop widgets
- Time-lapse progress photo view
- Year in review report
- Strength score composite rating
- Running cadence and stride analysis (with wearable)

#### Platform & Business
- Public API for third-party integrations
- Enterprise/B2B corporate wellness tier
- In-app messaging within tenants
- Restaurant menu nutrition lookup
- Advanced coach features (client retention metrics, bulk messaging)

### Phase 3 Success Criteria

| Metric | Target |
|--------|--------|
| AI feature engagement | > 50% of Premium users interact with AI features monthly |
| Marketplace transactions | > 100 programs/plans sold per month |
| Enterprise clients | > 10 companies onboarded |
| Annual retention rate | > 60% |
| NPS score | > 50 |
| Total registered users | > 100,000 |

---

## Cross-Phase Timeline

```
Week  0                    16                   30                    50
      |─── Phase 1 (MVP) ──|─── Phase 2 (Social) ──|──── Phase 3 (AI) ────|
      |                     |                        |                      |
      | Goals, Workouts,    | Tenants, Challenges,   | AI, Marketplace,     |
      | Nutrition, Progress  | GPS Running, Plans,    | Enterprise,          |
      | Photos, Dashboard    | Coach Tier, Reports    | Advanced Analytics   |
      |                     |                        |                      |
      | Single-user         | Multi-user             | Platform/Ecosystem   |
      | Core tracking       | Social & depth         | Intelligence & scale |
```

---

## Development Priorities Within Each Phase

### Priority Ordering Principles

1. **User-facing features before internal polish** -- Ship what users see first
2. **High-engagement features before niche features** -- Workout logging before interval builder
3. **Retention features before acquisition features** -- Streaks before SEO landing pages
4. **Revenue features before free features** -- Subscription flow before data export
5. **Multiplier features before additive features** -- Templates (reused daily) before one-time reports

### MVP Internal Milestones

| Milestone | Includes | Target Week |
|-----------|----------|-------------|
| M1: Foundation | Auth, profile, onboarding, basic UI shell, navigation | Week 3 |
| M2: Goals | Goal CRUD, habit tracking, streaks, dashboard | Week 6 |
| M3: Training | Workout logging, exercise library, templates, programs | Week 9 |
| M4: Nutrition | Meal logging, food database, barcode, macros, recipes | Week 12 |
| M5: Progress | Photos, measurements, weight trend, analytics charts | Week 14 |
| M6: Polish & Launch | Subscriptions, notifications, dark mode, bug fixes, performance | Week 16 |

---

## Risk Checkpoints

| Checkpoint | Question | Action if No |
|-----------|----------|-------------|
| Week 4 | Is onboarding flow achieving > 70% completion in internal testing? | Simplify onboarding before proceeding |
| Week 8 | Are workout logging flows < 30 seconds to start a session? | Redesign UX before adding more features |
| Week 12 | Is barcode scanning working with > 75% accuracy on test products? | Consider alternative food database provider |
| Week 14 | Do 3 out of 6 personas find the MVP useful in usability testing? | Adjust feature priorities before launch |
| Phase 2 Week 4 | Are tenant invites converting at > 50%? | Simplify invite flow |
| Phase 2 Week 8 | Are challenge participants engaging daily? | Redesign challenge notifications |
