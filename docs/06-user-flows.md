# 06 -- User Flows

This document maps the key user journeys through GoalifyNow. Each flow describes the step-by-step experience from the user's perspective, the decision points, and the system responses.

---

## Flow 1: Onboarding & Profile Setup

**Trigger:** User opens GoalifyNow for the first time after downloading/visiting.

**Steps:**

1. **Welcome screen** -- GoalifyNow logo, tagline "Plan. Track. Share. Grow.", and two options: "Get Started" / "I have an account"
2. **Registration** -- Email/password OR continue with Google/Apple
3. **Basic profile** -- Name, date of birth, gender (optional), profile photo (optional)
4. **Body baseline (optional, skippable)** -- Current weight, height, activity level
5. **Goal selection** -- "What would you like to focus on?" (multi-select)
   - Lose weight
   - Build muscle
   - Run a race
   - Eat healthier
   - Build consistent habits
   - Train for an event
   - Track progress / body transformation
6. **Module activation** -- Based on selections, GoalifyNow highlights which modules matter most and offers to set up the first goal
7. **First goal wizard** -- Guided creation of the user's primary goal (e.g., "Lose 10kg in 6 months")
8. **Notification preferences** -- Choose reminder frequency and channels (push, email)
9. **Subscription prompt** -- Brief overview of Free vs Premium with option to start 14-day free trial or continue with Free tier
10. **Dashboard** -- User lands on their personalized dashboard with their first goal visible and "Quick actions" (Log a workout, Log a meal, Take a progress photo)

**Decision points:**
- Skipping body baseline should not block onboarding -- data can be added later
- Goal selection determines the initial dashboard layout and module emphasis
- Free trial prompt should feel inviting, not aggressive (user can dismiss and never see it again for 7 days)

---

## Flow 2: Creating and Tracking a Goal

**Trigger:** User taps "New Goal" from dashboard or goals section.

**Steps:**

1. **Goal type selection** -- Choose from templates or "Custom goal"
   - Templates: "Lose Weight", "Build Strength", "Run a 5K", "30-Day Challenge", "Read More", "Drink More Water", etc.
2. **Goal definition** -- Name, target metric, target value, deadline
   - Example: Name = "Run 100km this month", Metric = distance (km), Target = 100, Deadline = end of current month
3. **Milestone setup (optional)** -- Add intermediate milestones
   - Example: 25km by week 1, 50km by week 2, 75km by week 3
4. **Tracking method** -- How will progress be recorded?
   - Automatic (linked to workout/run/meal logs)
   - Manual entry
   - Habit-style (yes/no per day)
5. **Reminders** -- Set reminder schedule
6. **Sharing (if in a tenant)** -- Make this goal visible to tenant members? Yes/No
7. **Confirmation** -- Goal appears on dashboard with progress bar at 0%

**Ongoing tracking:**
- If automatic: progress updates as user logs workouts/runs/meals
- If manual: user taps the goal card to enter today's value
- If habit-style: simple check-in (done/not done)
- Streak counter appears if the goal involves daily actions
- Milestone celebrations appear as pop-ups when milestones are hit
- At deadline: goal marked as achieved (with celebration) or missed (with reflection prompt)

---

## Flow 3: Logging a Workout Session

**Trigger:** User taps "Log Workout" from dashboard, quick actions, or training module.

**Steps:**

1. **Session type selection**
   - Start from a saved template ("Push Day", "Full Body A", etc.)
   - Start from a program schedule (today's programmed workout)
   - Start a blank session
2. **Exercise selection** -- Search exercise library or recent exercises
   - Each exercise shows target muscles and a thumbnail
3. **Set logging** -- For each exercise:
   - Enter weight and reps (or duration for timed exercises)
   - Previous performance shown as reference ("Last time: 80kg x 8")
   - Add sets with "+" button
   - Mark set as warm-up, working, or failure
4. **Rest timer** -- Configurable timer starts automatically between sets (can be dismissed)
5. **Add exercises** -- Continue adding exercises to the session
6. **Session notes** -- Optional free-text notes about the session
7. **Finish workout** -- Tap "Complete"
8. **Session summary** -- Shows:
   - Total volume (sets x reps x weight)
   - Duration
   - Personal records hit (highlighted)
   - Comparison to previous session of the same template
   - Muscle groups trained (visual)
9. **Goal update** -- If any active goals are linked to workouts, progress updates automatically
10. **Tenant feed** -- If sharing is enabled, session appears in tenant activity feed

**Key interactions:**
- Swiping on a set to delete it
- Long-pressing an exercise to reorder
- Tapping on "Previous performance" to see full history for that exercise
- Quick duplicate: copy the last session and modify only what changed

---

## Flow 4: Logging a Meal / Scanning Food

**Trigger:** User taps "Log Meal" from dashboard, quick actions, or nutrition module.

**Steps:**

1. **Meal selection** -- Breakfast, Lunch, Dinner, Snack, or custom meal name
2. **Add food items** -- Multiple methods:
   - **Search:** Type food name, see matching results with calorie/macro preview
   - **Barcode scan:** Point camera at product barcode, auto-populate nutrition data
   - **Recent foods:** Quick-select from recently logged items
   - **Favorites:** Select from saved favorite foods
   - **Meal templates:** Load a saved meal (e.g., "My usual breakfast")
   - **Recipe:** Select from user-created recipes
3. **Portion adjustment** -- For each food item:
   - Adjust serving size (grams, cups, pieces, servings)
   - See macro values update in real-time as portion changes
4. **Review meal** -- Summary showing total calories, protein, carbs, fat for the meal
5. **Confirm** -- Meal logged, daily totals updated
6. **Daily nutrition view** -- Shows:
   - Remaining macros for the day (target minus consumed)
   - Meal-by-meal breakdown
   - Visual progress bars for each macro
   - Water intake tracker

**Key interactions:**
- Quick copy: duplicate yesterday's breakfast to today
- Meal photo (optional): take a photo of the meal for personal reference
- Edit any logged meal at any time during the day
- AI photo logging (Phase 2): take a photo and the system estimates food items and portions

---

## Flow 5: Recording Progress (Photo & Measurement)

**Trigger:** User taps "Progress" from dashboard or progress module.

**Steps:**

1. **Progress type selection**
   - Take a progress photo
   - Log body measurements
   - Log weight
2. **If progress photo:**
   - Camera opens with pose guide overlay (front, side, back)
   - User takes the photo
   - Photo is stored privately (not in phone's camera roll unless user opts in)
   - Tag: pose type, date, optional notes
   - If previous photos exist with the same pose: side-by-side comparison offered
3. **If body measurements:**
   - Form with standard fields: weight, body fat %, waist, chest, hips, biceps (L/R), thighs (L/R), calves (L/R)
   - Previous values shown as reference
   - Only changed fields need to be filled (others carry forward)
4. **If weight only:**
   - Simple number input
   - Weight trend chart updates immediately
   - Smoothed trend line shows actual trajectory vs daily fluctuations
5. **Goal connection** -- If weight/measurement goals exist, progress updates automatically
6. **Milestone check** -- If a milestone is hit (e.g., "Under 90kg for the first time"), celebration notification
7. **Optional sharing** -- Share the progress update to tenant feed (never shares actual photo or numbers without explicit selection of what to share)

---

## Flow 6: Inviting a Partner/Friend (Multi-Tenant)

**Trigger:** User taps "Invite People" from profile, settings, or the social module.

**Steps:**

1. **Tenant creation (if first invite)**
   - Name the tenant (e.g., "Mike & Sarah's Fitness", "Carlos's Clients", "The Running Crew")
   - Choose tenant type:
     - **Partner/Couple** -- Equal visibility, shared goals encouraged
     - **Friend Group** -- Social challenges focus
     - **Coach/Client** -- Coach sees client data, clients see only their own
2. **Invite method**
   - Email invitation (system sends invite link)
   - Share invite link (copy/paste to any messaging app)
   - QR code (for in-person invitations)
3. **Permission setup**
   - Default permissions based on tenant type
   - Customizable per-module: For each module, choose visibility level for the invitee:
     - Full visibility
     - Summary only (e.g., "Completed a workout" without details)
     - Hidden
4. **Invitee experience**
   - Receives invitation via email/link
   - If new user: completes onboarding first, then joins tenant
   - If existing user: accepts invitation and the tenant appears in their navigation
5. **Confirmation** -- Both users see each other in the tenant member list
6. **First interaction** -- System suggests: "Start a challenge together?" or "Share a goal with [name]?"

**Permission examples:**
- Clara shares workouts and goals with Dan but hides body measurements
- Coach Carlos sees everything from his clients; clients only see their own data
- Friend group shares only challenges and activity feed, all personal data is hidden

---

## Flow 7: Joining a Group Challenge

**Trigger:** User receives a challenge invitation OR browses available challenges in tenant.

**Steps:**

1. **Challenge discovery** -- User sees available challenges:
   - Invited by a tenant member
   - Browse tenant's open challenges
   - Browse template challenges (pre-built by GoalifyNow)
2. **Challenge preview** -- Shows:
   - Challenge name and description (e.g., "30-Day Workout Streak")
   - Duration (start date, end date)
   - Rules (e.g., "Log at least one workout every day for 30 days")
   - Current participants and their status
   - Prizes/stakes (bragging rights, or custom stakes set by creator)
3. **Join challenge** -- Tap "Join"
4. **Challenge dashboard** -- Shows:
   - Personal progress (days completed, current streak)
   - Leaderboard (all participants ranked)
   - Activity feed (who did what today)
   - Days remaining countdown
5. **Daily check-in** -- Automatic via tracked data (if challenge links to workout/meal/run logs) OR manual check-in
6. **Mid-challenge engagement:**
   - Notifications when falling behind ("Dan is 2 days ahead of you!")
   - Celebrations when milestones are hit within the challenge
   - Nudge option: tap to send an encouragement notification to a struggling participant
7. **Challenge completion:**
   - Final leaderboard
   - Personal stats (total workouts, best streak, etc.)
   - Badge awarded for completion
   - Prompt: "Create another challenge?" or "Rematch?"

---

## Flow 8: Subscription Purchase

**Trigger:** User hits a feature gate (premium feature) OR taps "Upgrade" from settings/profile.

**Steps:**

1. **Subscription page** -- Three tiers displayed with feature comparison:
   - **Free** -- Current tier (highlighted if user is on free)
   - **Premium** -- Full features for individuals
   - **Pro** -- For coaches and power users managing multiple people
2. **Tier selection** -- User taps on Premium or Pro
3. **Billing cycle** -- Choose Monthly or Annual (annual shows savings percentage)
4. **Payment** -- Redirect to App Store (iOS), Play Store (Android), or Stripe (web)
5. **Confirmation** -- "Welcome to GoalifyNow Premium!" with summary of unlocked features
6. **Feature unlock** -- Previously gated features become available immediately
7. **Onboarding prompt** -- Suggest setting up newly available features:
   - "You now have access to challenges! Want to invite a friend?"
   - "You can now track detailed analytics! Check out your workout trends."

**Feature gates (Free → Premium):**
- Free tier limits: 3 active goals, no challenges, no progress photo comparisons, basic analytics only, ads/promotional banners
- Premium unlock: unlimited goals, all social features, full analytics, photo comparisons, no ads, meal plans, workout programs
- Pro unlock: everything in Premium + manage up to 50 people in a coach tenant, client dashboard, bulk program assignment, priority support

**Cancellation flow:**
1. User taps "Manage Subscription" in settings
2. Shows current plan, renewal date, and "Cancel" option
3. Cancellation survey: "Why are you leaving?" (optional)
4. Confirmation with clarity: "Your Premium features will remain active until [renewal date]"
5. Downgrade to Free tier at renewal date (data is preserved, premium features become read-only)

---

## Flow Summary Matrix

| Flow | Primary Persona(s) | Modules Involved | Phase |
|------|-------------------|------------------|-------|
| Onboarding | All | Platform | MVP |
| Goal Creation | Ben, Mike, Diana | Goals & Habits | MVP |
| Workout Logging | Gina, Carlos, Dan | Training & Workouts | MVP |
| Meal Logging | Diana, Ben, Clara | Diet & Nutrition | MVP |
| Progress Recording | Gina, Diana, Mike | Progress & Analytics | MVP |
| Inviting People | Clara & Dan, Carlos | Social & Accountability | Phase 2 |
| Group Challenge | Clara & Dan, Friend groups | Social & Accountability | Phase 2 |
| Subscription Purchase | All (conversion point) | Platform | MVP |
