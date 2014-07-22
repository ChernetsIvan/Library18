(function () {
    'use strict';
    var serviceId = 'bookService';

    angular.module('app').factory(serviceId, ['$http', '$q', 'tokenCurator', 'serverBase', bookService]);

    function bookService($http, $q, tokenCurator, serverBase) {
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

        function addBook(newBook) {

            var url = serverBase.getServerBaseUrl() + "/api/Book/";

            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: url,
                headers: tokenCurator.getToken(),
                dataType: 'json',
                data: newBook
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function updateBook(newBook) {

            var url = serverBase.getServerBaseUrl() + "/api/Book/";

            var deferred = $q.defer();
            $http({
                method: 'PUT',
                url: url,
                headers: tokenCurator.getToken(),
                data: JSON.stringify(newBook)
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function deleteBook(bookId) {

            var url = serverBase.getServerBaseUrl() + "/api/Book/" + bookId;

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