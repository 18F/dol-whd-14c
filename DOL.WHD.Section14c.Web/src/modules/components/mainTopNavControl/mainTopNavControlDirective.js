const mainTopNav = {
  restrict: 'EA',
  template: require('./mainTopNavControlTemplate.html'),
  controller: 'mainTopNavControlController',
  scope: { admin: '=' },
  controllerAs: 'vm'
};

module.exports = function(ngModule) {
  ngModule.component('mainTopNavControl', mainTopNav);
};
