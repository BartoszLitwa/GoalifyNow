# 07 -- Subscription & Monetization

## Revenue Model

GoalifyNow operates as a **freemium SaaS** with three subscription tiers. Revenue is driven by individual Premium subscriptions, couple/group Premium subscriptions, and Pro tier subscriptions from coaches and power users.

---

## Subscription Tiers

### Free Tier

**Purpose:** Acquire users, demonstrate value, create habit before asking for money.

| Feature Area | Free Tier Limits |
|-------------|-----------------|
| Goals & Habits | Up to 3 active goals, basic streak tracking |
| Training & Workouts | Unlimited workout logging, 3 saved templates |
| Running & Cardio | Manual cardio logging only (no GPS tracking) |
| Diet & Nutrition | Meal logging with food search and barcode scanner, basic macro view |
| Progress & Analytics | Weight logging with trend chart, basic workout stats |
| Progress Photos | Unlimited photo storage, no side-by-side comparison |
| Social & Accountability | View-only (can be invited to a tenant but cannot create one) |
| Challenges | Cannot create or join challenges |
| Reports | No exportable reports |
| Ads | Occasional non-intrusive promotional banners |

**Design principle:** Free tier must be useful enough that users build the habit of daily logging. The conversion trigger is wanting more (analytics, social features, unlimited goals) -- not being annoyed into paying.

### Premium Tier -- $9.99/month or $79.99/year

**Purpose:** Core revenue driver for individual users and couples.

| Feature Area | Premium Unlocks |
|-------------|----------------|
| Goals & Habits | Unlimited goals, milestone markers, goal templates, goal dependencies, custom metrics |
| Training & Workouts | Unlimited templates, workout programs, 1RM estimation, muscle heatmap |
| Running & Cardio | GPS tracking, running plans, race calendar, interval training, route history |
| Diet & Nutrition | Meal plans, micro-nutrient tracking, grocery list generation, recipe sharing, photo meal logging |
| Progress & Analytics | Photo side-by-side comparison, weekly/monthly reports, exportable PDFs, full analytics suite |
| Social & Accountability | Create tenants, invite up to 10 people, challenges, leaderboards, badges, nudges |
| Reports | Full weekly/monthly digest, PDF export |
| Ads | None |
| Offline | Core logging works offline |
| Support | Standard support (email, in-app) |

**Annual discount:** $79.99/year = $6.67/month (33% savings). Prominently displayed during upgrade flow.

### Pro Tier -- $19.99/month or $159.99/year

**Purpose:** Revenue from coaches, trainers, and power users managing multiple people.

| Feature Area | Pro Unlocks (everything in Premium, plus) |
|-------------|------------------------------------------|
| Tenant management | Manage up to 50 people across multiple tenants |
| Coach dashboard | Unified view of all client progress, compliance, and goals |
| Program assignment | Create workout programs and meal plans, assign to multiple clients at once |
| Client onboarding | Custom invite flow branded with coach's name |
| Advanced analytics | Client retention metrics, compliance rates, progress comparisons across clients |
| Data export | Bulk export client data for reporting |
| Priority support | Priority email + chat support |
| API access (Phase 3) | Integrate with external tools (calendar, invoicing, etc.) |

---

## Pricing Strategy Rationale

### Why $9.99/month

- **Market alignment:** Most fitness/habit apps charge $4.99--$14.99/month. $9.99 sits at the median.
- **Value comparison:** Users currently spend $15--25/month across 2--3 separate apps. $9.99 for an all-in-one is a clear save.
- **Psychological pricing:** Below the $10 threshold, which is a common mental boundary for app subscriptions.
- **Annual hook:** $79.99/year ($6.67/month) creates urgency and increases LTV.

### Why $19.99/month for Pro

- **B2B pricing model:** Coaches treat this as a business expense, not personal spending. $19.99 is very competitive vs. Trainerize ($50/month) and similar coach-facing tools.
- **Client value:** A coach charging clients $150/month can justify a $19.99 tool subscription easily.
- **Multiplier effect:** Each Pro user brings 5--50 clients onto the platform (who may upgrade to Premium independently).

### Price Sensitivity Considerations

- Industry data: most users abandon at $15+/month for a single app
- GoalifyNow Premium at $9.99 stays below this threshold
- Annual billing (38% conversion rate industry average) improves unit economics significantly
- 14-day free trial of Premium for all new users reduces friction

---

## Revenue Projections Framework

### Assumptions (Conservative)

| Metric | Year 1 | Year 2 | Year 3 |
|--------|--------|--------|--------|
| Total registered users | 50,000 | 200,000 | 500,000 |
| Free tier % | 70% | 65% | 60% |
| Premium tier % | 25% | 28% | 30% |
| Pro tier % | 5% | 7% | 10% |
| Monthly churn (Premium) | 8% | 6% | 5% |
| Annual billing % | 30% | 35% | 40% |

### Revenue Estimates

| Revenue Stream | Year 1 | Year 2 | Year 3 |
|---------------|--------|--------|--------|
| Premium subscriptions (monthly) | ~$87K | ~$470K | ~$1.44M |
| Premium subscriptions (annual) | ~$30K | ~$186K | ~$640K |
| Pro subscriptions (monthly) | ~$42K | ~$252K | ~$960K |
| Pro subscriptions (annual) | ~$12K | ~$84K | ~$320K |
| **Total ARR** | **~$171K** | **~$992K** | **~$3.36M** |

*These are illustrative projections based on industry benchmarks, not guarantees.*

### Key Revenue Levers

1. **Free-to-Premium conversion:** Every 1% improvement in conversion = significant ARR impact
2. **Churn reduction:** Reducing monthly churn from 8% to 5% increases LTV by 60%
3. **Annual billing adoption:** Higher annual % = more predictable revenue + lower churn
4. **Pro tier expansion:** Each Pro coach brings 5--50 client accounts (organic growth)
5. **Couple/group pricing:** Two Premium users in a tenant = 2x revenue per household

---

## Retention Strategy

The #1 business metric is **30-day retention rate**. Industry average: 48%. GoalifyNow target: 65%+.

### Anti-Churn Design Principles

#### 1. Multi-Hook Engagement

A user who only tracks workouts has one reason to open the app. A user who tracks workouts + logs meals + has a running goal + shares with their partner has four reasons. The more modules engaged, the stickier the user.

**Tactic:** Onboarding encourages activation of 2+ modules. Weekly prompts suggest trying a new module: "You've been crushing your workouts. Want to see how your nutrition affects your gains?"

#### 2. Social Lock-In

Users with a tenant connection churn at approximately half the rate of solo users (industry data from Strava and social fitness apps). Once your partner, friend, or coach is in the system, leaving means losing shared context.

**Tactic:** Referral incentive (free Premium month) + onboarding prompt to invite someone within the first week.

#### 3. Progress Investment

Users who have 3+ months of progress photos, workout history, and weight trends develop emotional attachment to their data. Switching cost becomes high because the history cannot be exported to a competing app in a meaningful format.

**Tactic:** Weekly "look how far you've come" notifications with progress comparisons. Monthly automated summaries.

#### 4. Adaptive Goals Over Punitive Streaks

Traditional streak systems punish missed days, creating guilt and eventual abandonment. GoalifyNow uses grace days (1--2 per streak) and adaptive goals that adjust based on actual performance.

**Tactic:** "You missed yesterday's workout -- no worries, your streak has a 1-day grace period. Get back on track today!"

#### 5. Win-Back Campaigns

For users who go inactive:
- Day 3 of inactivity: gentle push notification ("Your goals are waiting for you")
- Day 7: email with their progress summary ("You've logged 47 workouts this year -- don't stop now")
- Day 14: "Your partner/friend is still going strong" (if in a tenant)
- Day 30: offer a free Premium week to re-engage

---

## Additional Revenue Opportunities (Phase 3+)

| Opportunity | Description | Estimated Impact |
|-------------|-------------|-----------------|
| **Marketplace commission** | Take 15--20% commission on community-sold workout programs and meal plans | Medium |
| **Enterprise/B2B** | Corporate wellness packages ($5/employee/month) | High |
| **In-app purchases** | Cosmetic badges, custom themes, profile customization | Low |
| **Affiliate partnerships** | Supplement brands, fitness equipment, race registrations | Medium |
| **Data insights (anonymized)** | Aggregate fitness trend reports for health industry | Low (long-term) |
| **Sponsored challenges** | Brands sponsor community challenges (e.g., "Nike 30-Day Run Challenge") | Medium |

---

## Billing & Payment Implementation

### Payment Methods
- App Store / Play Store for mobile (mandatory for iOS/Android native)
- Stripe for web subscriptions (lower commission than app stores)
- PayPal support for markets where credit card penetration is low

### Billing Features
- Subscription management page (upgrade, downgrade, cancel)
- Invoice generation for Pro tier (coaches may need receipts for tax)
- Grace period on failed payments (3 days retry before downgrade)
- Proration on mid-cycle tier changes
- Gift subscriptions (buy Premium for a friend)

### Free Trial Strategy
- 14-day Premium trial for all new users (no credit card required)
- Trial converts to Free tier if not upgraded (no surprise charges)
- Trial extension: one-time 7-day extension offered at day 12 if user has been active
- Coach trial: 30-day Pro trial with up to 3 clients
