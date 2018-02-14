'use strict';

module.exports = function(ngModule) {
  ngModule.directive('stateField', function() {
    'use strict';

    return {
      restrict: 'EA',
      template: require('./stateFieldTemplate.html'),
      scope: {
        selectedState: '=',
        name: '@'
      },
      link: function(scope) {
        var territoriesAndDistricts = ['DC','AS','GU','MP','PR','UM','VI'];
        scope.$watch('selectedState', function(val) {
          var addressType = scope.name.split('State')[0];
          if(territoriesAndDistricts.indexOf(val) >= 0 && scope.$parent.formData.employer[addressType].county) {
          scope.$parent.formData.employer[addressType].county = "N/A"
          }
        })
      }
    };
  });
};
