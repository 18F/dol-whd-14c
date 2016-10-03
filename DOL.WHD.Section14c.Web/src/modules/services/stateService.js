'use strict';

module.exports = function(ngModule) {
    ngModule.service('stateService', function() {
        'use strict';

        let state = {
        };

        // email
        Object.defineProperty(this, 'email', {
            get: function() { return state.email; },
            set: function(value) { state.email = value; }
        });

        Object.defineProperty(this, 'access_token', {
            get: function() { return state.access_token; },
            set: function(value) { state.access_token = value; }
        });
    });
}
