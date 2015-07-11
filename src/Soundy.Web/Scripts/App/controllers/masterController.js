app.controller('MasterController', ['$scope', '$rootScope', 'songsService', 'playlistsService', 'authorsService', '$mdDialog', function ($scope, $rootScope, songsService, playlistsService, authorsService, $mdDialog) {


    activate();
    $rootScope.currentUpdateSong = {};

    $scope.updateSong = function (song) {
        $rootScope.currentUpdateSong = song;
        $mdDialog.show({
            controller: UpdateSongDialogController,
            templateUrl: '/Scripts/App/views/crud/songs/update-song.html',
            parent: angular.element(document.body)
        });
    };


    $scope.deleteSong = function (song) {

        var promise = songsService.deleteSong(song);
        $rootScope.isBusy = true;
        promise.then(function (response) { activate(); toastr.success("Song deleted"); })
            .error(function (response) { toastr.error("An error has occured"); })
            .finally(function () { $rootScope.isBusy = false; });

    };










    function activate() {

        $rootScope.isBusy = true;
        var promise = songsService.getSongs();
        promise.then(function (data) {
            $rootScope.songs = data;
            if ($rootScope.songs.length == 0) {
                toastr.info("There are no songs.");
            }
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });


    }
}]);

function UpdateSongDialogController($scope, $rootScope, $mdDialog, songsService, authorsService) {
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
    $scope.updateSong = function (song) { };
    $scope.authors = [];

    $scope.currentUpdateSong = $rootScope.currentUpdateSong;
    var promise = authorsService.getAuthors();
    promise.then(function (result) {
        $scope.authors = result;
        toastr.info("Authors loaded.");
        $rootScope.isBusy = false;
        $scope.updateSong = function (song) {
            $rootScope.isBusy = true;
            var promise = songsService.updateSong(song);
            promise.then(function (result) {
                $rootScope.songs.push(song);
                toastr.info("Song updated.");
                $scope.hide();
            }, function (error) {
                toastr.error(error.Message);
            }).finally(function () {
                $rootScope.isBusy = false;
            });
        };

    }, function (error) {
        toastr.error(error.Message);
    }).finally(function () {
        $rootScope.isBusy = false;
    });




}

