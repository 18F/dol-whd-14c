"use strict";

var gulp = require('gulp');
var concat = require('gulp-concat');
var connect = require('gulp-connect');
var open = require('gulp-open');
var sass = require('gulp-sass');
var browserify = require('browserify');
var jshint = require('gulp-jshint');
var source = require('vinyl-source-stream');
var del = require('del');
var runSequence = require('run-sequence');
var Server = require('karma').Server;
var mswebdeploy = require('gulp-mswebdeploy-package');

var config = {
    port: 9000,
    devBaseUrl: 'http://localhost',
    apiUrl: 'http://localhost:50014',
    paths: {
        html: ['./app/*.html', './app/**/*.html'],
        js: ['./app/*.js', './app/**/*.js'],
        sass: ['./app/sass/main.scss'],
        fonts: ['./node_modules/uswds/dist/fonts/**/*.*'],
        assets: [ './node_modules/uswds/dist/img/**/*.*'],
        config: './web.config',
        dist: './dist',
        deploy: './deploy',
        mainJs: './app/main.js'
    }
}

gulp.task('connect', function () {
    return connect.server({
        root: ['dist'],
        port: config.port,
        base: config.devBaseUrl,
        livereload: true
    });
});

gulp.task('open', ['connect'], function () {
    return gulp.src('dist/index.html')
		.pipe(open({ uri: config.devBaseUrl + ':' + config.port + '/' }));
});

gulp.task('clean', function () {
    return del([config.paths.deploy, config.paths.dist]);
});

gulp.task('lint', function () {
    return gulp.src(config.paths.js)
      .pipe(jshint({ "browserify": true }))
      .pipe(jshint.reporter('default'))
      .pipe(jshint.reporter('fail'));
});

gulp.task('test', function (done) {
    return new Server({
        configFile: __dirname + '/karma.conf.js',
        singleRun: true
    }, done).start();
});

gulp.task('sass', function () {
    return gulp.src(config.paths.sass)
        .pipe(sass({
            // includePaths: require('node-bourbon').with('other/path', 'another/path')
            // - or -
            includePaths: require('node-bourbon').includePaths
        }))
        .pipe(concat('bundle.css'))
        .pipe(gulp.dest(config.paths.dist + '/css'));
});

gulp.task('js', function () {
    return browserify(config.paths.mainJs)
       .bundle()
       .pipe(source('bundle.js'))
       .pipe(gulp.dest(config.paths.dist + '/js'));
});

gulp.task('config', function () {
    return gulp.src(config.paths.config)
        .pipe(gulp.dest(config.paths.dist));
});

gulp.task('html', function () {
    return gulp.src(config.paths.html)
        .pipe(gulp.dest(config.paths.dist));
});

gulp.task('fonts', function () {
    return gulp.src(config.paths.fonts)
        .pipe(gulp.dest(config.paths.dist + '/fonts'));
});

gulp.task('assets', function () {
    return gulp.src(config.paths.assets)
        .pipe(gulp.dest(config.paths.dist + '/img'));
});

gulp.task('build-webdeploy', function () {
    return gulp.src('app/')
           .pipe(mswebdeploy({
               'source': 'dist',
               "dest": "webdeploy",
               "package":"DOL.WHD.Section14c.Web.zip",
               'parameters': [
                   {
                       'parameter': {
                           '@name': 'ApiUrl',
                           '@description': 'Base URL for API',
                           'parameterEntry': {
                               '@type': 'TextFile',
                               '@scope': 'dist\\\\js\\\\bundle.js',
                               '@match': config.apiUrl
                           }
                       }
                   }
               ]
           }))
           .pipe(gulp.dest('/webdeploy'));
});

gulp.task('watch', ['connect'], function () {
    gulp.watch(config.paths.html, ['html']);
    gulp.watch(config.paths.js, ['js', 'lint']);
    gulp.src('dist/index.html')
		.pipe(open({ uri: config.devBaseUrl + ':' + config.port + '/' }));
});


gulp.task('package', function () {
    return runSequence(
      'clean', 'lint', 'js', 'html', 'assets', 'fonts', 'sass', 'config', 'build-webdeploy'
    );
});

gulp.task('default', function () {
    return runSequence(
      'clean', 'lint', 'js', 'html', 'assets', 'fonts', 'sass'
    );

});