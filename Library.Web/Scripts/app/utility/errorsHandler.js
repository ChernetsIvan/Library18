(function () {
    'use strict';
    var utilityId = 'errorsHandler';

    angular.module('app').factory(utilityId, ['alertService', 'strReprs', '$location', errorsHandler]);

    function errorsHandler(alertService, strReprs, $location) {
        // Define the functions and properties to reveal.
        var service = {
            handleError: handleError
        };
        return service;

        //Public
        function handleError(error) {
            if (error === 400) {
                alertService.addAlert('danger', strReprs.getRegisterFailed());
            }
            if (error === 401) {
                alertService.addAlert('danger', strReprs.getUnauthorized());
                $location.path('/Account');
            }
            if (error == "") {
                alertService.addAlert('danger', strReprs.getConnectionFailed());
            }
        }
    }
})();