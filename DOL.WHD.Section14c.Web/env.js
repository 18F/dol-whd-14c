// This file is only used for local developement. If you edit anything besides existing environment variables, you must also edit ./src/deploy/env.js for deployment
(function(window) {
    window.__env = window.__env || {};
    Object.assign(window.__env, {
        api_url: 'https://localhost:44399',
        reCaptchaSiteKey: '6LeqeggUAAAAALC5zT4OHbDJk9gHNT0GGZbJMOnG',
        requireHttps: true,
        tokenCookieDurationMinutes: 20160
    });
})(this);