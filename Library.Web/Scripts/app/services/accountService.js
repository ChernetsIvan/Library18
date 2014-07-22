(function () {
    'use strict';
    var serviceId = 'accountService';

    angular.module('app').factory(serviceId, ['$http', '$q', 'tokenCurator', 'serverBase', accountService]);

    function accountService($http, $q, tokenCurator, serverBase) {                                 //потом можно переделать и хранить accessToken не в куках - а тут
        // Define the functions and properties to reveal.
        var service = {
            registerUser: registerUser,
            loginUser: loginUser,
            getValues: getValues,
        };
        return service;

        function registerUser(userData) {

            var accountUrl = serverBase.getServerBaseUrl() + "/api/Account/Register";

            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: accountUrl,
                data: userData,
            }).success(function (data) {
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function loginUser(userData) {
            var tokenUrl = serverBase.getServerBaseUrl() + "/Token";

            if (!userData.grant_type) {
                userData.grant_type = "password";
            }

            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: tokenUrl,
                data: userData,
            }).success(function (data) {
                // save the access_token as this is required for each API call.
                tokenCurator.setToken(data.access_token);
                console.log(data);
                deferred.resolve(data);

            }).error(function (err, status) {
                console.log(err);
                deferred.reject(status);
            });

            return deferred.promise;
        }

        function getValues() {
            var url = serverBase.getServerBaseUrl() + "/api/values/";

            var deferred = $q.defer();
            $http({
                method: 'GET',
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