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

var config = {
    port: 9000,
    devBaseUrl: 'http://localhost',
    paths: {
        html: ['./app/*.html', './app/**/*.html'],
        js: ['./app/*.js', './app/**/*.js'],
        sass: ['./app/sass/main.scss'],
        fonts: ['./node_modules/uswds/dist/fonts/**/*.*'],
        assets: [ './node_modules/uswds/dist/img/**/*.*'],
        dist: './dist',
        mainJs: './app/main.js'
    }
}

gulp.task('connect', function () {
    connect.server({
        root: ['dist'],
        port: config.port,
        base: config.devBaseUrl,
        livereload: true
    });
});

gulp.task('open', ['connect'], function () {
    gulp.src('dist/index.html')
		.pipe(open({ uri: config.devBaseUrl + ':' + config.port + '/' }));
});

gulp.task('clean', function () {
    return del(config.paths.dist);
});

gulp.task('lint', function () {
    return gulp.src(config.paths.js)
      .pipe(jshint({ "browserify": true }))
      .pipe(jshint.reporter('default'))
      .pipe(jshint.reporter('fail'));
});

gulp.task('test', function (done) {
    new Server({
        configFile: __dirname + '/karma.conf.js',
        singleRun: true
    }, done).start();
});

gulp.task('sass', function () {
    gulp.src(config.paths.sass)
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

gulp.task('html', function () {
    gulp.src(config.paths.html)
        .pipe(gulp.dest(config.paths.dist));
});

gulp.task('fonts', function () {
    gulp.src(config.paths.fonts)
        .pipe(gulp.dest(config.paths.dist + '/fonts'));
});

gulp.task('assets', function () {
    gulp.src(config.paths.assets)
        .pipe(gulp.dest(config.paths.dist + '/img'));
});


gulp.task('watch', ['connect'], function () {
    gulp.watch(config.paths.html, ['html']);
    gulp.watch(config.paths.js, ['js', 'lint']);
    gulp.src('dist/index.html')
		.pipe(open({ uri: config.devBaseUrl + ':' + config.port + '/' }));
});

gulp.task('default', function () {
    return runSequence(
      'clean', 'lint', 'js', 'html', 'assets', 'fonts', 'sass'
    );

});