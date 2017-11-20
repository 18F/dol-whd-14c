'use strict';
module.exports = {
  access: {
    ROUTE_PUBLIC: 1,
    ROUTE_LOGGEDIN: 3,
    ROUTE_USER: 7,
    ROUTE_ADMIN: 11
  },
  checkRouteAccess: function(route, userAccess) {
    if (!route || !route.access) {
      return true;
    }
    return (route.access & userAccess) === route.access;
  }
}
