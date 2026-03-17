# Module 08g -- Preparation & Planning

## Purpose

The Preparation & Planning module helps users prepare for specific events: races, competitions, fitness photoshoots, personal milestones, or any target date that requires structured preparation. It ties together training programs, nutrition periodization, and goal milestones into a unified timeline. This module differentiates GoalifyNow from apps that track daily activity but don't help users plan ahead.

---

## Feature List

### Event Management

| Feature | Phase | Description |
|---------|-------|-------------|
| Event creation | Phase 2 | Create a target event with name, type, date, location, and description |
| Event types | Phase 2 | Running race, cycling event, strength competition, fitness photoshoot, personal milestone, other |
| Event countdown | Phase 2 | Dashboard widget showing days remaining until the event |
| Event goal | Phase 2 | Set a specific goal for the event (e.g., "Finish under 4 hours", "Hit 150kg squat") |
| Event history | Phase 2 | View all past events with results and reflections |
| Event sharing | Phase 2 | Share an upcoming event with tenant members (joint preparation) |

### Preparation Timeline

| Feature | Phase | Description |
|---------|-------|-------------|
| Auto-generated timeline | Phase 2 | Based on event date and type, generate a preparation timeline with suggested phases |
| Training phases | Phase 2 | Define training blocks: Base, Build, Peak, Taper, Recovery |
| Phase milestones | Phase 2 | Auto-generated milestone markers at phase transitions |
| Custom timeline adjustments | Phase 2 | Modify the auto-generated timeline to fit personal needs |
| Timeline view | Phase 2 | Visual horizontal timeline showing phases, milestones, and current position |

### Training Block Planning

| Feature | Phase | Description |
|---------|-------|-------------|
| Training block definition | Phase 2 | Define the focus and intensity of each training phase |
| Block-specific goals | Phase 2 | Set goals per training block (e.g., "Build phase: increase weekly mileage to 60km") |
| Training plan linking | Phase 2 | Link a running plan or workout program to a specific training block |
| Block completion tracking | Phase 2 | Track actual vs planned training volume per block |
| Deload/recovery blocks | Phase 2 | Built-in support for planned recovery periods |

### Race-Specific Features

| Feature | Phase | Description |
|---------|-------|-------------|
| Race-day checklist | Phase 2 | Customizable checklist for race day (gear, nutrition, logistics, warm-up) |
| Pre-built race checklists | Phase 2 | Templates for common race distances (5K, 10K, half marathon, marathon) |
| Tapering plans | Phase 2 | Pre-built taper strategies (2-week taper, 3-week taper) linked to the event countdown |
| Race nutrition plan | Phase 2 | Plan race-day nutrition: pre-race meal, during-race fueling, post-race recovery |
| Carb loading protocol | Phase 2 | Structured carb-loading schedule for the final days before a race |
| Weather preparation | Phase 3 | Weather forecast for race location and date with clothing/hydration recommendations |

### Equipment Tracking

| Feature | Phase | Description |
|---------|-------|-------------|
| Equipment inventory | Phase 2 | Track shoes, gear, and equipment with purchase date and mileage/usage |
| Shoe mileage tracking | Phase 2 | Auto-increment shoe mileage from GPS-tracked runs |
| Replacement reminders | Phase 2 | Notification when shoes reach a configurable mileage threshold (default: 500km) |
| Equipment notes | Phase 2 | Log notes about gear condition, comfort, and performance |
| Race-day gear selection | Phase 2 | Mark which gear items are selected for race day on the checklist |

### Post-Event Review

| Feature | Phase | Description |
|---------|-------|-------------|
| Event result logging | Phase 2 | Log actual result: finish time, placement, personal assessment |
| Structured reflection | Phase 2 | Guided reflection: What went well? What to improve? Lessons learned? |
| Goal achievement review | Phase 2 | Compare actual result to the event goal |
| Next event planning | Phase 2 | Prompt to plan the next event based on lessons learned |
| Season history | Phase 2 | Chronological view of all events in a season with trend analysis |

### Advanced Planning

| Feature | Phase | Description |
|---------|-------|-------------|
| Nutrition periodization | Phase 3 | Adjust macro targets automatically based on the current training phase |
| Multi-event season planning | Phase 3 | Plan an entire season of events with A-races, B-races, and recovery weeks |
| Peaking strategy | Phase 3 | AI-suggested peaking strategies based on training data and event date |
| Cross-event analytics | Phase 3 | Compare preparation and results across multiple events to identify patterns |

---

## User Stories

### Event Management
- As a runner, I want to add my upcoming half marathon to GoalifyNow with the date and my goal time so I have a clear target.
- As a user, I want to see a countdown to my event on my dashboard so I always know how much time I have left.
- As a user, I want to share my upcoming race with my tenant so my partner can see what I'm training for.
- As a couple, we want to add the same 10K race and both track our preparation toward it.

### Preparation Timeline
- As a marathon trainee, I want GoalifyNow to generate an 18-week preparation timeline with base, build, peak, and taper phases.
- As a user, I want to see a visual timeline showing where I am in my preparation and what's coming next.
- As a user, I want to customize the timeline if the auto-generated one doesn't fit my schedule.

### Training Blocks
- As a runner, I want to link my "base building" running plan to the "Base" phase of my marathon preparation.
- As a user, I want to see whether my actual training volume in the current block matches the planned volume.
- As a user, I want a deload week automatically scheduled between my Build and Peak phases.

### Race Day
- As a runner, I want a race-day checklist so I don't forget anything (race bib, gels, watch, sunscreen, etc.).
- As a marathon runner, I want a carb-loading schedule for the 3 days before the race with specific daily carb targets.
- As a runner, I want to plan my during-race fueling (gel every 45 minutes, water at every station).

### Equipment
- As a runner, I want to track the mileage on my running shoes so I know when to replace them.
- As a user, I want to be notified when my shoes hit 500km so I can order a new pair before they wear out.
- As a runner, I want to mark which shoes I'm using for race day on my checklist.

### Post-Event
- As a runner, I want to log my race result (1:48:32, 156th overall) immediately after the race.
- As a user, I want to reflect on what went well and what to improve in a structured format.
- As a user, I want to compare my race result to my goal time and see if my preparation was sufficient.
- As a user, after completing my half marathon, I want GoalifyNow to suggest "Ready for a full marathon?" as a next event.

---

## Acceptance Criteria

### Event Creation
- An event must have a name and a future date
- Event type determines which preparation features are available (e.g., running race enables tapering, carb loading)
- Multiple users in a tenant can link the same external event (shared preparation)
- Past events with results are preserved in history indefinitely

### Preparation Timeline
- Auto-generated timelines are based on event type and weeks until event:
  - 4--8 weeks: 2 phases (Build + Taper)
  - 8--16 weeks: 3 phases (Base + Build + Taper)
  - 16+ weeks: 4 phases (Base + Build + Peak + Taper)
- Phase transition dates are editable by the user
- Each phase can have a linked training plan or workout program
- Timeline visualization shows completed phases, current phase (highlighted), and upcoming phases

### Race-Day Checklist
- Checklists are editable up to and including race day
- Pre-built templates provide a starting point; all items can be added, removed, or modified
- Checklist items can be marked as "packed" and "done"
- Checklists are accessible offline (for race-day situations with poor connectivity)

### Equipment Tracking
- Shoe mileage auto-increments when a GPS-tracked run is logged and the user has assigned active shoes
- If no shoes are assigned, the system prompts the user to select shoes after their run
- Replacement reminder threshold is configurable per shoe (default 500km / 300mi)
- Equipment entries persist even after an item is marked as "retired"

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Event** | A target occasion with name, type, date, location, goal, and result (when complete) |
| **Preparation Timeline** | A sequence of training phases leading up to an event, with date ranges and milestones |
| **Training Phase** | A named block within a timeline (Base, Build, Peak, Taper, Recovery) with goals and linked plans |
| **Race-Day Checklist** | A list of items to prepare and bring for event day, with check-off status |
| **Checklist Item** | A single entry on a checklist with name, category (gear, nutrition, logistics), and status |
| **Equipment** | A tracked piece of gear with name, type, purchase date, and usage metrics (mileage, session count) |
| **Post-Event Review** | A structured reflection on an event including result, achievements, lessons, and next steps |
| **Season** | A collection of events within a time period (e.g., "2026 Race Season") with periodization |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Goals & Habits** | Event goals link to the goals module; preparation milestones contribute to goal progress |
| **Training & Workouts** | Workout programs link to training blocks; training volume tracked per phase |
| **Running & Cardio** | Running plans link to preparation timelines; shoe mileage auto-tracked from runs; race results logged |
| **Diet & Nutrition** | Carb loading and race nutrition planned here, executed in nutrition module; nutrition periodization adjusts targets per phase |
| **Progress & Analytics** | Preparation progress visualized; post-event analytics show effectiveness of training; cross-event trends |
| **Social & Accountability** | Shared events within tenants; joint preparation visible; race results shareable in feed |
