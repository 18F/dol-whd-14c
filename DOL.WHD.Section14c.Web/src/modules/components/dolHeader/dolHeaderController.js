'use strict';

module.exports = function(ngModule) {
  ngModule.controller('dolHeaderController', function(
    $scope,
    $location,
    $route,
    $anchorScroll,
    navService,
    autoSaveService,
    validationService
  ) {
    'ngInject';
    'use strict';

    $scope.skipToMainContent = function (id) {
      //$location.hash(id);
      $anchorScroll(id);
      document.getElementById(id).focus();
    }
  });
};
