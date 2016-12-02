'use strict';

module.exports = function(ngModule) {
  ngModule.directive('sectionAdminWorkSites', function() {

      'use strict';

      return {
          restrict: 'EA',
          //template: require('./sectionAdminWorkSitesTemplate.html'),
          template: function(elem, attr) {
              if(attr.itemId) {
                  return require('./adminWorkSiteDetailTemplate.html')
              }
              return require('./sectionAdminWorkSitesTemplate.html');
          },
          controller: 'sectionAdminWorkSitesController',
          controllerAs: 'vm',
          scope: {
              itemId: '='
          }
      };
  });
}
