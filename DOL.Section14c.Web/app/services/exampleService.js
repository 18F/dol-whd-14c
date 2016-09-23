
module.exports = function ($log) {
    'use strict';

    function doSomething() {
        $log.info('something done!');
    }

    return {
        DoSomething: doSomething
    };
};