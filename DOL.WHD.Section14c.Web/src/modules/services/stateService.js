'use strict';

import merge from 'lodash/merge'

module.exports = function(ngModule) {
    ngModule.service('stateService', function() {
        'use strict';

        let state = {
            form_data: { }
        };

        /*** Properties ***/

        // email
        Object.defineProperty(this, 'email', {
            get: function() { return state.email; },
            set: function(value) { state.email = value; }
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
