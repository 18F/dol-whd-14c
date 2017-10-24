'use strict';

module.exports = function(ngModule) {
  ngModule.service('authService', function(
    stateService,
    apiService,
    autoSaveService,
    $q,
    _env,
    $http
  ) {
    'use strict';

    this.userLogin = function(email, password) {
      const self = this;
      const url = _env.api_url + '/Token';
      const mockUrl = _env.api_url + '/'; // Temporary fix for IE 11
      const d = $q.defer();
      $http({
        method: 'GET',
        url: mockUrl
      }).then(function(){
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
            //console.log(error);
            d.reject(error);
          }
        );
      })
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
          if (data.organizations.length > 0) {
            stateService.ein = data.organizations[0].ein; //TODO: Add EIN selection?
          }
          if (!stateService.isAdmin) {
            stateService.loadSavedApplication().then(
              function() {
                // start auto-save
                if (stateService.ein) {
                  autoSaveService.start();
                }
                d.resolve();
              },
              function(error) {
                d.reject(error);
              }
            );
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
