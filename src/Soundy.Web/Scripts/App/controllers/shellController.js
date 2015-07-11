app.controller('ShellController', function ($scope, $rootScope, $timeout, $mdSidenav, $mdDialog, $mdUtil, $log, songsService) {
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

    $scope.showAddSong = function () {
        $mdDialog.show({
            controller: AddSongDialogController,
            templateUrl: '/Scripts/App/views/crud/songs/add-song.html',
            parent: angular.element(document.body)
        });
    }


    $scope.showAddAuthor = function () {
        $mdDialog.show({
            controller: AddAuthorDialogController,
            templateUrl: '/Scripts/App/views/crud/authors/add-author.html',
            parent: angular.element(document.body)
        });
    }




    $rootScope.songs = [];
    $scope.searchTerm = '';
    $rootScope.isBusy = false;
    $scope.search = function (searchTerm) {
        var promise = songsService.search(searchTerm);
        $rootScope.isBusy = true;
        promise.then(function (result) {
            $rootScope.songs = result;
            if ($scope.posts.length == 0) {
                toastr.info("There are no songs matching the search term.");
            }
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });

    };

});


function AddSongDialogController($scope, $rootScope, $mdDialog, songsService, authorsService) {
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
    $scope.authors = [];
    $rootScope.isBusy = true;
    $scope.addSong = function (song) { };

    var promise = authorsService.getAuthors();
    promise.then(function (result) {
        $scope.authors = result;
        toastr.info("Authors loaded.");
        $rootScope.isBusy = false;

        $scope.addSong = function (song) {
            $rootScope.isBusy = true;
            var promise = songsService.addSong(song);
            promise.then(function (result) {
                $rootScope.songs.push(song);
                toastr.info("Song added.");
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


function AddAuthorDialogController($scope, $rootScope, $mdDialog, authorsService) {
    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };

    $scope.header = {
        monday: 'Mon',
        tuesday: 'Tue',
        wednesday: 'Wed',
        thursday: 'Thu',
        friday: 'Fri',
        saturday: 'Sat',
        sunday: 'Sun',
    }
    $scope.arrows = {
        year: {
            left: 'images/white_arrow_left.svg',
            right: 'images/white_arrow_right.svg'
        },
        month: {
            left: 'images/grey_arrow_left.svg',
            right: 'images/grey_arrow_right.svg'
        }
    }
    $scope.author = {};
    $rootScope.isBusy = true;

    $scope.addAuthor = function (author) {
        $rootScope.isBusy = true;
        var promise = authorsService.addAuthor(author);
        promise.then(function (result) {
            toastr.success("Author added!");
            $scope.hide();
        }, function (error) {
            toastr.error(error.Message);
        }).finally(function () {
            $rootScope.isBusy = false;
        });
    };



}