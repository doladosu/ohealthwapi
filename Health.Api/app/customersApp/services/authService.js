(function () {

  var injectParams = ['$http', '$rootScope', '$q', 'localStorageService', 'config', '$location', '$log'];

  var authFactory = function ($http, $rootScope, $q, localStorageService, config, $location, $log) {
    var serviceBase = config.serviceBase,
        factory = {
          loginPath: '/login',
          user: {
            isAuthenticated: false,
            userName: "",
            roles: null
          }
        };

    factory.login = function (userName, password) {
      var data = "grant_type=password&username=" + userName + "&password=" + password + "&client_id=" + config.clientId;
      var deferred = $q.defer();

      $http.post(serviceBase + 'login', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

        localStorageService.set('authorizationData', {
          token: response.access_token,
          userName: userName,
          roles: response.role,
          refreshToken: response.refresh_token,
          expires: Date.parse(response['.expires'])
        });
        factory.user.isAuthenticated = true;
        factory.user.userName = userName;
        factory.user.roles = response.role;
        $rootScope.$broadcast('loginStatusChanged', true);
        deferred.resolve(response);

      }).error(function (err, status) {
        factory.logout();
        deferred.reject(err);
      });

      return deferred.promise;
    };

    factory.logout = function () {
      localStorageService.remove('authorizationData');

      factory.user.isAuthenticated = false;
      factory.user.userName = "";
      $rootScope.$broadcast('loginStatusChanged', false);
    };

    // NOTE: NavbarController had this same functionality.
    // The authservice used to broadcast the redirect message
    // BUT, the navbarconfig didn't setup its listener until after this
    // So, bypass the man in the middle and just redirect here.
    factory.redirectToLogin = function () {
      // Remove all previous prepends to prevent things like: /login/login/login/login/templates
      var path = '/login' + $location.$$path.replace(/\/login/gi, '');
      path = path.replace(/\/{2,}/g, ''); // Collapse slashes
      $location.replace();
      $location.path(path);
    };

    factory.fillAuthData = function () {
      var authData = localStorageService.get('authorizationData');
      if (authData) {
        // Check token expiration
        if (Date.now() < authData.expires) {
          factory.user.isAuthenticated = true;
          factory.user.userName = authData.userName;
          factory.user.roles = authData.roles;
        } else {
          // Expired, no auth
        }

      }
    };

    factory.hasAuthRole = function (roles) {
      if (typeof factory.user.roles === 'undefined') {
        return false; // No data to validate against
      }

      if (typeof roles === 'string') {
        roles = [roles];
      }

      var rolesArray = factory.user.roles.split(',');
      for (var i = 0; i < rolesArray.length; i++) {
        var authRoles = rolesArray[i];
        if (roles.indexOf(authRoles) >= 0) {
          return true;
        }
      }

      return false;
    };

    factory.forgotPassword = function (userName) {
      var sUrl = serviceBase + 'api/security/forgotpassword/' + userName;
      return $http.post(sUrl, userName).then(function (response) {
        return response.data;
      });
    };

    factory.resetPassword = function (data) {
      var sUrl = serviceBase + 'api/security/resetpassword?id=' + data.Id + '&code=' + data.Code + '&password=' + data.Password;
      return $http.post(sUrl, data).then(function (response) {
        return response.data;
      });
    };

    return factory;
  };

  authFactory.$inject = injectParams;

  angular.module('customersApp').factory('authService', authFactory);

}());

