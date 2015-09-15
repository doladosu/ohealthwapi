(function () {

    var value = {
      serviceBase: 'http://localhost:14543/api/',
      clientId: 'HealthWeb'
    };

    angular.module('customersApp').value('config', value);

}());