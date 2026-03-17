# 05 -- User Personas

This document defines the six primary user personas that represent GoalifyNow's target audience. Every feature decision, user flow, and design choice should be validated against at least one of these personas.

---

## Persona 1: Marathon Mike

**Profile:** Amateur runner preparing for races

| Attribute | Detail |
|-----------|--------|
| Age | 32 |
| Occupation | Software engineer |
| Location | London, UK |
| Fitness level | Intermediate runner (2 half marathons completed, training for first full marathon) |
| Current apps | Strava (running), MyFitnessPal (diet during training), Google Sheets (training plan) |
| Monthly app spend | ~$15 (Strava Summit + MFP Premium) |
| Devices | iPhone 15, Apple Watch Series 9, MacBook |

**Goals:**
- Complete a sub-4-hour marathon in 6 months
- Follow a structured 18-week training plan
- Optimize nutrition for endurance performance
- Track weekly mileage and long run progression
- Document the journey (for personal motivation and social media)

**Frustrations:**
- Training plan lives in a spreadsheet that doesn't connect to his run data
- Has to manually cross-reference Strava runs with MFP nutrition data
- No easy way to see "Am I on track for my race goal?" in a single view
- Strava is social but nobody cares about his macro splits; MFP is private but nobody sees his runs

**GoalifyNow value:**
- Single app for the training plan, run logging, nutrition tracking, and race preparation
- Countdown to race day with milestone tracking ("First 20-miler", "Peak week", "Taper starts")
- Shares progress with his running partner (also on GoalifyNow) in a shared tenant
- Cross-module insight: "Your long run performance improves when your carb intake is above 300g the day before"

**Key modules:** Running & Cardio, Diet & Nutrition, Preparation & Planning, Goals & Habits, Progress & Analytics

---

## Persona 2: Gym Gina

**Profile:** Dedicated gym-goer focused on strength training and body composition

| Attribute | Detail |
|-----------|--------|
| Age | 27 |
| Occupation | Marketing coordinator |
| Location | Chicago, USA |
| Fitness level | Advanced gym-goer (3 years consistent, follows PPL split) |
| Current apps | Strong (workouts), Cronometer (nutrition), Instagram (progress photos) |
| Monthly app spend | ~$15 (Strong Pro + Cronometer Gold) |
| Devices | Samsung Galaxy S24, Galaxy Watch 6 |

**Goals:**
- Build lean muscle while slowly reducing body fat
- Hit specific strength milestones (200lb squat, 135lb bench)
- Track body composition changes over 6 months
- Maintain consistent protein intake above 140g/day

**Frustrations:**
- Takes progress photos on her phone camera -- they're mixed in with regular photos, no organization
- Strong tracks her workouts perfectly but has zero diet awareness
- Cronometer tracks nutrition but doesn't know she just did a heavy leg day (can't adjust protein targets)
- No single view showing "Is my body composition improving alongside my strength?"
- Shares progress photos on Instagram but doesn't want the public to see her actual workout data or diet

**GoalifyNow value:**
- Dedicated progress photo system with timestamps, comparisons, and private storage
- Workout logging with progressive overload tracking (her existing workflow)
- Nutrition tracking that knows today was "heavy leg day" and adjusts reminders
- Body measurement tracking alongside strength data -- one dashboard
- Shares with her gym buddy in a private tenant (visible workouts and progress, diet stays private)

**Key modules:** Training & Workouts, Diet & Nutrition, Progress & Analytics, Goals & Habits

---

## Persona 3: Balanced Ben

**Profile:** Holistic self-improver managing multiple life dimensions

| Attribute | Detail |
|-----------|--------|
| Age | 35 |
| Occupation | Product manager |
| Location | Toronto, Canada |
| Fitness level | Intermediate (mix of gym and running, started yoga) |
| Current apps | Habitica (habits), Strava (running), Nike Training Club (gym), Lose It (diet) |
| Monthly app spend | ~$22 (Habitica, Strava, Lose It subscriptions) |
| Devices | iPhone 14 Pro, Apple Watch SE |

**Goals:**
- Lose 10kg over 6 months (was 95kg, target 85kg)
- Exercise 5 days/week (3 gym, 2 runs)
- Meditate daily
- Read 2 books/month
- Reduce alcohol to weekends only
- Track all of this in fewer apps

**Frustrations:**
- Uses 4 apps and still feels disorganized
- Habitica is fun but checking "went to the gym" doesn't tell him if the workout was any good
- No overview of "How is my overall life going this week?" across all dimensions
- Each app sends notifications independently -- notification fatigue
- Paying $22/month across 4 apps feels excessive for what he gets

**GoalifyNow value:**
- Single dashboard showing all active goals across fitness, nutrition, and personal habits
- Replace 4 apps with 1 subscription
- Goal categories: Fitness, Nutrition, Personal, Reading -- all in one place
- "My Week at a Glance" summary combining workouts completed, diet adherence, habits maintained, and weight trend
- The weight loss goal connects to diet logging and exercise frequency automatically

**Key modules:** Goals & Habits, Training & Workouts, Diet & Nutrition, Progress & Analytics

---

## Persona 4: Clara & Dan (Couple)

**Profile:** Couple motivating each other through shared health goals

| Attribute | Detail |
|-----------|--------|
| Age | 29 (Clara), 31 (Dan) |
| Occupations | Clara: UX designer; Dan: Accountant |
| Location | Berlin, Germany |
| Fitness level | Clara: Beginner (just started running); Dan: Intermediate (regular gym-goer) |
| Current apps | Clara: Couch-to-5K, Noom; Dan: Hevy, MyFitnessPal |
| Monthly app spend | ~$75 combined (Noom alone is $59) |
| Devices | Both: iPhones, Clara has an Apple Watch |

**Goals:**
- Clara: Complete Couch-to-5K, lose 8kg, develop consistent exercise habit
- Dan: Gain muscle, improve diet (too much takeaway), support Clara's running
- Together: Do a 10K race together in 6 months
- Together: Cook healthy meals at home 5x/week
- Together: Hold each other accountable daily

**Frustrations:**
- They text each other "Did you work out today?" which is easy to lie about or ignore
- Dan can't see Clara's running progress and vice versa for his gym sessions
- Noom costs $59/month and Dan doesn't use it (it's Clara's thing)
- No shared meal planning -- they each track food independently
- Want to train for the same race but use completely different apps

**GoalifyNow value:**
- Shared tenant where both see each other's workouts, goals, and streaks
- Joint challenge: "10K race preparation" with both their training plans visible
- Shared meal logging for dinners they cook together (log once, applies to both)
- Clara sees Dan's gym streak (motivates her); Dan sees Clara's running progress (proud of her)
- One subscription covers both of them in a shared space -- replaces $75/month in separate apps
- Permission control: Clara keeps her weight measurements private; Dan keeps his body photos private

**Key modules:** Social & Accountability, Goals & Habits, Running & Cardio, Training & Workouts, Diet & Nutrition

---

## Persona 5: Coach Carlos

**Profile:** Independent personal trainer managing multiple clients

| Attribute | Detail |
|-----------|--------|
| Age | 38 |
| Occupation | Self-employed personal trainer (15 active clients) |
| Location | Miami, USA |
| Fitness level | Advanced (former competitive bodybuilder) |
| Current apps | Trainerize ($50/mo), Google Sheets (meal plans), WhatsApp (client communication) |
| Monthly app spend | ~$50 (Trainerize only) |
| Devices | iPhone 15 Pro Max, iPad Pro |

**Goals:**
- Deliver professional-quality training programs and meal plans to all 15 clients
- Monitor client compliance and progress without constant WhatsApp messages
- Scale to 30 clients without doubling his admin time
- Differentiate his service from other local trainers
- Charge clients $150--200/month and provide real tool access as part of the package

**Frustrations:**
- Trainerize handles workout programming but meal plans are in Google Sheets
- Client progress photos come via WhatsApp -- disorganized, no comparison tools
- Can't see a client's workout adherence alongside their diet adherence in one view
- Clients don't like the Trainerize UI -- it feels corporate and complicated
- Adding a meal planning tool would be another $30--50/month on top of Trainerize

**GoalifyNow value:**
- Pro/Coach tier: one dashboard showing all 15 clients' workouts, diet, progress, and goals
- Assigns workout programs AND meal plans from the same platform
- Clients use GoalifyNow as their personal app (included in their coaching fee via coach's Pro subscription)
- Progress photos, body measurements, and workout data in one client profile
- Client-coach tenant: Carlos sees everything; clients see only their own data (unless they opt into a group)
- Template system: create a meal plan or workout program once, assign to multiple clients

**Key modules:** Social & Accountability (coach-client model), Training & Workouts, Diet & Nutrition, Progress & Analytics, Goals & Habits

---

## Persona 6: Diet Diana

**Profile:** Weight loss-focused user prioritizing nutrition and body composition

| Attribute | Detail |
|-----------|--------|
| Age | 42 |
| Occupation | School teacher |
| Location | Sydney, Australia |
| Fitness level | Beginner (walks daily, starting light gym work) |
| Current apps | MyFitnessPal (calorie counting), Noom (behavior coaching -- cancelled after 3 months) |
| Monthly app spend | ~$10 (MFP Premium) |
| Devices | iPhone 13, no wearable |

**Goals:**
- Lose 15kg over 12 months (sustainable, not crash dieting)
- Learn to cook healthier meals
- Understand macros, not just calories
- Start a basic gym routine (intimidated by the gym)
- Track progress without obsessing over daily weight fluctuations

**Frustrations:**
- MyFitnessPal's food database has too many user-submitted entries with wrong data
- Noom's psychology-based approach felt patronizing and wasn't worth $59/month
- Scared of the gym and doesn't know where to start with strength training
- Daily weight fluctuations cause anxiety -- needs a smoothed trend view
- No one to talk to about her journey (doesn't want to post on social media)

**GoalifyNow value:**
- Accurate food database with barcode scanning (like Cronometer's accuracy, not MFP's chaos)
- Weight trend algorithm that smooths daily fluctuations and shows the real trajectory
- Beginner workout programs with video guidance (removes gym intimidation)
- Recipe system with per-serving macro calculation (supports her goal to cook more)
- Private progress photos she never has to share with anyone
- Optional: invite her sister into a tenant for gentle accountability without public exposure
- Goal framework: "Lose 15kg in 12 months" with automatic monthly milestones (1.25kg/month)

**Key modules:** Diet & Nutrition, Progress & Analytics, Goals & Habits, Training & Workouts (beginner focus)

---

## Persona Priority Matrix

| Persona | Revenue Tier | Acquisition Cost | Retention Likelihood | Strategic Importance |
|---------|-------------|-----------------|---------------------|---------------------|
| Marathon Mike | Premium | Medium | High (race commitment) | High (power user, word-of-mouth) |
| Gym Gina | Premium | Low | High (habit-driven) | High (core demographic) |
| Balanced Ben | Premium | Low | Medium (multi-goal keeps engagement) | Very High (proves the "all-in-one" value) |
| Clara & Dan | Premium (x2) | Medium (pair acquisition) | Very High (social lock-in) | Very High (proves multi-tenant value) |
| Coach Carlos | Pro ($19.99+) | High (B2B sales cycle) | Very High (business dependency) | High (revenue, brings clients) |
| Diet Diana | Free → Premium | Low | Medium (needs emotional hooks) | Medium (large market, lower ARPU) |

---

## Persona Coverage by Module

| Module | Mike | Gina | Ben | Clara & Dan | Carlos | Diana |
|--------|------|------|-----|-------------|--------|-------|
| Goals & Habits | High | Medium | Very High | High | High | High |
| Training & Workouts | Medium | Very High | High | Medium | Very High | Medium |
| Running & Cardio | Very High | Low | Medium | High | Low | Low |
| Diet & Nutrition | High | High | High | High | Very High | Very High |
| Progress & Analytics | High | Very High | Medium | Medium | Very High | High |
| Social & Accountability | Medium | Medium | Low | Very High | Very High | Low |
| Preparation & Planning | Very High | Low | Low | High | Medium | Low |
