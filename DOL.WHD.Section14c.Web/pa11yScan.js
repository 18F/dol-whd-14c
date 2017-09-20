#!/usr/bin/env node

/*

  examples:

  basic example:

  ./pa11yScan.js \
  --email='brendan@foobar.com' \
  --password='abc123 ;)' \
  --url-path='section/work-sites'

  use "url-base" to specify a different URL origin (i.e., localhost):

  ./pa11yScan.js \
  --email='brendan.sudol@gsa.gov' \
  --password='Boom18f!!' \
  --url-base='https://localhost:3333' \
  --url-path='section/work-sites'

*/

const _ = require('lodash');
const pa11y = require('pa11y');
const program = require('commander');

program
  .description('Run an accessibility test against a 14(c) app URL')
  .option('-e, --email <email>', 'Add user email')
  .option('-p, --password <password>', 'Add user password')
  .option(
    '-b, --url-base <url_base>',
    'Add URL base',
    'https://dol-whd-section14c-stg.azurewebsites.net'
  )
  .option('-u, --url-path <url_path>', 'Add URL path to scan')
  .parse(process.argv);

const errMsg = `
  Uh-oh! Make sure you specify the proper params.
  For help, run: ./pa11yScan.js --help
`;

if (!program.email || !program.password || !program.urlPath) {
  console.error(errMsg);
  process.exit(1);
}

const urlConnect = program.urlBase.includes('localhost') ? '#!' : '#';
const urlPath = `${urlConnect}/${program.urlPath}`;

const PARAMS = {
  userVal: program.email,
  pwVal: program.password,
  url: { full: `${program.urlBase}/${urlPath}`, path: urlPath }
};

console.log('RUN PARAMS:', JSON.stringify(PARAMS));

const runner = pa11y({
  log: {
    debug: console.log.bind(console),
    error: console.error.bind(console),
    info: console.log.bind(console)
  },

  beforeScript: function(page, options, next) {
    // show console messages from web page
    page.onConsoleMessage = msg => console.log(msg);

    // check for some condition on web page before continuing
    function waitUntil(condition, retries, waitOver) {
      page.evaluate(condition, PARAMS, (error, result) => {
        if (result || retries < 1) waitOver();
        else {
          retries -= 1;
          setTimeout(() => waitUntil(condition, retries, waitOver), 200);
        }
      });
    }

    // redirect related methods

    function doRedirect(args) {
      const redirectUrl = args.url.full;
      console.log('Redirecting to', redirectUrl);

      window.location = redirectUrl;
      window.location.reload();
    }

    function checkRedirect(args) {
      // check that current url = target url
      const currUrl = window.location.href;
      console.log('Current url:', currUrl);
      return currUrl === args.url.full;
    }

    function startPa11y() {
      setTimeout(next, 1000);
    }

    function postRedirect() {
      waitUntil(checkRedirect, 10, startPa11y);
    }

    function pageRedirect() {
      page.evaluate(doRedirect, PARAMS, postRedirect);
    }

    // auth related methods

    function doAuth(args) {
      console.log('args:', JSON.stringify(args));
      console.log('Filling in login form...');

      var user = document.querySelector('#userName');
      var password = document.querySelector('#password');
      var submit = document.querySelector('.loginbtn button');

      var userEl = angular.element(user);
      var pwEl = angular.element(password);

      userEl.val(args.userVal);
      userEl.triggerHandler('input');

      pwEl.val(args.pwVal);
      pwEl.triggerHandler('input');

      console.log('Submitting login form...');
      submit.click();
    }

    function checkAuth() {
      // user input shouldn't be on page if login successful
      return document.querySelector('#userName') === null;
    }

    function postAuth() {
      waitUntil(checkAuth, 10, pageRedirect);
    }

    // kick things off (log in -> go to proper page)
    page.evaluate(doAuth, PARAMS, postAuth);
  }
});

function prettyEntry(e) {
  return `${e.code}\n\n${e.message}\n\n${e.context}\n\n---\n`;
}

function handleResults(data) {
  console.log('\n\nPa11y results:\n------\n');
  const dataGrouped = _.groupBy(data, d => d.type);

  for (const key in dataGrouped) {
    const entries = dataGrouped[key];
    console.log(`"${key}" entries: ${entries.length}`);
  }

  const errors = dataGrouped.error || [];
  if (!errors.length) {
    console.log('\n\nWoohoo! No accessibility errors!');
  } else {
    console.log('\n\nHere are the error entries:\n------\n');
    errors.forEach(e => console.log(prettyEntry(e)));
  }
}

runner.run(PARAMS.url.full, (error, results) => {
  if (error) return console.error(error.message);
  handleResults(results);
});
