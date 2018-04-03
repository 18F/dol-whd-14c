(function(window) {
  window.__env = window.__env || {};
  Object.assign(window.__env, {
    api_url: '<api_url_for_jenkins>',
    requireHttps: true,
    tokenCookieDurationMinutes: 20160,
    allowedFileTypes: '<allowed_file_types>',
	helpEmailAddress:'14c-help@dol.gov',
    helpPhoneNumber:'1-800-DOL-HELP'	
  });
})(this);
