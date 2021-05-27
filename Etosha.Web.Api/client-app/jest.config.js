module.exports = {
  setupFilesAfterEnv: [
    '<rootDir>/jest/setupTests.js'
  ],
  testMatch: ['**/*.spec.ts'],
  collectCoverage: true,
  collectCoverageFrom: ['src/**/*.ts'],
  coveragePathIgnorePatterns: [
    '/node_modules/',
    '/jest/',
    '\.module\.ts$',
    '/environments/',
    '/src/main.ts',
    '/src/polyfills.ts',
    '/src/app/app.routing.ts',
    '/src/app/stocks/ticker/stocks-ticker.component.ts',
    '/src/app/stocks/ticker/stocks-ticker-item.component.ts',
  ],
  coverageReporters: [
    'text-summary',
    'html',
    'cobertura'
  ],
  reporters: [
    'default',
    ['jest-junit', { output: 'coverage/junit.xml' }]
  ],
  transformIgnorePatterns: ['^.+\\.js$']
};
