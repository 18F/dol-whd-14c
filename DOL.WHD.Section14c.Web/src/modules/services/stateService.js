'use strict';

import merge from 'lodash/merge'
import some from 'lodash/some'
import has from 'lodash/has'
import property from 'lodash/property'


module.exports = function(ngModule) {
    ngModule.service('stateService', function($cookies, moment, apiService, $q) {
        'use strict';

        const sectionArray = ['assurances', 'app-info', 'employer', 'wage-data', 'work-sites', 'wioa'];
        const accessTokenCookieName = 'api_access_token';

        let state;
        setInitialState();

        /*** Properties ***/

        // user
        Object.defineProperty(this, 'user', {
            get: function() { return state.user; },
            set: function(value) { state.user = value; }
        });

        Object.defineProperty(this, 'ein', {
            get: function() { return state.activeEIN; },
            set: function(value) { state.activeEIN = value; }
        });

        // REST access token
        Object.defineProperty(this, 'access_token', {
            get: function() {
                return $cookies.get(accessTokenCookieName);
            },
            set: function(value) {
                $cookies.put(accessTokenCookieName, value, {
                    secure: true,
                    expires: moment().add(1, 'y').toDate()
                });
            }
        });

        // Core form data object model
        Object.defineProperty(this, 'formData', {
            get: function() { return state.form_data; },
            set: function(value) { state.form_data = value; }
        });

        Object.defineProperty(this, 'appData', {
            get: function() { return state.app_data; },
            set: function(value) { state.app_data = value; }
        });

        this.hasClaim = function(claimName) {
            return some(state.user.applicationClaims, function(claim){
                return claim === 'DOL.WHD.Section14c.' + claimName;
            });
        }

        /*** Methods ***/

        this.setFormData = function(value) {
            merge(state.form_data, value);
        }

        this.setFormValue = function(property, value) {
            merge(state.form_data, { [property]: value });
        }

        this.logOut = function() {
            // remove access_token cookie
            $cookies.remove(accessTokenCookieName);

            setInitialState();
        }

        this.loadState = function() {
            const self = this;
            const d = $q.defer();

            // Get User Info
            apiService.userInfo(self.access_token).then(function (result) {
                const data = result.data;
                self.user = data;
                if(data.organizations.length > 0){
                    self.ein = data.organizations[0].ein; //TODO: Add EIN selection?
                    // Get Application State for Organization
                    apiService.getApplication(self.access_token, self.ein).then(function (result) {
                        const data = result.data;
                        self.setFormData(JSON.parse(data));
                        d.resolve(data);
                    }, function (error) {
                        d.reject(error);
                    });
                }
            }, function (error) {
                d.reject(error);
            });

            return d.promise;
        }

        function setInitialState() {
            state = {
                form_data: { },
                app_data: { },
                activeEIN: undefined,
                user: {
                    email: '',
                    claims: []
                }
            };
        }
    });
}
