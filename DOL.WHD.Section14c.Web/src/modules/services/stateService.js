'use strict';

import merge from 'lodash/merge';
import some from 'lodash/some';
import omit from 'lodash/omit';
import isEmpty from 'lodash/isEmpty';

module.exports = function(ngModule) {
  ngModule.service('stateService', function(
    $cookies,
    moment,
    apiService,
    $q,
    _env,
    $rootScope,
    _constants
  ) {
    'use strict';

    const accessTokenCookieName = 'api_access_token';

    let state;
    setInitialState();

    /*** Properties ***/

    // user
    Object.defineProperty(this, 'user', {
      get: function() {
        return state.user;
      },
      set: function(value) {
        state.user = value;
      }
    });

    Object.defineProperty(this, 'ein', {
      get: function() {
        return state.activeEIN;
      },
      set: function(value) {
        state.activeEIN = value;
      }
    });

    Object.defineProperty(this, 'employerId', {
      get: function() {
        return state.activeEmployerId;
      },
      set: function(value) {
        state.activeEmployerId = value;
      }
    });

    Object.defineProperty(this, 'applicationId', {
      get: function() {
        return state.activeApplicationId;
      },
      set: function(value) {
        state.activeApplicationId = value;
      }
    });

    Object.defineProperty(this, 'loggedIn', {
      get: function() {
        return state.loggedIn;
      },
      set: function(value) {
        state.loggedIn = value;
      }
    });

    Object.defineProperty(this, 'IsPointOfContact', {
      get: function() {
        return this.hasClaim(_constants.applicationClaimTypes.viewAdminUI);
      }
    });

    // REST access token
    Object.defineProperty(this, 'access_token', {
      get: function() {
        return $cookies.get(accessTokenCookieName);
      },
      set: function(value) {
        $cookies.put(accessTokenCookieName, value, {
          secure: _env.requireHttps,
          expires: moment()
            .add(_env.tokenCookieDurationMinutes, 'm')
            .toDate()
        });
      }
    });

    // Core form data object model
    Object.defineProperty(this, 'formData', {
      get: function() {
        return state.form_data;
      },
      set: function(value) {
        state.form_data = value;
      }
    });

    Object.defineProperty(this, 'appData', {
      get: function() {
        return state.app_data;
      }
    });

    Object.defineProperty(this, 'appList', {
      get: function() {
        return state.app_list;
      }
    });

    this.hasClaim = function(claimName) {
      return some(state.user.applicationClaims, function(claim) {
        return claim === 'DOL.WHD.Section14c.' + claimName;
      });
    };

    /*** Methods ***/

    this.setFormData = function(value) {
      merge(state.form_data, value);
    };

    this.setFormValue = function(property, value) {
      merge(state.form_data, { [property]: value });
    };

    this.setAppData = function(value) {
      merge(state.app_data, value);
    };

    this.setAppList = function(value) {
      state.app_list = value;
    };

    this.logOut = function() {
      // remove access_token cookie
      $cookies.remove(accessTokenCookieName);

      setInitialState();
    };

    this.loadSavedApplication = function() {
      const self = this;
      const d = $q.defer();

      // Get Application State for Organization
      apiService.getApplication(self.access_token, self.applicationId).then(
        function(result) {
          const data = result.data;
          self.setFormData(JSON.parse(data));
          d.resolve(data);
        },
        function(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.saveNewApplication = function() {
      const self = this;
      const d = $q.defer();

      // Get Application State for Organization
      apiService.saveApplication(self.access_token, self.ein, self.employerId, self.applicationId, self.formData).then(
        function() {
          // const data = result.data;
          // self.setFormData(JSON.parse(data));
          d.resolve();
        },
        function(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    function setInitialState() {
      state = {
        form_data: {},
        app_data: {},
        app_list: [],
        activeEIN: undefined,
        activeEmployerId: undefined,
        activeApplicationId: undefined,
        user: {
          email: '',
          claims: []
        },
        loggedIn: false
      };
    }

    this.applicationStarted = function() {
      return !isEmpty(omit(state.form_data, ['saved', 'lastSaved']));
    };

    this.resetFormData = function() {
      state.form_data = {};
    };

    this.loadApplicationData = function(appid) {
      const self = this;
      const d = $q.defer();

      apiService.getSubmittedApplication(self.access_token, appid).then(
        function(result) {
          const data = result.data;
          self.setAppData(data);
          d.resolve(data);
        },
        function(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.downloadApplicationPdf = function() {
      const self = this;
      const d = $q.defer();

      apiService.downloadApplicationPdf(self.access_token, self.applicationId).then(
        function(result) {
          const data = result.data;
          d.resolve(data);
        },
        function(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };

    this.loadApplicationList = function() {
      const self = this;
      const d = $q.defer();

      apiService.getSubmittedApplications(self.access_token).then(
        function(result) {
          const data = result.data;
          self.setAppList(data);
          d.resolve(data);
        },
        function(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };
  });
};
