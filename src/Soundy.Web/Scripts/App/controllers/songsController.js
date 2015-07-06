app.controller('SongsController', ['$scope', 'songsService', function ($scope, songsService) {
    $scope.songs = [];
    $scope.isBusy = true;
    function activate() {
        var promise = songsService.getSongs();
        promise.then(function (result) {
            $scope.posts = result.data;
            if ($scope.posts.length == 0) {
                toastr.info("There are no songs.");
            }
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $scope.isBusy = false;
        });
    }


}]);