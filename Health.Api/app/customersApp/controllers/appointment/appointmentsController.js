(function () {

    var injectParams = ['$location', '$filter', '$window',
                        '$timeout', 'authService', 'appointmentsService', 'modalService', 'listsService'];

    var appointmentsController = function ($location, $filter, $window, $timeout, authService, appointmentsService, modalService, listsService) {

        var vm = this;

        vm.appointments = [];
        vm.filteredAppointments = [];
        vm.filteredCount = 0;
        vm.orderby = 'lastName';
        vm.reverse = false;
        vm.searchText = null;
        vm.cardAnimationClass = '.card-animation';

        //paging
        vm.totalRecords = 0;
        vm.pageSize = 10;
        vm.currentPage = 1;

        function filterAppointments (filterText) {
          vm.filteredPatients = $filter("nameCityStateFilter")(vm.patients, filterText);
          vm.filteredCount = (vm.filteredPatients) ? vm.filteredPatients.length : 0;
        }

        function getAppointments() {
            appointmentsService.getAppointments(vm.currentPage - 1, vm.pageSize)
          .then(function (data) {
            vm.totalRecords = data.totalRecords;
            vm.appointments = data.results;
            filteredAppointments(''); //Trigger initial filter

            $timeout(function () {
              vm.cardAnimationClass = ''; //Turn off animation since it won't keep up with filtering
            }, 1000);

          }, function (error) {
            $window.alert('Sorry, an error occurred: ' + error.data.message);
          });
        }

        vm.pageChanged = function (page) {
            vm.currentPage = page;
            getAppointments();
        };

        function getAppointmentById(id) {
          for (var i = 0; i < vm.patients.length; i++) {
            var cust = vm.patients[i];
            if (cust.id === id) {
              return cust;
            }
          }
          return null;
        }

        vm.deleteAppointment = function (id) {
            if (!authService.user.isAuthenticated) {
                $location.path(authService.loginPath + $location.$$path);
                return;
            }

            var cust = getAppointmentById(id);
            var custName = cust.firstName + ' ' + cust.lastName;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Customer',
                headerText: 'Delete ' + custName + '?',
                bodyText: 'Are you sure you want to delete this customer?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    appointmentsService.deleteAppointment(id).then(function () {
                        for (var i = 0; i < vm.appointments.length; i++) {
                            if (vm.appointments[i].id === id) {
                                vm.appointments.splice(i, 1);
                                break;
                            }
                        }
                        filterAppointments(vm.searchText);
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
            filterAppointments(vm.searchText);
        };

        function init() {
            //createWatches();
            getAppointments();
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

    appointmentsController.$inject = injectParams;

    angular.module('customersApp').controller('AppointmentsController', appointmentsController);

}());
