
'use strict';

app.service('authorsService', authorsService);

authorsService.$inject = ['$http', '$q'];

function authorsService($http, $q) {
    function getAuthors() {
        var deffered = $q.defer();

        $http.get('api/authors/get')
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function getAuthor(id) {
        var deffered = $q.defer();

        $http.get('api/authors/get/' + id)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function addAuthor(author) {
        var deffered = $q.defer();

        $http.post('api/authors/create', author)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function updateAuthor(author) {
        var deffered = $q.defer();

        $http.put('api/authors/update/' + author.Id, author)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    function deleteAuthor(id) {
        var deffered = $q.defer();

        $http.delete('api/authors/delete/' + id)
            .success(function (response) { deffered.resolve(response); })
            .error(function (response) { deffered.reject(response); });

        return deffered.promise;
    }

    this.getAuthors = getAuthors;
    this.getAuthor = getAuthor;
    this.addAuthor = addAuthor;
    this.updateAuthor = updateAuthor;
    this.deleteAuthor = deleteAuthor;
}
