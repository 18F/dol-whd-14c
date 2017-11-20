module.exports = function(ngModule) {
  ngModule.directive('breadcrumb', function() {
    'use strict';

    return {
      restrict: 'EA',
      transclude: true,
      template: require('./breadcrumbTemplate.html'),
      scope: {

      },
      controller: ['$scope', '$location', 'crumble', function($scope, $location, crumble) {
        var section = $location.$$path.split('/section/')[1];
        if(section) {
          var route = section.charAt(0).toUpperCase() + section.slice(1);
          crumble.context = {name: route};
          crumble.update()
        }
        $scope.crumbs = crumble.trail;
      }]
    };
  });
};
