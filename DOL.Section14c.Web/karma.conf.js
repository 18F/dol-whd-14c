// Karma configuration
// Generated on Fri Sep 23 2016 09:19:06 GMT-0400 (Eastern Daylight Time)

module.exports = function(config) {
    config.set({

        // base path that will be used to resolve all patterns (eg. files, exclude)
        basePath: '',


        // frameworks to use
        // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
        frameworks: ['browserify', 'mocha', 'chai'],

        files: [
            './node_modules/angular/angular.js',
            './node_modules/angular-route/angular-route.js',
            './node_modules/angular-mocks/angular-mocks.js',
            './app/main.js',
            './app/**/*.js',
        ],

        // list of files to exclude
        exclude: [
        ],


        // preprocess matching files before serving them to the browser
        // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor

        preprocessors: {
            './node_modules/angular/angular.js': ["browserify"],
            './node_modules/angular-route/angular-route.js': ["browserify"],
            './node_modules/angular-mocks/angular-mocks.js': ["browserify"],
            './app/main.js': ["browserify"],
            './app/**/*.js': ["browserify"]
        },


        browserify: {
            debug: true
        },


        // test results reporter to use
        // possible values: 'dots', 'progress'
        // available reporters: https://npmjs.org/browse/keyword/karma-reporter
        reporters: ['progress'],


        // web server port
        port: 9876,


        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: false,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        browsers: ['PhantomJS'],


        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: true,

        // Concurrency level
        // how many browser should be started simultaneous
        concurrency: Infinity
    });
};