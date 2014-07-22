(function () {
    'use strict';
    var serviceId = 'bookAuthorService';

    angular.module('app').factory(serviceId, ['$http', '$q', 'tokenCurator', 'serverBase', bookAuthorService]);

    function bookAuthorService($http, $q, tokenCurator, serverBase) {
        // Define the functions and properties to reveal.
        var service = {
            getBooks: getBooks,
            addBook: addBook,
            updateBook: updateBook,
            deleteBook: deleteBook
        };
        return service;

        function getBooks(parameters) {
            var url = serverBase.getServerBaseUrl() + "/api/Book/";

            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: url,
                headers: tokenCurator.getToken(),
                params: parameters
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function addAuthor(newAuthor) {

            var url = serverBase.getServerBaseUrl() + "/api/Author/";

            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: url,
                headers: tokenCurator.getToken(),
                dataType: 'json',
                data: newAuthor
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function updateAuthor(newAuthor) {

            var url = serverBase.getServerBaseUrl() + "/api/Author/";

            var deferred = $q.defer();
            $http({
                method: 'PUT',
                url: url,
                headers: tokenCurator.getToken(),
                data: JSON.stringify(newAuthor)
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function deleteAuthor(authorId) {

            var url = serverBase.getServerBaseUrl() + "/api/Author/" + authorId;

            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: url,
                headers: tokenCurator.getToken()
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }
    }
})();