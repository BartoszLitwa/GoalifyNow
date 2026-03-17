import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-landing',
  imports: [RouterLink],
  templateUrl: './landing.page.html',
  styleUrl: './landing.page.scss',
})
export class LandingPage {
  readonly modules = [
    { name: 'Goals', icon: 'pi-flag' },
    { name: 'Training', icon: 'pi-bolt' },
    { name: 'Running', icon: 'pi-directions-run' },
    { name: 'Nutrition', icon: 'pi-apple' },
    { name: 'Progress', icon: 'pi-chart-line' },
    { name: 'Social', icon: 'pi-users' },
    { name: 'Planning', icon: 'pi-calendar' },
  ];

  readonly competitorApps = ['Strava', 'MyFitnessPal', 'Strong', 'Habitica', 'Noom'];

  readonly features = [
    {
      icon: 'pi-flag',
      title: 'Goals & Habits',
      description: 'Set measurable goals, build daily habits, and track streaks with grace days that forgive missed days instead of punishing you.',
      bg: '#eef2ff',
      color: '#4f46e5',
    },
    {
      icon: 'pi-bolt',
      title: 'Training & Workouts',
      description: 'Log sets, reps, and weight. Follow structured programs. Track progressive overload and personal records automatically.',
      bg: '#fef3c7',
      color: '#d97706',
    },
    {
      icon: 'pi-map',
      title: 'Running & Cardio',
      description: 'GPS-tracked runs, structured plans from 5K to marathon, interval training, pace zones, and race preparation.',
      bg: '#dcfce7',
      color: '#16a34a',
    },
    {
      icon: 'pi-apple',
      title: 'Diet & Nutrition',
      description: 'Meal logging with barcode scanner, macro tracking, recipe builder, meal plans, and hydration tracking.',
      bg: '#fce7f3',
      color: '#db2777',
    },
    {
      icon: 'pi-chart-line',
      title: 'Progress & Analytics',
      description: 'Progress photos with pose guides, body measurements, smoothed weight trend charts, and exportable reports.',
      bg: '#e0e7ff',
      color: '#6366f1',
    },
    {
      icon: 'pi-users',
      title: 'Social & Accountability',
      description: 'Invite partners, friends, or clients into shared spaces. Real challenges with real data, not self-reported check-ins.',
      bg: '#f3e8ff',
      color: '#9333ea',
    },
    {
      icon: 'pi-calendar',
      title: 'Preparation & Planning',
      description: 'Plan for races, competitions, and events with timelines, training blocks, checklists, and equipment tracking.',
      bg: '#ecfdf5',
      color: '#059669',
    },
  ];

  readonly gamificationItems = [
    { icon: 'pi-bolt', title: 'Streaks with Grace Days', description: 'Miss a day without losing your streak. Grace days recharge as you maintain consistency.' },
    { icon: 'pi-trophy', title: 'Achievements & Badges', description: 'Unlock badges for milestones: 100 workouts, 30-day streaks, race completions, and more.' },
    { icon: 'pi-flag', title: 'Group Challenges', description: 'Create time-bound challenges with friends tracked by real workout and meal data.' },
    { icon: 'pi-chart-bar', title: 'Leaderboards', description: 'Compete with tenant members on workouts, distance, consistency, and more.' },
  ];

  readonly sampleBadges = [
    { emoji: '🔥', label: '30-Day Streak' },
    { emoji: '💪', label: '100 Workouts' },
    { emoji: '🏃', label: 'First 10K' },
    { emoji: '🥗', label: 'Macro Master' },
  ];

  readonly aiFeatures = [
    {
      icon: 'pi-sparkles',
      title: 'Cross-Module Insights',
      description: 'AI connects your diet, training, sleep, and goals to surface patterns you\'d never see in siloed apps.',
      example: 'Your running pace improves 8% when protein exceeds 140g the day before.',
    },
    {
      icon: 'pi-sliders-h',
      title: 'Adaptive Goals',
      description: 'Goals adjust to reality. If you miss a week, your plan recalibrates instead of marking 7 failures.',
      example: 'Adjusted your weekly target from 5 to 4 workouts based on your travel schedule.',
    },
    {
      icon: 'pi-lightbulb',
      title: 'Smart Suggestions',
      description: 'Personalized recommendations for workouts, meals, and goals based on your history and progress.',
      example: 'Based on your plateau, try adding a deload week before your next push.',
    },
  ];

  readonly plans = [
    {
      name: 'Free',
      price: '$0',
      period: '',
      description: 'Get started with core tracking features.',
      featured: false,
      cta: 'Start Free',
      features: [
        'Up to 3 active goals',
        'Unlimited workout logging',
        'Basic meal logging with barcode scanner',
        'Weight tracking with trend chart',
        'Progress photo storage',
        '3 saved workout templates',
      ],
    },
    {
      name: 'Premium',
      price: '$9.99',
      period: '/month',
      description: 'Full power for serious self-improvers.',
      featured: true,
      cta: 'Start 14-Day Free Trial',
      features: [
        'Everything in Free',
        'Unlimited goals & habits',
        'GPS run tracking & running plans',
        'Meal plans & recipe sharing',
        'Full analytics & PDF reports',
        'Social: tenants, challenges, badges',
        'Photo side-by-side comparison',
        'Offline support',
      ],
    },
    {
      name: 'Pro',
      price: '$19.99',
      period: '/month',
      description: 'For coaches and power users.',
      featured: false,
      cta: 'Start Pro Trial',
      features: [
        'Everything in Premium',
        'Manage up to 50 clients',
        'Coach dashboard',
        'Assign programs & meal plans',
        'Client progress reports',
        'Priority support',
        'API access',
      ],
    },
  ];
}
