'use strict';

module.exports = function(ngModule) {
    ngModule.service('assetService', function() {
        'use strict';

        this.loadImage = function(image) {
            return require('../../images/' + image);
        }
    });
}
