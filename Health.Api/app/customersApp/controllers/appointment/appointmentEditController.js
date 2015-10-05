(function () {

    var injectParams = ['$scope', '$location', '$routeParams',
                        '$timeout', 'config', 'appointmentsService', 'modalService', 'listsService'];

    var appointmentEditController = function ($scope, $location, $routeParams,
                                           $timeout, config, appointmentsService, modalService, listsService) {

        var vm = this,
            appointmentId = ($routeParams.appointmentId) ? parseInt($routeParams.appointmentId) : 0,
            timer,
            onRouteChangeOff;

        vm.appointment = {};
        vm.states = [];
        vm.title = (appointmentId > 0) ? 'Edit' : 'Add';
        vm.buttonText = (appointmentId > 0) ? 'Update' : 'Add';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.isStateSelected = function (customerStateId, stateId) {
            return customerStateId === stateId;
        };

        function startTimer() {
            timer = $timeout(function () {
                $timeout.cancel(timer);
                vm.errorMessage = '';
                vm.updateStatus = false;
            }, 3000);
        }

        function processSuccess() {
            $scope.editForm.$dirty = false;
            vm.updateStatus = true;
            vm.title = 'Edit';
            vm.buttonText = 'Update';
            startTimer();
        }

        function processError(error) {
            vm.errorMessage = error.message;
            startTimer();
        }

        vm.saveAppointment = function () {
            if ($scope.editForm.$valid) {
                if (!vm.customer.id) {
                    appointmentsService.insertCustomer(vm.appointment).then(processSuccess, processError);
                }
                else {
                    appointmentsService.updateCustomer(vm.appointment).then(processSuccess, processError);
                }
            }
        };

        function getStates() {
          return listsService.getStates().then(function (states) {
            vm.states = states;
          }, processError);
        }

        vm.deleteAppointment = function () {
            var custName = vm.appointment.firstName + ' ' + vm.appointment.lastName;
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Cancel Appointment',
                headerText: 'Cancel ' + custName + '?',
                bodyText: 'Are you sure you want to cancel this appointment?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    appointmentsService.deleteAppointment(vm.appointment.id).then(function () {
                        onRouteChangeOff(); //Stop listening for location changes
                        $location.path('/customers');
                    }, processError);
                }
            });
        };

        function routeChange(event, newUrl, oldUrl) {
            //Navigate to newUrl if the form isn't dirty
            if (!vm.editForm || !vm.editForm.$dirty) return;

            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Ignore Changes',
                headerText: 'Unsaved Changes',
                bodyText: 'You have unsaved changes. Leave the page?'
            };

            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    onRouteChangeOff(); //Stop listening for location changes
                    $location.path($location.url(newUrl).hash()); //Go to page they're interested in
                }
            });

            //prevent navigation by default since we'll handle it
            //once the user selects a dialog option
            event.preventDefault();
            return;
        }

        function init() {

            getStates().then(function () {
                if (appointmentId > 0) {
                    appointmentsService.getAppointment(appointmentId).then(function (appointment) {
                        vm.appointment = appointment;
                    }, processError);
                } else {
                    appointmentsService.newAppointment().then(function (appointment) {
                        vm.appointment = appointment;
                    });
                }
            });


            //Make sure they're warned if they made a change but didn't save it
            //Call to $on returns a "deregistration" function that can be called to
            //remove the listener (see routeChange() for an example of using it)
            onRouteChangeOff = $scope.$on('$locationChangeStart', routeChange);
        }

        init();
    };

    appointmentEditController.$inject = injectParams;

    angular.module('customersApp').controller('AppointmentEditController', appointmentEditController);
}());