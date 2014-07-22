(function () {
    'use strict';
    var utilityId = 'tokenCurator';

    angular.module('app').factory(utilityId, ['$cookies',tokenCurator]);

    function tokenCurator($cookies) {
        // Define the functions and properties to reveal.
        var service = {
            getToken: getToken,
            setToken: setToken
        };
        return service;

        //Public
        function getToken() {
            if ($cookies.access_token) {
                return { "Authorization": "Bearer " + $cookies.access_token };
            }
            return "Error! Token has not been set";
        }
        function setToken(token) {
            $cookies.access_token = token;
        }
    }
})();