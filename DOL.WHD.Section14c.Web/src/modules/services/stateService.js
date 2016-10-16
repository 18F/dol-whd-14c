'use strict';

import merge from 'lodash/merge'

module.exports = function(ngModule) {
    ngModule.service('stateService', function() {
        'use strict';

        let state = {
            currentSection: 0,
            form_data: { },
            user: {
                email: '',
                ein: ''
            }
        };

        /*** Properties ***/

        // currentSection
        Object.defineProperty(this, 'currentSection', {
            get: function() { return state.currentSection; },
            set: function(value) { state.currentSection = value; }
        });

        // user
        Object.defineProperty(this, 'user', {
            get: function() { return state.user; },
            set: function(value) { state.user = value; }
        });

        // REST access token
        Object.defineProperty(this, 'access_token', {
            get: function() { return state.access_token; },
            set: function(value) { state.access_token = value; }
        });

        // Core form data object model
        Object.defineProperty(this, 'form_data', {
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
    });
}
