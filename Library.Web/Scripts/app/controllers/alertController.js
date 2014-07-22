(function () {
    'use strict';
    var controllerId = 'alertController';

    angular.module('app').controller(controllerId,
        ['alertService', '$scope', '$timeout', alertController]);              //order is important (see in authorController)

    function alertController(alertService, $scope, $timeout) {
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).

        //Bindable public methods - as interface :)       
        $scope.init = init;
        $scope.closeAlert = closeAlert;
        $scope.isBeginShowHideAnimation = isBeginShowHideAnimation;
        
        // Bindable public properties
        $scope.alerts = [];

        //Public methods
        function init() {

        }

        function closeAlert(alertId) {
            alertService.removeAlert(alertId);
            for (var i = 0; i < $scope.alerts.length; i++) {
                if ($scope.alerts[i].id == alertId) {
                    $scope.alerts.remove(i);                   
                    return;
                }
            };
        }

        function isBeginShowHideAnimation(alertId) {
            for (var i = 0; i < $scope.alerts.length; i++) {
                if ($scope.alerts[i].id == alertId) {                   
                    if ($scope.alerts[i].beginHideAnimation == true) {
                        return false;
                    }
                }
            };
            return true;
        }             

        //Private methods       
        function changeShowedSecRemained(alertId) {
            if ($scope.alerts.length > 0) {
                for (var i = 0; i < $scope.alerts.length; i++) {
                    if ($scope.alerts[i].id == alertId) {
                        if ($scope.alerts[i].timerSec == 0) {
                            closeAlert($scope.alerts[i].id);
                            return;
                        }
                        $scope.alerts[i].timerSec = $scope.alerts[i].timerSec - 999;
                        if ($scope.alerts[i].timerSec <= 10) {
                            alertService.setBeginHideAnimation(true, $scope.alerts[i].id);
                        }                      
                        $timeout(function () {
                            changeShowedSecRemained(alertId);
                        }, 1000);                        
                        return;
                    }
                }               
            }
        }

        // Array Remove - By John Resig (MIT Licensed)
        Array.prototype.remove = function (from, to) {
            var rest = this.slice((to || from) + 1 || this.length);
            this.length = from < 0 ? this.length + from : from;
            return this.push.apply(this, rest);
        };

        function updateAlerts() {
            $scope.alerts = alertService.getAlerts();            //... и тогда контроллер обновит свои alerts и т.д.
            if (typeof $scope.alerts != 'undefined') {
                for (var i = 0; i < $scope.alerts.length; i++) {
                    if ($scope.alerts[i].timerSecRemainedStarted == false) {
                        alertService.setTimerSecRemainedStartedToAlert(true, $scope.alerts[i].id);
                        changeShowedSecRemained($scope.alerts[i].id);                        
                    }
                }
            }
        }

        alertService.registerObserverCallback(updateAlerts); //Теперь сервис уведомит, если allAlerts в сервисе изменится ...
    }
})();
