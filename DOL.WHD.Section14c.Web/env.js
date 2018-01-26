// This file is only used for local developement. If you edit anything besides existing environment variables, you must also edit ./src/deploy/env.js for deployment
(function(window) {
  window.__env = window.__env || {};
  Object.assign(window.__env, {
    api_url: 'https://localhost:44399',
    requireHttps: true,
    tokenCookieDurationMinutes: 20160,
    allowedFileTypes: '["pdf", "jpg", "JPG", "jpeg", "JPEG", "png", "PNG", "csv", "CSV", "PDF"]',
    helpEmailAddress:'14c-help@dol.gov',
    helpPhoneNumber:'1-800-DOL-HELP'
  });
})(this);
