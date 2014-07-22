(function () {
    'use strict';

    var controllerId = 'bookFullController';

    angular.module('app').controller(controllerId,
        ['bookService', 'authorService', 'alertService', 'loadingStatusService', 'strReprs', 'errorsHandler', bookController]); 

    function bookController(bookService, authorService, alertService, loadingStatusService, strReprs, errorsHandler) {
        // Using 'Controller As' syntax, so we assign this to the vm variable (for viewmodel).
        var vm = this;

        //Interface :)
        vm.init = init;
        vm.getBooks = getBooks;
        vm.addBook = addBook;
        vm.updateBook = updateBook;
        vm.deleteBook = deleteBook;

        vm.showDetails = showDetails;
        vm.showTakeBookFields = showTakeBookFields;
        vm.showGiveBookFields = showGiveBookFields;
        vm.showQr = showQr;
        vm.showEditFields = showEditFields;
        vm.showDeleteConfirm = showDeleteConfirm;

        vm.changeSelectedRow = changeSelectedRow;
        vm.deleteSelectedBook = deleteSelectedBook;
        vm.showAddFields = showAddFields;
        vm.cancelAddNewBook = cancelAddNewBook;
        vm.getFilteredAuthors = getFilteredAuthors;
        vm.addSelectedAuthor = addSelectedAuthor;
        vm.deleteSelectedAuthor = deleteSelectedAuthor;

        // Bindable public properties
        vm.books = [];
        vm.newBookTitle = "";
        vm.newBookAuthors = [];
        vm.newBookIsbn = "";
        vm.newBookYear = "";
        vm.newBookDescription = "";
        vm.newBookPagesAmount = "";
        vm.newBookPublishingHouse = "";
        vm.newBookBookAmount = "";

        vm.updateBookTitle = "";
        vm.updateBookAuthors = [];
        vm.updateBookIsbn = "";
        vm.updateBookYear = "";
        vm.updateBookDescription = "";
        vm.updateBookPagesAmount = "";
        vm.updateBookPublishingHouse = "";
        vm.updateBookBookAmount = "";

        vm.contentIsLoaded = false;
        vm.userActionsBtns = { detailsBtn: false, takeBookBtn: false, giveBookBtn: false, qrBtn: false, editBookBtn: false, deleteBookBtn: false };
        vm.userActionsBtnState = ''; //for store state of button
        vm.paginationTotalItems = 0;
        vm.showAddBookFields = false;
        vm.typeAheadAuthors = [];
        vm.oneAddedAuthor = author;
        vm.showTypeAheadLoadingImg = false;

        //Private fields 
        var startIndex = 0;
        var pageSize = 30;
        var sorting = "Name ASC";
        var selectedRow = 1;
        var author = { AuthorId: '', Name: '' }
        var countOfFilteringAuthors = 5; //filtering authors

        function init() {
            getBooks();
        }

        //Public methods
        //---CRUD operations:
        function getBooks() {
            vm.contentIsLoaded = false;
            loadingStatusService.showLoading();
            var params = {
                startIndex: startIndex,
                pageSize: pageSize,
                sorting: sorting
            }
            bookService.getBooks(params).then(
                function (data) {
                    vm.contentIsLoaded = true;
                    loadingStatusService.hideLoading();
                    vm.books = data.Records;
                    vm.paginationTotalItems = data.TotalRecordCount;
                },
                function (error, status) {
                    vm.contentIsLoaded = true;
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }

        function addBook() {
            loadingStatusService.showLoading();
            var newBook = {
                Title: vm.newBookTitle,
                Authors: vm.newBookAuthors,
                Isbn: vm.newBookIsbn,
                Year: vm.newBookYear,
                Description: vm.newBookDescription,
                PagesAmount: vm.newBookPagesAmount,
                PublishingHouse: vm.newBookPublishingHouse,
                BookAmount: vm.newBookBookAmount
            };
            bookService.addBook(newBook).then(
                function (data) {
                    loadingStatusService.hideLoading();
                    if (data.Result == "OK") {
                        getBooks();
                        vm.showAddBookFields = false;
                        clearNewInputs();
                        alertService.addAlert('success', strReprs.getNewRecordAdded(newBook.Title));
                    }
                    else {
                        alertService.addAlert('danger', strReprs.getRecordAddingFail(newBook.Title, data.Message));
                    }
                },
                function (error, status) {
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }

        function updateBook() {
            loadingStatusService.showLoading();
            var updated = {
                BookId: vm.Books[selectedRow].BookId,
                Name: vm.updateBookName + " " + vm.updateBookLastName
            };
            bookService.updateBook(updated).then(
                function (data) {
                    loadingStatusService.hideLoading();
                    if (data.Result == "OK") {
                        getBooks();
                        vm.updateBookName = '';
                        vm.updateBookLastName = '';
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
        function deleteBook(id) {
            loadingStatusService.showLoading();
            var Book = vm.Books[selectedRow].Name;
            bookService.deleteBook(id).then(
                function (data) {
                    loadingStatusService.hideLoading();
                    if (data.Result == "OK") {
                        getBooks();
                        alertService.addAlert('success', strReprs.getRecordDeleted(Book));
                    }
                    else {
                        alertService.addAlert('danger', strReprs.getRecordDeletingFail(Book, data.Message));
                    }
                },
                function (error, status) {
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }
        //---

        //---Button's handlers of events
        function showDetails() {
            clearUserActionBtns();
            vm.userActionsBtns.detailsBtn = true;

        }

        function showTakeBookFields() {
            clearUserActionBtns();
            vm.userActionsBtns.takeBookBtn = true;

        }

        function showGiveBookFields() {
            clearUserActionBtns();
            vm.userActionsBtns.giveBookBtn = true;

        }

        function showQr() {
            clearUserActionBtns();
            vm.userActionsBtns.qrBtn = true;

        }

        function showEditFields() {
            //var nameAndLastName = vm.Books[selectedRow].Name.split(" ");
            //vm.updateBookName = nameAndLastName[0];
            //vm.updateBookLastName = nameAndLastName[1];
            clearUserActionBtns();
            vm.userActionsBtns.editBookBtn = true;
        }

        function showDeleteConfirm() {
            clearUserActionBtns();
            vm.userActionsBtns.deleteBookBtn = true;
        }

        //---

        function changeSelectedRow(row) {
            selectedRow = row;
            clearUserActionBtns();
        }

        function deleteSelectedBook() {
            var id = vm.Books[selectedRow].BookId;
            deleteBook(id);
        }

        function showAddFields() {
            vm.showAddBookFields = true;
        }

        function cancelAddNewBook() {
            vm.showAddBookFields = false;
            clearNewInputs();
        }

        function getFilteredAuthors(filter) {
            vm.showTypeAheadLoadingImg = true;
            var params = {
                startIndex: -1,
                pageSize: countOfFilteringAuthors,
                sorting: -1,
                filtering: filter
            }
            return authorService.getAuthors(params).then(
                function (data) {
                    vm.showTypeAheadLoadingImg = false;
                    vm.typeAheadAuthors = [];
                    angular.forEach(data.Records, function (item) {
                        if (authorAlreadyExist(item.AuthorId) == false) {
                            vm.typeAheadAuthors.push(item);
                        }
                    });
                    return vm.typeAheadAuthors;
                },
                function (error, status) {
                    vm.showTypeAheadLoadingImg = false;
                    loadingStatusService.hideLoading();
                    errorsHandler.handleError(error);
                });
        }

        function addSelectedAuthor(item) {
            vm.newBookAuthors.push(item);
            vm.oneAddedAuthor = author;
        }

        function deleteSelectedAuthor(id) {
            for (var i = 0; i < vm.newBookAuthors.length; i++) {
                if (vm.newBookAuthors[i].AuthorId == id) {
                    vm.newBookAuthors.remove(i);
                    return;
                }
            };
        }

        //Private methods
        function clearUserActionBtns() {
            vm.userActionsBtns.detailsBtn = false;
            vm.userActionsBtns.takeBookBtn = false;
            vm.userActionsBtns.giveBookBtn = false;
            vm.userActionsBtns.qrBtn = false;
            vm.userActionsBtns.editBookBtn = false;
            vm.userActionsBtns.deleteBookBtn = false;
        }

        function clearNewInputs() {
            vm.newBookTitle = "";
            vm.newBookAuthors = [];
            vm.newBookIsbn = "";
            vm.newBookYear = "";
            vm.newBookDescription = "";
            vm.newBookPagesAmount = "";
            vm.newBookPublishingHouse = "";
            vm.newBookBookAmount = "";
        }

        function clearUpdateInputs() {
            vm.updateBookTitle = "";
            vm.updateBookAuthors = [];
            vm.updateBookIsbn = "";
            vm.updateBookYear = "";
            vm.updateBookDescription = "";
            vm.updateBookPagesAmount = "";
            vm.updateBookPublishingHouse = "";
            vm.updateBookBookAmount = "";
        }

        function authorAlreadyExist(id) {
            for (var i = 0; i < vm.newBookAuthors.length; i ++) {
                if (vm.newBookAuthors[i].AuthorId == id) {
                    return true;
                }
            }
            return false;
        }

        // Array Remove - By John Resig (MIT Licensed)
        Array.prototype.remove = function (from, to) {
            var rest = this.slice((to || from) + 1 || this.length);
            this.length = from < 0 ? this.length + from : from;
            return this.push.apply(this, rest);
        };
    }
})();
