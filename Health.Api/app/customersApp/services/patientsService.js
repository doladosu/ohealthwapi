﻿(function () {

    var injectParams = ['$http', '$q'];

    var patientsFactory = function ($http, $q) {
        var serviceBase = '/api/',
            factory = {};

        function buildPagingUri(pageIndex, pageSize) {
          var uri = '?$top=' + pageSize + '&$skip=' + (pageIndex * pageSize);
          return uri;
        }

        function ordersTotal(customer) {
          var total = 0;
          var orders = customer.orders;
          var count = orders.length;

          for (var i = 0; i < count; i++) {
            total += orders[i].orderTotal;
          }
          return total;
        };

        function extendCustomers(customers) {
          var custsLen = customers.length;
          //Iterate through customers
          for (var i = 0; i < custsLen; i++) {
            var cust = customers[i];
            if (!cust.orders) continue;

            var ordersLen = cust.orders.length;
            for (var j = 0; j < ordersLen; j++) {
              var order = cust.orders[j];
              order.orderTotal = order.quantity * order.price;
            }
            cust.ordersTotal = ordersTotal(cust);
          }
        }

        function getPagedResource(baseResource, pageIndex, pageSize) {
          var resource = baseResource;
          resource += (arguments.length == 3) ? buildPagingUri(pageIndex, pageSize) : '';
          return $http.get(serviceBase + resource).then(function (response) {
            var custs = response.data;
            //extendCustomers(custs);
            return {
              totalRecords: parseInt(response.headers('X-InlineCount')),
              results: custs
            };
          });
        }

        factory.getPatients = function (pageIndex, pageSize) {
          return getPagedResource('Patients', pageIndex, pageSize);
        };

        factory.checkUniqueValue = function (id, property, value) {
            if (!id) id = 0;
            return $http.get(serviceBase + 'checkUnique/' + id + '?property=' + property + '&value=' + escape(value)).then(
                function (results) {
                    return results.data.status;
                });
        };

        factory.insertPatient = function (patient) {
            return $http.post(serviceBase + 'patient', patient).then(function (results) {
                patient.id = results.data.id;
                return results.data;
            });
        };

        factory.newPatient = function () {
            return $q.when({id: 0});
        };

        factory.updatePatient = function (customer) {
            return $http.put(serviceBase + 'putCustomer/' + customer.id, customer).then(function (status) {
                return status.data;
            });
        };

        factory.deletePatient = function (id) {
            return $http.delete(serviceBase + 'deleteCustomer/' + id).then(function (status) {
                return status.data;
            });
        };

        factory.getPatient = function (id) {
            //then does not unwrap data so must go through .data property
            //success unwraps data automatically (no need to call .data property)
            return $http.get(serviceBase + 'customerById/' + id).then(function (results) {
                //extendCustomers([results.data]);
                return results.data;
            });
        };

        // is this still used???
        function orderTotal(order) {
            return order.quantity * order.price;
        };



        return factory;
    };

    patientsFactory.$inject = injectParams;

    angular.module('customersApp').factory('patientsService', patientsFactory);

}());