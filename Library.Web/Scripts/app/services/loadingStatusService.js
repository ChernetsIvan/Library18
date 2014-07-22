(function () {
    'use strict';
    var serviceId = 'loadingStatusService';

    angular.module('app').factory(serviceId, [loadingStatusService]);

    function loadingStatusService() {
        //Private fields
        var status = true;
        var observerCallbacks = []; //for use observer pattern

        // Define the functions and properties to reveal.
        var service = {
            registerObserverCallback: registerObserverCallback,
            showLoading: showLoading,
            hideLoading: hideLoading,
            getStatus: getStatus
    };
        return service;

        //Public methods
        function registerObserverCallback(callback) {
            observerCallbacks.push(callback);
        }

        function showLoading() {
            status = true;
            notifyObservers();
        }

        function hideLoading() {
            status = false;
            notifyObservers();
        }

        function getStatus() {
            return status;
        }

        //Private methods
        function notifyObservers() {
            angular.forEach(observerCallbacks, function (callback) {
                callback();
            });
        }
    }
})();