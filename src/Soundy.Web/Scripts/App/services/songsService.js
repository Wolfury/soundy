
'use strict';

app.service('songsService', songsService);

songsService.$inject = ['$http', '$q'];

function songsService($http, $q)
{
    function getSongs() {
        var deffered = $q.defer();
        $http.get('api/songs/get').success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function shuffleSongs() {
        var deffered = $q.defer();
        $http.get('api/songs/shuffle').success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function getSong(id) {
        var deffered = $q.defer();
        $http.get('api/songs/get/' + id).success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function addSong(song) {
        var deffered = $q.defer();
        $http.post('api/songs/create', song).success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function updateSong(song) {
        var deffered = $q.defer();
        $http.put('api/songs/update/' + song.Id, song).success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function deleteSong(song) {
        var deffered = $q.defer();
        $http.delete('api/songs/delete' + song.Id).success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function search(searchTerm) {
        var deffered = $q.defer();

        $http.get('api/songs/search?searchTerm=' + searchTerm)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response) });

        return deffered.promise;
    }

    this.getSongs = getSongs;
    this.shuffleSongs = shuffleSongs;
    this.getSong = getSong;
    this.addSong = addSong;
    this.updateSong = updateSong;
    this.deleteSong = deleteSong;
    this.search = search;
}
