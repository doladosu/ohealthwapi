(function () {

    var value = {
      serviceBase: 'http://testweb.evoqapps.com/wapi/',
      reportingServicesUrl: 'http://54.175.38.46/Reports/',
      clientId: 'evoqWeb'
    };

    angular.module('customersApp').value('config', value);

}());