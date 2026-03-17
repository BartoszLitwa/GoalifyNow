# Module 08f -- Social & Accountability

## Purpose

The Social & Accountability module is GoalifyNow's primary differentiator and retention moat. It provides a multi-tenant collaboration system where users share spaces with partners, friends, family, or coaches. Unlike social features in competing apps (which are feed-based and passive), GoalifyNow's social model is structural: shared goals, joint challenges, real-time visibility into each other's progress, and permission-controlled data sharing.

---

## Feature List

### Tenant Management

| Feature | Phase | Description |
|---------|-------|-------------|
| Tenant creation | Phase 2 | Create a named shared space (e.g., "Mike & Sarah", "Team Alpha", "Carlos's Clients") |
| Tenant types | Phase 2 | Choose a type that sets default permissions: Partner/Couple, Friend Group, Coach/Client |
| Invite by email | Phase 2 | Send an email invitation to join a tenant |
| Invite by link | Phase 2 | Generate a shareable invite link (single-use or multi-use) |
| Invite by QR code | Phase 2 | Generate a QR code for in-person invitations |
| Tenant member list | Phase 2 | View all members with their roles and status |
| Remove member | Phase 2 | Tenant creator can remove a member (with confirmation) |
| Leave tenant | Phase 2 | Any member can leave a tenant voluntarily |
| Multiple tenants | Phase 2 | A user can belong to multiple tenants (e.g., one with partner, one with gym buddy, one with coach) |
| Tenant limits | Phase 2 | Free: join tenants (view-only); Premium: create tenants with up to 10 members; Pro: up to 50 members |

### Permission Controls

| Feature | Phase | Description |
|---------|-------|-------------|
| Per-module permissions | Phase 2 | For each tenant member, control visibility per module: Full / Summary / Hidden |
| Default permission profiles | Phase 2 | Tenant types set sensible defaults (e.g., Coach/Client defaults to full visibility for coach) |
| Custom permission override | Phase 2 | Override defaults for specific members or specific modules |
| Permission levels | Phase 2 | Full (see all data), Summary (see activity without detail, e.g., "Completed a workout" without sets/reps), Hidden (nothing visible) |
| Self-service privacy | Phase 2 | Each user controls what THEY share, not what they can see of others |

**Permission matrix example:**

| Module | Partner Tenant (default) | Friend Group (default) | Coach/Client (default) |
|--------|------------------------|----------------------|----------------------|
| Goals & Habits | Full | Summary | Full |
| Training & Workouts | Full | Summary | Full |
| Running & Cardio | Full | Summary | Full |
| Diet & Nutrition | Summary | Hidden | Full |
| Progress (photos) | Hidden | Hidden | Full (opt-in) |
| Progress (measurements) | Hidden | Hidden | Full |
| Body Weight | Summary | Hidden | Full |

### Activity Feed

| Feature | Phase | Description |
|---------|-------|-------------|
| Tenant activity feed | Phase 2 | Chronological feed of tenant members' activities (respecting permissions) |
| Activity types in feed | Phase 2 | Workouts completed, goals achieved, milestones hit, streaks maintained, runs completed, challenges updated |
| Cheering / kudos | Phase 2 | React to a feed item with a cheer (similar to Strava's kudos) |
| Activity detail drill-down | Phase 2 | Tap a feed item to see details (if permission allows) |
| Feed filtering | Phase 2 | Filter feed by member or activity type |

### Challenges

| Feature | Phase | Description |
|---------|-------|-------------|
| Challenge creation | Phase 2 | Create a challenge with name, rules, duration, and scoring method |
| Challenge types | Phase 2 | Workout streak, distance (running/cycling), volume (total weight lifted), habit completion, custom metric |
| Challenge templates | Phase 2 | Pre-built templates: "30-Day Workout Challenge", "10K Steps Daily", "Protein Goal for a Month" |
| Challenge invitation | Phase 2 | Invite specific tenant members to join a challenge |
| Challenge leaderboard | Phase 2 | Real-time ranked standings within a challenge |
| Challenge progress tracking | Phase 2 | Automatic tracking based on logged data (not self-reported) |
| Challenge notifications | Phase 2 | Alerts when falling behind, when someone overtakes you, when milestones are hit |
| Challenge completion | Phase 2 | Final standings, personal stats, badge awarded for completion |
| Challenge rematch | Phase 2 | One-tap rematch to restart the same challenge |
| Public community challenges | Phase 3 | Join challenges with the broader GoalifyNow user base |
| Sponsored challenges | Phase 3 | Brand-sponsored challenges with prizes |

### Motivational Features

| Feature | Phase | Description |
|---------|-------|-------------|
| Nudges | Phase 2 | Send a motivational push notification to a tenant member ("Hey, have you worked out today?") |
| Achievements & badges | Phase 2 | Unlock badges for milestones: workout count, streak length, challenge wins, goals completed |
| Badge gallery | Phase 2 | View all earned and available badges |
| Streak visibility | Phase 2 | See each tenant member's active streaks (mutual accountability) |
| "Look how far you've come" | Phase 2 | Weekly automated notification showing personal progress highlights |
| Celebration notifications | Phase 2 | When a tenant member achieves something notable, all members receive a celebration notification |

### Coach-Specific Features (Pro Tier)

| Feature | Phase | Description |
|---------|-------|-------------|
| Client dashboard | Phase 2 | Unified view of all clients' key metrics: compliance, progress, recent activity |
| Program assignment | Phase 2 | Assign workout programs to clients from the coach dashboard |
| Meal plan assignment | Phase 2 | Assign meal plans to clients from the coach dashboard |
| Client progress reports | Phase 2 | Generate per-client progress reports for check-in meetings |
| Client groups | Phase 2 | Group clients (e.g., "Weight Loss Group", "Strength Group") for batch communication |
| Bulk messaging | Phase 3 | Send a message to all clients or a client group |
| Client retention metrics | Phase 3 | Track which clients are at risk of disengaging |

---

## User Stories

### Tenants
- As a user, I want to create a shared space with my partner so we can see each other's fitness activities.
- As a user, I want to invite my gym buddy to my tenant via a link I send on WhatsApp.
- As a user, I want to belong to multiple tenants: one with my partner, one with my running group.
- As a coach, I want to create a tenant for my clients where I can see their data but they can only see their own.

### Permissions
- As a user, I want to share my workout data with my partner but keep my body measurements private.
- As a user in a friend group tenant, I want others to see that I worked out today without seeing my exact weights.
- As a coach's client, I want to give my coach full visibility into everything so they can guide me effectively.
- As a user, I want to change my sharing permissions at any time without needing the tenant creator's approval.

### Activity Feed
- As a tenant member, I want to see when my partner completed a workout so I can cheer them on.
- As a tenant member, I want to send a "kudos" reaction when my friend hits a new personal record.
- As a user, I want to filter the feed to only show my partner's activities (not the entire friend group).

### Challenges
- As a user, I want to challenge my partner to "who can maintain a 30-day workout streak."
- As a user, I want the challenge to track our workouts automatically -- no honor-system check-ins.
- As a user, I want to see a live leaderboard during a challenge so I know if I'm ahead or behind.
- As a user, I want to receive a notification when my friend overtakes me in a challenge ("Dan just passed you with 12 workouts this month!").
- As a user, I want to create a custom challenge with my own rules (e.g., "Most kilometers run in February").

### Coach Features
- As a coach, I want a single dashboard showing all 15 of my clients' workout completion rates this week.
- As a coach, I want to assign the same workout program to 5 clients in one action.
- As a coach, I want to generate a progress report for a client before our monthly check-in.
- As a client, I want to see only my own data in the app -- not other clients of my coach.

### Motivation
- As a user, I want to send a friendly nudge to my partner on a rest day that's turned into a 3-day break.
- As a user, I want to earn a badge when I complete 100 workouts and have it displayed on my profile.
- As a user, I want to receive a "look how far you've come" notification comparing this month to last month.

---

## Acceptance Criteria

### Tenant Creation & Invites
- A tenant must have a name and type
- Invite links expire after 7 days (configurable by creator)
- An invited user who doesn't have a GoalifyNow account is prompted to create one before joining
- A user can belong to a maximum of 5 tenants on Premium, 20 on Pro
- Removing a member from a tenant does not delete their account or data -- only the shared visibility

### Permissions
- Permission changes take effect immediately for all future feed items and data views
- Historical data already visible in the feed before a permission change remains visible (no retroactive hiding)
- A user's permission setting is stored on their profile within the tenant, not on each individual data point
- The "Hidden" permission level makes the module completely invisible for that tenant (no feed items, no data, no acknowledgment that data exists)

### Challenges
- Challenge progress is calculated from actual logged data, not self-reported
- A challenge can have 2--50 participants
- Challenge leaderboard updates within 5 minutes of any participant's activity
- If a participant leaves a tenant, they are removed from active challenges (their data remains in completed challenges)
- Challenge completion awards a badge to all participants who met the minimum criteria (not just the winner)

### Activity Feed
- Feed items appear within 2 minutes of the source activity being logged
- Feed is reverse-chronological by default
- Kudos/cheers are anonymous by default (just a count) unless the user taps to see who cheered

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Tenant** | A shared space with a name, type, creator, and list of members with role/permission assignments |
| **Tenant Member** | A user within a tenant with a role (Creator, Member, Coach, Client) and per-module permissions |
| **Permission** | A per-module visibility level (Full, Summary, Hidden) set by each user for each tenant they belong to |
| **Activity Feed Item** | A feed entry representing a user action (workout, goal, milestone, etc.) visible to tenant members per permissions |
| **Challenge** | A time-bound competition within a tenant with rules, participants, scoring, and a leaderboard |
| **Challenge Entry** | A participant's standing in a challenge, including current score and rank |
| **Badge** | An earned achievement with name, description, icon, and unlock criteria |
| **Nudge** | A push notification sent from one tenant member to another as a motivational prompt |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Goals & Habits** | Goals can be shared or joint within a tenant; streaks visible to members; goal milestones appear in feed |
| **Training & Workouts** | Completed workouts appear in feed; workout templates shareable; programs assignable by coaches |
| **Running & Cardio** | Run routes and race results shareable in feed; distance challenges use real run data |
| **Diet & Nutrition** | Recipes shareable; meal plans assignable by coaches; couple meals logged jointly |
| **Progress & Analytics** | Weekly digests optionally shared; coach reports generated from client analytics |
| **Preparation & Planning** | Shared race goals with preparation visibility; joint event participation |
