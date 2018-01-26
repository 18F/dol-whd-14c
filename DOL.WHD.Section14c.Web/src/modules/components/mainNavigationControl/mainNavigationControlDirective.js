'use strict';

module.exports = function(ngModule) {
  ngModule.directive('mainNavigationControl', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./mainNavigationControlTemplate.html'),
      controller: 'mainNavigationControlController',
      scope: {
        admin: '='
      },
      link: function(scope, elem) {
        elem.on('keydown', function(e) {
          if(e.keyCode && e.keyCode === 13) {
            scope.vm.onNavClick(e);
          }
        })
      },
      controllerAs: 'vm'
    };
  });
};
