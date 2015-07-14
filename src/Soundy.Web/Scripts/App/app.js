var app = angular.module("Soundy", ['ngRoute', 'ngAnimate', 'ngSanitize', 'ngMaterial', '720kb.datepicker']);

app.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "MasterController",
        templateUrl: "/Scripts/App/views/master.html"
    });
    $routeProvider.when("/playlist/:id", {
        controller: "PlaylistDetailsController",
        templateUrl: "/Scripts/App/views/crud/playlists/playlist.html"
    });
    $routeProvider.otherwise("/");



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