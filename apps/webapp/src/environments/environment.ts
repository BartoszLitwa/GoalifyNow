import { AppEnvironment } from './environment.model';

const resolveHost = (): string => {
  if (typeof window === 'undefined') {
    return 'localhost';
  }

  const query = new URLSearchParams(window.location.search).get('apiHost');
  if (query) {
    return query.replace(/^https?:\/\//i, '').replace(/:\d+$/, '');
  }

  return window.location.hostname || 'localhost';
};

const host = resolveHost();

export const environment: AppEnvironment = {
  production: false,
  appName: 'GoalifyNow',
  apiBaseUrl: `http://${host}:5090`,
  stripePublishableKey: 'pk_test_change_me'
};
