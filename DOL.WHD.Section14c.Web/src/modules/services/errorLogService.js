'use strict';

module.exports = function(ngModule) {
  ngModule.service('errorLogService', function(
    $log,
    loggingService
  ) {
    function log( exception, cause ) {
        // Pass off the error to the default error handler
        // on the AngualrJS logger. This will output the
        // error to the console (and let the application
        // keep running normally for the user).
        $log.error.apply( $log, arguments );
        // prevents the same client from
        // logging the same error over and over again
        try {
            var errorMessage = new customError(exception.toString(), "Error")
            // Log the JavaScript error to the server.

            loggingService.addLog(errorMessage)
        } catch ( loggingError ) {
            // For Developers - log the log-failure.
            $log.warn( "Error logging failed" );
            $log.log( loggingError );
        }
    }
    // Return the logging function.
    return( log );
  });
};
