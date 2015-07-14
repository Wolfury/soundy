app.controller('MasterController', ['$scope', '$rootScope', 'songsService', 'playlistsService', 'authorsService', '$mdDialog', '$location', function ($scope, $rootScope, songsService, playlistsService, authorsService, $mdDialog, $location) {


    activate();
    $rootScope.currentUpdateSong = {};
    $rootScope.currentUpdatePlaylist = {};


    $scope.shuffleSongs = function () {
        var promise = songsService.shuffleSongs();
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
    };
    $scope.openPlaylist = function (playlist) {
        $location.path('/playlist/' + playlist.Id);
    };
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

        promise.then(function (response) {
            activate(); toastr.success("Song deleted!");
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });
    };

    $scope.updatePlaylist = function (playlist) {
        $rootScope.currentUpdatePlaylist = playlist;
        $mdDialog.show({
            controller: UpdatePlaylistDialogController,
            templateUrl: '/Scripts/App/views/crud/playlists/update-playlist.html',
            parent: angular.element(document.body)
        });
    };
    $scope.deletePlaylist = function (playlist) {
        var promise = playlistsService.deletePlaylist(playlist.Id);
        $rootScope.isBusy = true;
        promise.then(function (response) {
            activate(); toastr.success("Playlist deleted!");
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });
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



        var playlistsPromise = playlistsService.getPlaylists();
        $rootScope.isBusy = true;
        playlistsPromise.then(function (data) {

            $rootScope.playlists = data;
        },
        function (err) {
            toastr.error(err.Message);
        })
        .finally(function () {
            $rootScope.isBusy = false;
        });

        var authorsPromise = authorsService.getAuthors();
        $rootScope.isBusy = true;
        authorsPromise.then(function (data) {

            $rootScope.authors = data;
        },
        function (err) {
            toastr.error(err.Message);
        })
        .finally(function () {
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
        toastr.info("Authors loaded!");
        $rootScope.isBusy = false;
        $scope.updateSong = function (song) {
            $rootScope.isBusy = true;
            var promise = songsService.updateSong(song);
            promise.then(function (result) {
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


function UpdatePlaylistDialogController($scope, $rootScope, $mdDialog, playlistsService) {
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };

    $scope.currentUpdatePlaylist = $rootScope.currentUpdatePlaylist;
    $scope.updatePlaylist = function (playlist) {
        $rootScope.isBusy = true;
        var promise = playlistsService.updatePlaylist(playlist);
        promise.then(function (result) {
            toastr.info("Playlist updated!");
            $scope.hide();
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });
    };




}

