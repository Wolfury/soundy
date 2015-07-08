app.controller('MasterController', ['$scope', '$rootScope', 'songsService', 'playlistsService', 'authorsService', '$mdDialog', function ($scope, $rootScope, songsService, playlistsService, authorsService, $mdDialog) {
    $scope.songs = [];
    $rootScope.isBusy = true;



    $scope.showAddSong = function () {
        $mdDialog.show({
            controller: AddSongDialogController,
            templateUrl: '/Scripts/App/views/crud/songs/add-song.html',
            parent: angular.element(document.body),
            targetEvent: ev,
        });
    }




















    function activate() {
        var promise = songsService.getSongs();
        promise.then(function (result) {
            $rootScope.songs = result.data;
            if ($scope.posts.length == 0) {
                toastr.info("There are no songs.");
            }
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });


    }
}]);

function AddSongDialogController($scope, $mdDialog) {
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
}