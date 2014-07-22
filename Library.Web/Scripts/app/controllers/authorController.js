(function () {
    'use strict';

    var controllerId = 'authorController';

    angular.module('app').controller(controllerId,
        ['authorService', 'alertService', 'loadingStatusService', 'strReprs', 'errorsHandler', authorController]);        // !!!!!!!!!!!!      В каком порядке тут DI внедряем   (и имя функции - куда - всегда последнее!)

    function authorController(authorService, alertService, loadingStatusService, strReprs, errorsHandler) {                         // !!!!!   ... в таком и тут!
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var vm = this;

        //Interface :)
        vm.getAuthors = getAuthors;
        vm.addAuthor = addAuthor;
        vm.updateAuthor = updateAuthor;
        vm.deleteAuthor = deleteAuthor;
        vm.loadBookList = loadBookList;
        vm.showEditFields = showEditFields;
        vm.showDeleteConfirm = showDeleteConfirm;
        vm.changeSelectedRow = changeSelectedRow;
        vm.deleteSelectedAuthor = deleteSelectedAuthor;

        // Bindable public properties
        vm.authors = [];
        vm.newAuthorName = "";
        vm.newAuthorLastName = "";
        vm.updateAuthorName = "";
        vm.updateAuthorLastName = "";
        vm.contentIsLoaded = false;
        vm.userActionsBtns = { bookListBtn: false, editBtn: false, deleteBtn: false };
        vm.userActionsBtnState = ''; //for store state of button
        vm.paginationTotalItems = 0;
        vm.paginationCurrentPage = 1;
        vm.paginationMaxSizeOfVisPage = 5;
        vm.paginationPageSize = 5;
                                //фильтр (походу пусть глобальный будет - тот что был), список книжек автора,  потом переходить к книжкам
        //Private fields        //когда начну делать книжки - см. список Скращука        
        var startIndex = 0;        
        var sorting = "Name ASC";
        var selectedRow = 2;

        //Public methods
        //---CRUD operations:
        function getAuthors() {
            vm.contentIsLoaded = false;
            loadingStatusService.showLoading();
            startIndex = (vm.paginationCurrentPage - 1) * vm.paginationPageSize;
            var params = {
                startIndex: startIndex,
                pageSize: vm.paginationPageSize,
                sorting: sorting,
                filtering: -1
            }
            authorService.getAuthors(params).then(
                function (data) {
                    vm.contentIsLoaded = true;
                    loadingStatusService.hideLoading();
                    vm.authors = data.Records;
                    vm.paginationTotalItems = data.TotalRecordCount;                   
                },
                function (error, status) {
                    vm.contentIsLoaded = true;
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }

        function addAuthor() {
            loadingStatusService.showLoading();
            var newAuthor = {
                Name: vm.newAuthorName + " " + vm.newAuthorLastName
            };
            authorService.addAuthor(newAuthor).then(
                function (data) {
                    loadingStatusService.hideLoading();
                    if (data.Result == "OK") {
                        getAuthors();
                        vm.newAuthorName = '';
                        vm.newAuthorLastName = '';
                        alertService.addAlert('success', strReprs.getNewRecordAdded(newAuthor.Name));
                    }
                    else {
                        alertService.addAlert('danger', strReprs.getRecordAddingFail(newAuthor.Name, data.Message));
                    }
                },
                function (error, status) {
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }

        function updateAuthor() {
            loadingStatusService.showLoading();
            var updated = {
                AuthorId: vm.authors[selectedRow].AuthorId,
                Name: vm.updateAuthorName + " " + vm.updateAuthorLastName
            };
            authorService.updateAuthor(updated).then(
                function (data) {
                    loadingStatusService.hideLoading();
                    if (data.Result == "OK") {
                        getAuthors();
                        vm.updateAuthorName = '';
                        vm.updateAuthorLastName = '';
                        alertService.addAlert('success', strReprs.getChangesSaved(updated.Name));
                    }
                    else {
                        alertService.addAlert('danger', strReprs.getRecordChangeFail(updated.Name, data.Message));
                    }
                },
                function (error, status) {
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }

        //private:
        function deleteAuthor(id) {
            loadingStatusService.showLoading();           
            var author = vm.authors[selectedRow].Name;
            authorService.deleteAuthor(id).then(
                function (data) {
                    loadingStatusService.hideLoading();
                    if (data.Result == "OK") {
                        getAuthors();
                        alertService.addAlert('success', strReprs.getRecordDeleted(author));
                    }
                    else {
                        alertService.addAlert('danger', strReprs.getRecordDeletingFail(author, data.Message));
                    }
                },
                function (error, status) {
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }
        //---

        //---Button's handlers of events
        function loadBookList() {
            userActionsBtnsToFalse();
            vm.userActionsBtns.bookListBtn = true;
            
        }

        function showEditFields() {
            var nameAndLastName = vm.authors[selectedRow].Name.split(" ");
            vm.updateAuthorName = nameAndLastName[0];
            vm.updateAuthorLastName = nameAndLastName[1];
            userActionsBtnsToFalse();
            vm.userActionsBtns.editBtn = true;            
        }

        function showDeleteConfirm() {
            userActionsBtnsToFalse();
            vm.userActionsBtns.deleteBtn = true;
        }
        //---

        function changeSelectedRow(row) {
            selectedRow = row;
            userActionsBtnsToFalse();
            
        }

        function deleteSelectedAuthor() {
            var id = vm.authors[selectedRow].AuthorId;
            deleteAuthor(id);
        }

        //Private methods
        function userActionsBtnsToFalse() {
            vm.userActionsBtns.bookListBtn = false;
            vm.userActionsBtns.editBtn = false;
            vm.userActionsBtns.deleteBtn = false;
        }
    }
})();
