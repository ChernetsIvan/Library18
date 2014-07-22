(function () {
    'use strict';
    var controllerId = 'accountController';

    angular.module('app').controller(controllerId,
        ['accountService', 'alertService', 'strReprs', 'loadingStatusService', 'errorsHandler', accountController]);           //order is important (see in authorController)

    function accountController(accountService, alertService, strReprs, loadingStatusService, errorsHandler) {

        //Public
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var vm = this;

        // Bindable properties and functions are placed on vm.
        vm.isRegistered = false;
        vm.isLoggedIn = false;
        vm.userName = "";
        vm.bearerToken = "";
        vm.userData = { userName: "", password: "", confirmPassword : ""};

        //As interface :)
        vm.registerUser = registerUser;
        vm.loginUser = loginUser;

        function registerUser() {
            loadingStatusService.showLoading();
            accountService.registerUser(vm.userData).then(function (data) {
                vm.isRegistered = true;
                alertService.addAlert('success', strReprs.getRegisterSuccess());
                loadingStatusService.hideLoading();
            }, function (error, status) {
                vm.isRegistered = false;
                errorsHandler.handleError(error);
                loadingStatusService.hideLoading();
            });
        }

        function loginUser() {       //поправить здесь и в регистрации, чтобы когда при доступном сервере алерт был не "connection problem" a "authorization denied"
            loadingStatusService.showLoading();
            accountService.loginUser(vm.userData).then(function (data) {
                vm.isLoggedIn = true;
                vm.userName = data.userName;
                vm.bearerToken = data.access_token;
                alertService.addAlert('success', strReprs.getLoginSuccess(vm.userName));
                loadingStatusService.hideLoading();
            }, function (error, status) {
                vm.isLoggedIn = false;
                errorsHandler.handleError(error);
                loadingStatusService.hideLoading();
            });
        }
    }
})();
