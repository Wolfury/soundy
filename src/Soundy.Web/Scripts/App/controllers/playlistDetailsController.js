
app.controller('PlaylistDetailsController', ['$scope', '$rootScope', 'songsService', 'playlistsService', 'authorsService', '$mdDialog', '$routeParams', function ($scope, $rootScope, songsService, playlistsService, authorsService, $mdDialog, $routeParams) {


    activate();
    $rootScope.playlist = {};
    $scope.shuffleSongs = function () { };
   

    $scope.showAddSongToPlaylist = function () {

        $mdDialog.show({
            controller: AddSongToPlaylistDialogController,
            templateUrl: '/Scripts/App/views/crud/playlists/add-song.html',
            parent: angular.element(document.body)
        });

    };


    $scope.removeSongFromPlaylist = function (playlist, song) {
        var promise = playlistsService.removeSongFromPlaylist(playlist, song);
        $rootScope.isBusy = true;
        promise.then(function (data) { activate(); toastr.success("Removed song from playlist!"); },
            function (error) { toastr.error(error.Message); })
                .finally(function () {
                    $rootScope.isBusy = false;
                });
    };




    function activate() {

        var promise = playlistsService.getPlaylist($routeParams.id);
        $rootScope.isBusy = true;
        promise.then(function (playlist) {
            $rootScope.playlist = playlist;
            $rootScope.nowPlaying = playlist.Songs[0];

            $scope.shuffleSongs = function () {
                var promise = playlistsService.shuffleSongs(playlist);
                promise.then(function (data) {
                    $rootScope.playlist.Songs = data;
                    if ($rootScope.playlist.Songs.length == 0) {
                        toastr.info("There are no songs.");
                    }
                }, function (error) {
                    toastr.error(error.Message);
                }).finally(function () {
                    $rootScope.isBusy = false;
                });
            };



        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () { $rootScope.isBusy = false; });


    }
}]);

function AddSongToPlaylistDialogController($scope, $location, $rootScope, $mdDialog, playlistsService, $route) {
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };

    $scope.song = {};
    $scope.songs = $rootScope.songs;
    $scope.playlist = $rootScope.playlist;

    $scope.addSong = function (song) {
        var promise = playlistsService.addSongToPlaylist($scope.playlist, $scope.song);
        $rootScope.isBusy = true;
        promise.then(function (data) {
            $rootScope.playlist.Songs.push(song);
            $mdDialog.hide();
            $route.reload();
        },
        function (err) {
            toastr.error(err.Message);
            $mdDialog.hide();
        })
        .finally(function () {
            $rootScope.isBusy = false;
        });
    };

}

