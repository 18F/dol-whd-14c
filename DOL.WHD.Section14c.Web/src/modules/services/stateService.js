'use strict';

module.exports = function(ngModule) {
    ngModule.service('stateService', function() {
        'use strict';

        let state = {
        };

        // username
        Object.defineProperty(this, 'username', {
            get: function() { return state.username; },
            set: function(value) { state.username = value; }
        })
    });
}
