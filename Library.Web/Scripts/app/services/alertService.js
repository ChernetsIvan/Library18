(function () {
    'use strict';
    var serviceId = 'alertService';

    angular.module('app').factory(serviceId, ['$timeout', 'guidGenerator', alertService]);

    function alertService($timeout, guidGenerator) {
        //Private fields
        var alertTimerInit = 6000;
        var allAlerts = [];
        var observerCallbacks = []; //for use observer pattern


        // Define the functions and properties to reveal.
        var service = {
            init : init,
            addAlert: addAlert,
            registerObserverCallback: registerObserverCallback,
            getAlerts: getAlerts,
            removeAlert: removeAlert,
            setTimerSecRemainedStartedToAlert: setTimerSecRemainedStartedToAlert,
            setBeginHideAnimation: setBeginHideAnimation
        };
        return service;                                            // !  all fields define before it !

        //Public methods
        function init() {           
        }

        function addAlert(inputType, inputMsg) {
            var alert = {
                id: guidGenerator.getNewGuid(),
                type: inputType,
                msg: inputMsg,
                timerSec: alertTimerInit,
                timerSecRemainedStarted: false,
                beginHideAnimation: false
        };
            allAlerts.push(alert);
            notifyObservers();
            $timeout(
                function() {
                    removeAlert(alert.id);
                }, alertTimerInit);
        }       

        function registerObserverCallback(callback) {
            observerCallbacks.push(callback);
        }        

        function getAlerts() {
            return allAlerts;
        }

        function removeAlert(alertId) {
            for (var i = 0; i < allAlerts.length; i++) {
                if (allAlerts[i].id == alertId) {
                    allAlerts.remove(i);
                    return;
                }
            };
        }

        function setTimerSecRemainedStartedToAlert(flag, alertId) {
            for (var i = 0; i < allAlerts.length; i++) {
                if (allAlerts[i].id == alertId) {
                    allAlerts[i].timerSecRemainedStarted = flag;
                    return;
                }
            };
        }

        function setBeginHideAnimation(flag, alertId) {
            for (var i = 0; i < allAlerts.length; i++) {
                if (allAlerts[i].id == alertId) {
                    allAlerts[i].beginHideAnimation = flag;
                    return;
                }
            };
        }

        //Private methods        
        function notifyObservers() {
            angular.forEach(observerCallbacks, function (callback) {
                callback();
            });
        }

        // Array Remove - By John Resig (MIT Licensed)
        Array.prototype.remove = function(from, to) {
            var rest = this.slice((to || from) + 1 || this.length);
            this.length = from < 0 ? this.length + from : from;
            return this.push.apply(this, rest);
        };
    }
})();