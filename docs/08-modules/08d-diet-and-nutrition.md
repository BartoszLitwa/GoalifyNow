# Module 08d -- Diet & Nutrition

## Purpose

The Diet & Nutrition module provides comprehensive meal logging, macro/micro-nutrient tracking, meal planning, and recipe management. It replaces dedicated apps like MyFitnessPal, MacroFactor, and Cronometer while integrating with GoalifyNow's training and goal modules to provide context-aware nutrition guidance.

---

## Feature List

### Meal Logging

| Feature | Phase | Description |
|---------|-------|-------------|
| Meal logging by search | MVP | Search a food database to find and log food items with nutrition data |
| Barcode scanner | MVP | Scan packaged food barcodes to auto-populate nutrition information |
| Meal categories | MVP | Categorize entries as Breakfast, Lunch, Dinner, Snack, or custom meal names |
| Portion adjustment | MVP | Adjust serving size with real-time macro recalculation (grams, cups, pieces, servings) |
| Quick-add calories | MVP | Fast entry: just log a calorie number without selecting specific foods |
| Recent foods | MVP | Quick-select from the 20 most recently logged food items |
| Favorite foods | MVP | Save frequently eaten foods for one-tap logging |
| Meal templates | MVP | Save entire meals as templates (e.g., "My usual breakfast") and log with one tap |
| Copy previous day's meals | MVP | Duplicate all meals from a previous day to today |
| Meal editing | MVP | Edit any logged meal at any time (change items, adjust portions, delete items) |
| Photo meal logging | Phase 2 | Take a photo and AI identifies food items with estimated portions |
| Voice meal logging | Phase 3 | Describe a meal verbally and AI parses it into food items |

### Food Database

| Feature | Phase | Description |
|---------|-------|-------------|
| Comprehensive food database | MVP | 500,000+ food items with verified nutrition data |
| Branded food items | MVP | Packaged/branded products with barcode support |
| Generic food items | MVP | Whole foods, raw ingredients, common preparations |
| Restaurant menu items | Phase 3 | Nutrition data for popular restaurant chain menu items |
| User-submitted foods | Phase 2 | Users can submit new food items (moderated for accuracy) |
| Regional food databases | Phase 2 | Country-specific food items and brands (US, UK, EU, Australia) |

### Macro & Micro Tracking

| Feature | Phase | Description |
|---------|-------|-------------|
| Daily macro targets | MVP | Set targets for calories, protein, carbohydrates, and fat |
| Macro progress bars | MVP | Visual progress toward each daily macro target |
| Remaining macros display | MVP | Show remaining calories/macros for the day after each meal |
| Macro breakdown by meal | MVP | See macro distribution across meals |
| Micro-nutrient tracking | Phase 2 | Track fiber, sodium, sugar, vitamins, and minerals |
| Macro target profiles | MVP | Pre-set profiles: balanced, high-protein, keto, low-carb, custom |
| Training-day vs rest-day macros | Phase 2 | Different macro targets for training days and rest days |
| Adaptive macro adjustment | Phase 3 | AI adjusts macro targets based on weight trends and activity level |

### Recipes

| Feature | Phase | Description |
|---------|-------|-------------|
| Recipe creation | MVP | Define a recipe with ingredients, portions, and servings |
| Per-serving nutrition | MVP | Auto-calculate nutrition per serving from ingredient list |
| Recipe scaling | MVP | Scale recipes up or down by number of servings |
| Recipe search | MVP | Search saved recipes by name or ingredient |
| Recipe categories | MVP | Organize recipes by meal type, cuisine, or dietary tag |
| Recipe sharing | Phase 2 | Share recipes with tenant members |
| Recipe import from URL | Phase 3 | Paste a recipe URL and auto-extract ingredients and nutrition |

### Meal Plans

| Feature | Phase | Description |
|---------|-------|-------------|
| Weekly meal plan builder | Phase 2 | Plan meals for each day of the week |
| Meal plan templates | Phase 2 | Save and reuse weekly meal plan templates |
| Meal plan assignment (coach) | Phase 2 | Pro tier: assign meal plans to clients |
| Grocery list generation | Phase 2 | Auto-generate a shopping list from a meal plan |
| Meal plan macros preview | Phase 2 | See daily macro totals for the planned week before committing |

### Hydration

| Feature | Phase | Description |
|---------|-------|-------------|
| Water intake logging | MVP | Log water consumed in glasses, ml, or oz |
| Daily water target | MVP | Set a daily hydration goal (default: 2L, adjustable) |
| Water reminders | MVP | Configurable reminders throughout the day to drink water |
| Hydration tracking widget | MVP | Quick-add water from the dashboard without opening the nutrition module |

### Dietary Preferences

| Feature | Phase | Description |
|---------|-------|-------------|
| Dietary preference profile | MVP | Set preferences: vegan, vegetarian, pescatarian, keto, paleo, gluten-free, dairy-free, halal, kosher |
| Allergen warnings | Phase 2 | Flag foods containing user-specified allergens |
| Preference-filtered search | MVP | Food search results respect dietary preferences (hide non-matching items or flag them) |

---

## User Stories

### Meal Logging
- As a user, I want to scan a barcode on my protein bar and have it instantly logged with all nutrition data.
- As a user, I want to search "chicken breast grilled" and log it with the correct portion size in grams.
- As a user, I want to save my go-to breakfast as a template so I can log it in one tap every morning.
- As a user, I want to copy yesterday's meals to today when I eat roughly the same thing (meal prep days).
- As a user, I want to quickly add 200 calories for a snack I can't find in the database rather than skip logging.

### Macro Tracking
- As a user, I want to see how much protein I have left to eat today after logging lunch.
- As a user, I want to set different macro targets for training days (higher carbs) and rest days (lower carbs).
- As a user, I want to see a weekly average of my macros, not just daily, because some days I overeat and some I undereat.
- As a user on keto, I want a pre-set macro profile that caps carbs at 30g and adjusts fat accordingly.

### Recipes
- As a user, I want to add my chicken stir-fry recipe with all ingredients and see the per-serving macros automatically.
- As a user, I want to scale my recipe from 2 servings to 4 servings for meal prep and see the adjusted ingredient quantities.
- As a user, I want to log "1 serving of my chicken stir-fry" as a single item in my meal.

### Meal Plans (Phase 2)
- As a user, I want to plan all my meals for the week on Sunday so I know what to cook and buy.
- As a user, I want to generate a grocery list from my meal plan so I can shop efficiently.
- As a coach, I want to create a meal plan template and assign it to 5 clients who have similar goals.

### Social (Phase 2)
- As a couple sharing a tenant, I want to log a dinner recipe once and have it apply to both of us.
- As a tenant member, I want to share a recipe I created with my partner.

---

## Acceptance Criteria

### Meal Logging
- Barcode scan must return results within 3 seconds or show "Item not found" with manual add option
- Food search must return results as the user types (debounced, after 2+ characters)
- Portion size changes must update macro values in real-time (no need to save and refresh)
- Deleting a food item from a meal updates daily totals immediately
- A meal can contain unlimited food items

### Macro Tracking
- Daily totals update within 2 seconds of any meal change
- Macro progress bars use color coding: green (on track), yellow (within 10% of target), red (exceeded or significantly under)
- Weekly average view calculates mean daily intake across the selected 7-day period
- Macro targets are editable at any time without affecting historical data

### Food Database
- Database must support search in the user's language (English at MVP; German, Polish in Phase 2)
- Barcode coverage target: 80%+ of packaged items in US, UK, and EU markets
- All user-submitted food items require admin review before appearing in global search (Phase 2)
- Verified items are flagged as "verified" in search results

### Recipes
- Recipe nutrition is auto-calculated from the sum of ingredient nutrition divided by serving count
- Changing serving count updates all ingredient quantities proportionally
- A recipe can be logged as a food item in any meal
- Editing a recipe does NOT retroactively change previously logged meals that used the old version

---

## Data Concepts

| Concept | Description |
|---------|-------------|
| **Meal** | A collection of food items logged at a specific time, categorized by type (Breakfast, Lunch, Dinner, Snack) |
| **Food Item** | An entry from the food database with nutrition data (calories, protein, carbs, fat, and optionally micronutrients) |
| **Serving** | A specific quantity of a food item (e.g., 150g, 1 cup, 2 slices) |
| **Daily Nutrition** | Aggregated macro/micro totals for a single day across all meals |
| **Macro Target** | A user's daily target for calories, protein, carbs, and fat, optionally varying by day type |
| **Recipe** | A user-created combination of food items with serving count and auto-calculated per-serving nutrition |
| **Meal Template** | A saved meal (list of food items with portions) for quick one-tap logging |
| **Meal Plan** | A weekly schedule of planned meals with associated recipes/food items |
| **Water Entry** | A hydration log entry with amount and timestamp |
| **Dietary Profile** | User preferences including dietary restrictions, allergens, and macro target profiles |

---

## Integration Points

| Integrates With | How |
|----------------|-----|
| **Goals & Habits** | Daily nutrition adherence feeds into nutrition goals (e.g., "Eat 150g protein daily"); hydration habits track automatically |
| **Training & Workouts** | Training day awareness adjusts macro display; post-workout meal suggestions (Phase 3) |
| **Running & Cardio** | Long run / high cardio days trigger adjusted carbohydrate recommendations |
| **Progress & Analytics** | Nutrition trends (calorie, protein averages) appear in analytics; weight trend correlated with calorie intake |
| **Social & Accountability** | Recipes shareable within tenant; meal plans assignable by coaches; shared meals for couples |
| **Preparation & Planning** | Nutrition periodization for race prep (carb loading, taper nutrition) |
