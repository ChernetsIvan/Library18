(function () {
    'use strict';
    var utilityId = 'serverBase';

    angular.module('app').factory(utilityId, [serverBase]);

    function serverBase() {
        //Private fields
        var serverBaseUrl = "http://localhost:63578";

        // Define the functions and properties to reveal.
        var service = {
            getServerBaseUrl: getServerBaseUrl
        };
        return service;

        //Public
        function getServerBaseUrl() {
            return serverBaseUrl;
        }
    }
})();