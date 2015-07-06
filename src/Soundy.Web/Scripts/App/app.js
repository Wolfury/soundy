var app = angular.module("Soundy", ['ngRoute', 'ngAnimate', 'ngSanitize', 'ngMaterial']);

app.config(function ($routeProvider) {
    $routeProvider.when("/songs/list", {
        controller: "SongsController",
        templateUrl: "/Scripts/App/views/songs/list.html"
    });

    $routeProvider.when("/", {
        controller: "SongsController",
        templateUrl: "/Scripts/App/views/songs/list.html"
    });



});

app.config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});

app.config(function ($mdThemingProvider) {
    $mdThemingProvider.theme('default')
        .primaryPalette('cyan', { 'default': '700' })
        .accentPalette('yellow');

    $mdThemingProvider.theme('dark', 'default')
      .primaryPalette('cyan', { 'default': '700' })
      .dark();
});




console.log(app);