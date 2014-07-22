(function () {
    'use strict';
    var utilityId = 'strReprs';       //String Representations - строковые представления
                                      //место хранения/формирования строковых сообщений
    angular.module('app').factory(utilityId, [strReprs]);

    function strReprs() {
        //Private
        var connectionFailed = 'Problem with connecting to the server';
        var loginSuccess = 'Hello, you authorized as';
        var registerSuccess = 'New user has been successfully registered';
        var successfulAdded = 'was successful added';
        var successfulDeleted = 'was successful deleted';
        var changesToRecord = 'Changes to record';
        var notSave = 'were not save';
        var notAdded = 'was not added';
        var notDeleted = 'was not deleted';
        var reason = 'Reason:';
        var recordG = 'Record';
        var errorG = "Error. ";
        var unauthorized = "You must authorize first to perform this request";
        var loginFailed = "The user name or password are incorrect";
        var registerFailed = "Check completed fields on correctness";


        // Define the functions and properties to reveal.
        var service = {
            getConnectionFailed: getConnectionFailed,
            getLoginSuccess: getLoginSuccess,
            getRegisterSuccess: getRegisterSuccess,
            getNewRecordAdded: getNewRecordAdded,
            getRecordAddingFail: getRecordAddingFail,
            getChangesSaved: getChangesSaved,
            getRecordChangeFail: getRecordChangeFail,
            getRecordDeleted: getRecordDeleted,
            getRecordDeletingFail: getRecordDeletingFail,
            getUnauthorized: getUnauthorized,
            getLoginFailed: getLoginFailed,
            getRegisterFailed: getRegisterFailed
    };
        return service;

        //Public
        //Account actions
        function getConnectionFailed() {
            return errorG + connectionFailed;
        }

        function getLoginSuccess(userName) {
            return loginSuccess + ' ' + userName;
        }

        function getRegisterSuccess() {
            return registerSuccess;
        }

        //CRUD operations with records
        function getNewRecordAdded(record) {
            return recordG + ' ' + record + ' ' + successfulAdded;
        }

        function getRecordAddingFail(record, msg) {
            return errorG + recordG + ' ' + record + ' ' + notAdded + '. ' + reason + msg;
        }
        
        function getChangesSaved(record) {
            return changesToRecord + ' ' + record + ' ' + successfulAdded;
        }

        function getRecordChangeFail(record, msg) {
            return errorG + changesToRecord + ' ' + record + ' ' + notSave + '. ' + reason + msg;
        }
        
        function getRecordDeleted(record) {
            return recordG + ' ' + record + ' ' + successfulDeleted;
        }

        function getRecordDeletingFail(record, msg) {
            return errorG + recordG + ' ' + record + ' ' + notDeleted + '. ' + reason + msg;
        }

        function getUnauthorized() {
            return errorG + unauthorized;
        }

        function getLoginFailed() {
            return errorG + loginFailed;
        }

        function getRegisterFailed() {
            return errorG + registerFailed;
        }
    }
})();