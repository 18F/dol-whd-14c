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
<<<<<<< HEAD
  --password='abc123 ;)' \
=======
  --password='Boom18f!!' \
>>>>>>> remotes/origin/origin/sprint/0/DOL-WHD-14c-184
  --url-base='https://localhost:3333' \
  --url-path='section/work-sites'

*/

const fs = require('fs');
const _ = require('lodash');
const pa11y = require('pa11y');
const jsonReporter = require('pa11y/reporter/json');
const htmlReporter = require('pa11y/reporter/html');
const program = require('commander');

program
  .description('Run an accessibility test against a 14(c) app URL')
  .option('-e, --email <email>', 'Add user email')
  .option('-p, --password <password>', 'Add user password')
  .option(
<<<<<<< HEAD
    '-s, --standard <standard>',
    'Add accessibility standard (Section508, WCAG2A, WCAG2AA (default), WCAG2AAA)',
    'WCAG2AA'
  )
  .option(
=======
>>>>>>> remotes/origin/origin/sprint/0/DOL-WHD-14c-184
    '-b, --url-base <url_base>',
    'Add URL base',
    'https://dol-whd-section14c-stg.azurewebsites.net'
  )
  .option('-u, --url-path <url_path>', 'Add URL path to scan')
  .option('-w, --show-warnings', 'Show warnings')
<<<<<<< HEAD
  .option('-j, --save-json', 'Save results to json file')
  .option('-h, --save-html', 'Save results to html file')
=======
>>>>>>> remotes/origin/origin/sprint/0/DOL-WHD-14c-184
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
<<<<<<< HEAD
  standard: program.standard,
  url: { full: `${program.urlBase}/${urlPath}`, path: urlPath },
  showWarnings: !!program.showWarnings,
  saveJson: !!program.saveJson,
  saveHtml: !!program.saveHtml
=======
  url: { full: `${program.urlBase}/${urlPath}`, path: urlPath },
  showWarnings: !!program.showWarnings
>>>>>>> remotes/origin/origin/sprint/0/DOL-WHD-14c-184
};

console.log('RUN PARAMS:', JSON.stringify(PARAMS));

const runner = pa11y({
  log: {
    debug: console.log.bind(console),
    error: console.error.bind(console),
    info: console.log.bind(console)
  },

  standard: PARAMS.standard,

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

    function setEnvGlobal(envFnStr) {
      // Loads env into window.__env
      eval(envFnStr); // eslint-disable-line no-eval

      // The _env global is an object, so while we can't change
      // the reference, we CAN change its properties.
      var globalEnvObject = angular
        .element(document.body)
        .injector()
        .get('_env');

      Object.assign(globalEnvObject, window.__env);
    }

    function setEnv(callback) {
      const envFnStr = fs.readFileSync('env.js', { encoding: 'utf-8' });
      page.evaluate(setEnvGlobal, envFnStr, callback);
    }

    // kick things off (log in -> go to proper page)
    setEnv(() => page.evaluate(doAuth, PARAMS, postAuth));
  }
});

function prettify(e) {
  return `${e.code}\n\n${e.message}\n\n${e.context}\n\n---\n`;
}

function displayEntries(data, key) {
  const entries = data[key] || [];

  if (!entries.length) {
    return console.log(`\n\nWoohoo! No ${key}s!`);
  }

  console.log(`\n\nHere are the ${key} entries:\n------\n`);
  entries.forEach(e => console.log(prettify(e)));
}

function handleResults(data) {
  console.log('\n\nPa11y results:\n------\n');
  const dataGrouped = _.groupBy(data, d => d.type);

  for (const key in dataGrouped) {
    const entries = dataGrouped[key];
    console.log(`"${key}" entries: ${entries.length}`);
  }

  if (PARAMS.showWarnings) displayEntries(dataGrouped, 'warning');
  displayEntries(dataGrouped, 'error');
<<<<<<< HEAD

  if (PARAMS.saveJson) {
    const fname = `pa11y-results-${PARAMS.standard}.json`;
    const json = {
      url: PARAMS.url.full,
      results: jsonReporter.process(data)
    };

    fs.writeFileSync(fname, JSON.stringify(json));
    console.log(`Results saved to ${fname}!`);
  }

  if (PARAMS.saveHtml) {
    const fname = `pa11y-results-${PARAMS.standard}.html`;
    const url = PARAMS.url.full;

    fs.writeFileSync(fname, htmlReporter.process(data, url));
    console.log(`Results saved to ${fname}!`);
  }  
=======
>>>>>>> remotes/origin/origin/sprint/0/DOL-WHD-14c-184
}

runner.run(PARAMS.url.full, (error, results) => {
  if (error) return console.error(error.message);
  handleResults(results);
});
