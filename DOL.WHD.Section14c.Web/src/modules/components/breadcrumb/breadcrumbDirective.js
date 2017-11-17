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
        var route = section.charAt(0).toUpperCase() + section.slice(1);
        crumble.context = {name: route};
        console.log(route)
        crumble.update()
        $scope.crumbs = crumble.trail;
        console.log($scope.crumbs)
      }],
      link: function(scope) {

      }
    };
  });
};
