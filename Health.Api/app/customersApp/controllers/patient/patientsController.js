(function () {

    var injectParams = ['$location', '$filter', '$window',
                        '$timeout', 'authService', 'patientsService', 'modalService'];

    var patientsController = function ($location, $filter, $window, $timeout, authService, patientsService, modalService) {

        var vm = this;

        vm.patients = [];
        vm.filteredPatients = [];
        vm.filteredCount = 0;
        vm.orderby = 'lastName';
        vm.reverse = false;
        vm.searchText = null;
        vm.cardAnimationClass = '.card-animation';

        //paging
        vm.totalRecords = 0;
        vm.pageSize = 10;
        vm.currentPage = 1;

        function filterPatients(filterText) {
          vm.filteredPatients = $filter("nameCityStateFilter")(vm.patients, filterText);
          vm.filteredCount = (vm.filteredPatients) ? vm.filteredPatients.length : 0;
        }

        function getPatients() {
          patientsService.getPatients(vm.currentPage - 1, vm.pageSize)
          .then(function (data) {
            vm.totalRecords = data.totalRecords;
            vm.patients = data.results;
            filterPatients(''); //Trigger initial filter

            $timeout(function () {
              vm.cardAnimationClass = ''; //Turn off animation since it won't keep up with filtering
            }, 1000);

          }, function (error) {
            $window.alert('Sorry, an error occurred: ' + error.data.message);
          });
        }

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getPatients();
        };

        function getPatientById(id) {
          for (var i = 0; i < vm.patients.length; i++) {
            var cust = vm.patients[i];
            if (cust.id === id) {
              return cust;
            }
          }
          return null;
        }

        vm.deletePatient = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var cust = getPatientById(id);
            var custName = cust.firstName + ' ' + cust.lastName;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Customer',
                headerText: 'Delete ' + custName + '?',
                bodyText: 'Are you sure you want to delete this customer?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    patientsService.deleteCustomer(id).then(function () {
                        for (var i = 0; i < vm.patients.length; i++) {
                            if (vm.patients[i].id === id) {
                                vm.patients.splice(i, 1);
                                break;
                            }
                        }
                        filterPatients(vm.searchText);
                    }, function (error) {
                        $window.alert('Error deleting customer: ' + error.message);
                    });
                }
            });
        };

        vm.DisplayModeEnum = {
            Card: 0,
            List: 1
        };

        vm.changeDisplayMode = function (displayMode) {
            switch (displayMode) {
                case vm.DisplayModeEnum.Card:
                    vm.listDisplayModeEnabled = false;
                    break;
                case vm.DisplayModeEnum.List:
                    vm.listDisplayModeEnabled = true;
                    break;
            }
        };

        vm.navigate = function (url) {
            $location.path(url);
        };

        vm.setOrder = function (orderby) {
            if (orderby === vm.orderby) {
                vm.reverse = !vm.reverse;
            }
            vm.orderby = orderby;
        };

        vm.searchTextChanged = function () {
          filterPatients(vm.searchText);
        };

        function init() {
            //createWatches();
            getPatients();
        }

        //function createWatches() {
        //    //Watch searchText value and pass it and the customers to nameCityStateFilter
        //    //Doing this instead of adding the filter to ng-repeat allows it to only be run once (rather than twice)
        //    //while also accessing the filtered count via vm.filteredCount above

        //    //Better to handle this using ng-change on <input>. See searchTextChanged() function.
        //    vm.$watch("searchText", function (filterText) {
        //        filterCustomers(filterText);
        //    });
        //}

        init();
    };

    patientsController.$inject = injectParams;

    angular.module('customersApp').controller('PatientsController', patientsController);

}());
