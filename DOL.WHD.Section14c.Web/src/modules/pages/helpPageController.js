'use strict';

module.exports = function(ngModule) {
  ngModule.controller('helpPageController', function(
    $scope,
    _env
  ) {
    'ngInject';
    'use strict';
    
    $scope.helpEmailAddress = _env.helpEmailAddress;
    $scope.helpPhoneNumber = _env.helpPhoneNumber;
  });
};
