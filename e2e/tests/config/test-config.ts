export const TestConfig = {
  baseUrl: process.env.BASE_URL || 'http://localhost:4200',
  apiUrl: process.env.API_URL || 'http://localhost:5090',
  credentials: {
    demo: {
      email: 'demo@goalifynow.local',
      password: 'Demo123!',
    },
  },
  timeouts: {
    navigation: 10_000,
    api: 5_000,
    animation: 500,
  },
};
