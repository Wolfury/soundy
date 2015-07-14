
'use strict';

app.service('playlistsService', playlistsService);

playlistsService.$inject = ['$http', '$q'];

function playlistsService($http, $q) {
    function getPlaylists() {
        var deffered = $q.defer();

        $http.get('api/playlists/get')
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }
    function shuffleSongs(playlist) {
        var deffered = $q.defer();
        $http.get('api/songs/shuffle/' + playlist.Id, playlist.Songs).success(function (data) {
            deffered.resolve(data);
        }).error(function (error) {
            deffered.reject(error);
        });
        return deffered.promise;
    }

    function getPlaylist(id) {
        var deffered = $q.defer();

        $http.get('api/playlists/get/' + id)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function createPlaylist(playlist) {
        var deffered = $q.defer();

        $http.post('api/playlists/create', playlist)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function updatePlaylist(playlist) {
        var deffered = $q.defer();

        $http.put('api/playlists/update/' + playlist.Id, playlist)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function deletePlaylist(id) {
        var deffered = $q.defer();

        $http.delete('api/playlists/delete/' + id)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function addSongToPlaylist(playlist, song) {
        var deffered = $q.defer();

        $http.post('api/playlists/addsongtoplaylist/' + playlist.Id, song)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;

    }

    this.getPlaylists = getPlaylists;
    this.shuffleSongs = shuffleSongs;
    this.getPlaylist = getPlaylist;
    this.createPlaylist = createPlaylist;
    this.updatePlaylist = updatePlaylist;
    this.deletePlaylist = deletePlaylist;
    this.addSongToPlaylist = addSongToPlaylist;
}
