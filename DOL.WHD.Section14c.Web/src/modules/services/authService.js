'use strict';

module.exports = function(ngModule) {
  ngModule.service('authService', function(
    stateService,
    apiService,
    autoSaveService,
    $q,
    $location,
    _env,
    $http
  ) {
    'use strict';

    this.userLogin = function(email, password) {
      const self = this;
      const url = _env.api_url + '/Token';
      const d = $q.defer();

      $http({
        method: 'POST',
        url: url,
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        data: $.param({
          grant_type: 'password',
          userName: email,
          password: password
        })
      }).then(
        function successCallback(result) {
          const data = result.data;
          stateService.access_token = data.access_token;
          self.authenticateUser().then(
            function() {
              d.resolve();
            },
            function(error) {
              d.reject(error);
            }
          );
        },
        function errorCallback(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.authenticateUser = function() {
      const d = $q.defer();
      // Get User Info
      apiService.userInfo(stateService.access_token).then(
        function(result) {
          const data = result.data;
          stateService.loggedIn = true;
          stateService.user = data;
          if(result.data.organizations.length > 0) {
            var organization = data.organizations.reduce(function(a,b){
              if(a.applicationStatus.name === 'InProgress') {
                return a;
              } else {
                return b;
              }
            });
            stateService.ein = organization.ein;
            stateService.employerId = organization.employer.id;
            stateService.applicationId = organization.applicationId;
            stateService.employerName = organization.employer.legalName;
          }
          if (!stateService.IsPointOfContact) {
            d.resolve();
            return;
          } else {
            stateService.loadApplicationList().then(
              function() {
                d.resolve();
              },
              function(error) {
                d.reject(error);
              }
            );
          }
        },
        function(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };
  });
};
