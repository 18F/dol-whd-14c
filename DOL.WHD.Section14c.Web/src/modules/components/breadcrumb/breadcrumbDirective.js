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
        console.log($location.$$path)
        var section = $location.$$path.split('/section/')[1];
        console.log(section)
        if(section) {
          var route = section.charAt(0).toUpperCase() + section.slice(1);
          console.log(route)
          crumble.context = {name: route};
        } else {
          crumble.context = {name: $location.$$path}
        }
        crumble.update()
        $scope.crumbs = crumble.trail;
        console.log($scope.crumbs);
      }]
    };
  });
};
