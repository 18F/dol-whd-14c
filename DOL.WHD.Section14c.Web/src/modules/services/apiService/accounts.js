export const getAccountsFactory = (_env, $q, $http) => {
  return function(access_token) {
    let url = _env.api_url + '/api/account';
    let d = $q.defer();

    $http({
      method: 'GET',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      }
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export const getRolesFactory = (_env, $q, $http) => {
  return function(access_token) {
    let url = _env.api_url + '/api/account/roles';
    let d = $q.defer();

    $http({
      method: 'GET',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      }
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export const getAccountFactory = (_env, $q, $http) => {
  return function(access_token, userId) {
    let url = _env.api_url + '/api/account/' + userId;
    let d = $q.defer();

    $http({
      method: 'GET',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      }
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export const modifyAccountFactory = (_env, $q, $http) => {
  return function(access_token, account) {
    let url = _env.api_url + '/api/account/' + account.userId;
    let d = $q.defer();

    $http({
      method: 'POST',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      data: $.param({ Email: account.email, Roles: account.roles })
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export const createAccountFactory = (_env, $q, $http) => {
  return function(access_token, account) {
    let url = _env.api_url + '/api/account';
    let d = $q.defer();

    $http({
      method: 'POST',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      data: $.param({ Email: account.email, Roles: account.roles })
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export default (_env, $q, $http) => ({
  getAccounts: getAccountsFactory(_env, $q, $http),
  getRoles: getRolesFactory(_env, $q, $http),
  getAccount: getAccountFactory(_env, $q, $http),
  modifyAccount: modifyAccountFactory(_env, $q, $http),
  createAccount: createAccountFactory(_env, $q, $http)
});
