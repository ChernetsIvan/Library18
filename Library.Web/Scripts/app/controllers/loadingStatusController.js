(function () {
    'use strict';
    var controllerId = 'loadingStatusController';

    angular.module('app').controller(controllerId,
        ['loadingStatusService', '$scope', loadingStatusController]);              //order is important (see in authorController)

    function loadingStatusController(loadingStatusService, $scope) {

        //Bindable public methods - as interface :)       
        $scope.init = init;

        // Bindable public properties
        $scope.loadingStatus = false;

        //Public methods
        function init() {

        }       

        //Private methods
        function updateLoadingStatus() {
            $scope.loadingStatus = loadingStatusService.getStatus();           
        }

        loadingStatusService.registerObserverCallback(updateLoadingStatus);
    }
})();
