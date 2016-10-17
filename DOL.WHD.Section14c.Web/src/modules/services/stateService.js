'use strict';

import merge from 'lodash/merge'

module.exports = function(ngModule) {
    ngModule.service('stateService', function() {
        'use strict';

        const sectionArray = ['assurances', 'app-info', 'employer', 'wage-data', 'work-sites', 'wioa'];

        let state = {
            form_data: { },
            activeEIN: undefined,
            user: {
                email: ''
            }
        };

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
            get: function() { return state.access_token; },
            set: function(value) { state.access_token = value; }
        });

        // Core form data object model
        Object.defineProperty(this, 'formData', {
            get: function() { return state.form_data; },
            set: function(value) { state.form_data = value; }
        });

        /*** Methods ***/

        this.setFormData = function(value) {
            merge(state.form_data, value);
        }

        this.setFormValue = function(property, value) {
            merge(state.form_data, { [property]: value });
        }

        this.getNextSection = function(current) {
            let index = sectionArray.indexOf(current) + 1;
            if (index < sectionArray.length) {
                return sectionArray[index];
            }

            return undefined;
        }

        this.getPreviousSection = function(current) {
            let index = sectionArray.indexOf(current) - 1;
            if (index >= 0) {
                return sectionArray[index];
            }

            return undefined;
        }
    });
}
