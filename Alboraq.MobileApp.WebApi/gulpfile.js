/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    sass = require('gulp-sass'),
    notify = require('gulp-notify'),
    bower = require('gulp-bower'),
    uglify = require('gulp-uglify'),
    concat = require('gulp-concat');

var config = {
    sassPath: './assets/sass',
    jsPath: './assets/js',
    bowerDir: './bower_components',
};

gulp.task('bower', function () {
    return bower().pipe(gulp.dest(config.bowerDir));
});

gulp.task('icons', function () {
    return gulp.src(config.bowerDir + '/font-awesome/fonts/**.*')
        .pipe(gulp.dest('./dist/fonts'));
});
gulp.task('js-lib', function () {
    return gulp.src([        
        config.bowerDir + '/jquery/dist/jquery.min.js',
        config.bowerDir + '/bootstrap-sass/assets/javascripts/bootstrap.min.js',
        config.bowerDir + '/jquery-validation/dist/jquery.validate.min.js',
        config.bowerDir + '/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js',
        './assets/js/lib/**/*.js'
    ]).pipe(concat('lib.min.js')).pipe(gulp.dest('./dist/js'));
});

gulp.task('js', function () {
    return gulp.src(config.jsPath + '/**/*.js')
        .pipe(uglify())
        .pipe(gulp.dest('./dist/js'));
});

gulp.task('css', function () {
    return gulp.src(config.sassPath + '/**/*.scss')
        .pipe(sass({
            outputStyle: 'compressed',
            includePaths: [
                './assets/sass',
                config.bowerDir + './bootstrap-sass/assets/stylesheets',
                config.bowerDir + './font-awesome/scss'
            ]
        }).on('error', notify.onError(function (error) {
            return 'Error: ' + error.message;
        })))
        .pipe(gulp.dest('./dist/css'));
});

gulp.task('watch', function () {
    gulp.watch(config.sassPath + '/**/*.scss', ['css']);
    gulp.watch(config.jsPath + '/**/*.js', ['js']);
});

gulp.task('default', ['bower', 'icons', 'css', 'js']);