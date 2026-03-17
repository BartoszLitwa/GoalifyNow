import { AppEnvironment } from './environment.model';

export const environment: AppEnvironment = {
  production: true,
  appName: 'GoalifyNow',
  apiBaseUrl: 'https://api.goalifynow.com',
  stripePublishableKey: '{{STRIPE_PUBLISHABLE_KEY}}'
};
