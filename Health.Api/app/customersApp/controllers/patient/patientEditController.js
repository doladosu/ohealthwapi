﻿(function() {

    var injectParams = [
        '$scope', '$location', '$routeParams',
        '$timeout', 'config', 'patientsService', 'modalService', 'listsService'
    ];

    var patientEditController = function($scope, $location, $routeParams,
        $timeout, config, patientsService, modalService, listsService) {

        var vm = this,
            patientId = ($routeParams.patientId) ? parseInt($routeParams.patientId) : 0,
            timer,
            onRouteChangeOff;

        vm.patient = {};
        vm.states = [];
        vm.title = (patientId > 0) ? 'Edit' : 'Add';
        vm.buttonText = (patientId > 0) ? 'Update' : 'Add';
        vm.updateStatus = false;
        vm.errorMessage = '';

        vm.isStateSelected = function (patientStateId, stateId) {
            return patientStateId === stateId;
        };

        function startTimer() {
            timer = $timeout(function() {
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

        vm.savePatient = function () {
            if ($scope.editForm.$valid) {
                if (!vm.patient.id) {
                    patientsService.insertCustomer(vm.patient).then(processSuccess, processError);
                } else {
                    patientsService.updateCustomer(vm.patient).then(processSuccess, processError);
                }
            }
        };

        function getStates() {
            return listsService.getStates().then(function(states) {
                vm.states = states;
            }, processError);
        }

        vm.deletePatient = function () {
            var custName = vm.patient.firstName + ' ' + vm.patient.lastName;
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete patient',
                headerText: 'Delete ' + custName + '?',
                bodyText: 'Are you sure you want to delete this patient?'
            };

            modalService.showModal({}, modalOptions).then(function(result) {
                if (result === 'ok') {
                    patientsService.deletePatient(vm.customer.id).then(function () {
                        onRouteChangeOff(); //Stop listening for location changes
                        $location.path('/patients');
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

            getStates().then(function() {
                if (patientId > 0) {
                    patientsService.getCustomer(patientId).then(function (patient) {
                        vm.patient = patient;
                    }, processError);
                } else {
                    patientsService.newPatient().then(function (patient) {
                        vm.patient = patient;
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

    patientEditController.$inject = injectParams;

    angular.module('customersApp').controller('PatientEditController', patientEditController);

}());