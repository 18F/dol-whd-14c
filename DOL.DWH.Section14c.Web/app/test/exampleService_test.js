window.assert = chai.assert;

describe('ExampleService', function () {
    // define variables for the services we want to access in tests
    var ExampleService,
        $log;

    beforeEach(function () {
        // load the module we want to test
        angular.mock.module('app');

        // inject the services we want to test
        inject(function (_ExampleService_, _$log_) {
            ExampleService = _ExampleService_;
            $log = _$log_;
        });
    });

    describe('#DoSomething', function () {
        it('should log the message "something done!"', function () {
            // Arrange
            sinon.spy($log, 'info');

            // Act
            ExampleService.DoSomething();

            // Assert
            assert($log.info.calledOnce);
            assert($log.info.calledWith('something done!'));

            // Cleanup
            $log.info.restore();
        });
    });
});