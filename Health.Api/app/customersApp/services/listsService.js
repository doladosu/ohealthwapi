(function() {

    var injectParams = ['$http', '$q'];

    var listsFactory = function($http, $q) {
        var serviceBase = '/api/',
            factory = {};


        factory.getStates = function() {
            return $http.get(serviceBase + 'list/states').then(
                function(results) {
                    return results.data;
                });
        };

        return factory;
    };

    listsFactory.$inject = injectParams;

    angular.module('customersApp').factory('listsService', listsFactory);

}());