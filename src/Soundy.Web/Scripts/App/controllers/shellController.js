app.controller('ShellController', function ($scope,$rootScope,$timeout, $mdSidenav, $mdUtil, $log, songsService) {
    $scope.toggle = buildToggler('left');
    function buildToggler(navID) {
        var debounceFn = $mdUtil.debounce(function () {
            $mdSidenav(navID)
              .toggle()
              .then(function () {
                  $log.debug("toggle " + navID + " is done");
              });
        }, 300);
        return debounceFn;
    }
    $scope.closeNav = function () {
        $mdSidenav('left').close()
          .then(function () {
              $log.debug("close RIGHT is done");
          });
    };

    $rootScope.songs = [];
    $scope.searchTerm = '';
    $rootScope.isBusy = false;
    $scope.search = function (searchTerm) {
        var promise = songsService.search(searchTerm);
        $rootScope.isBusy = true;
        promise.then(function (result) {
            $rootScope.songs = result.data;
            if ($scope.posts.length == 0)
            {
                toastr.info("There are no songs matching the search term.");
            }
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });
        
    };

});