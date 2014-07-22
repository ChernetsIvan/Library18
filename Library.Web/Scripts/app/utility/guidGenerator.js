(function () {
    'use strict';
    var utilityId = 'guidGenerator';

    angular.module('app').factory(utilityId, [guidGenerator]);

    function guidGenerator() {
        // Define the functions and properties to reveal.
        var service = {
            getNewGuid: getNewGuid
        };
        return service;

        //Public
        function getNewGuid() {
            // then to call it, plus stitch in '4' in the third group
            var guid = (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
            return guid;
        }

        //Private
        function S4() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
        }
    }
})();