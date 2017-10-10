(function(window) {
  window.__env = window.__env || {};
  Object.assign(window.__env, {
    api_url: '<api_url_for_jenkins>',
    reCaptchaSiteKey: '<reCaptchaSiteKey_for_jenkins>',
    requireHttps: true,
    tokenCookieDurationMinutes: 20160
  });
})(this);
