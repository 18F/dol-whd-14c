export const saveApplicationFactory = (_env, $q, $http, moment) => {
  return function(access_token, ein, applicationData) {
    let url = _env.api_url + '/api/save/' + ein;
    let d = $q.defer();

    applicationData.saved = moment.utc();

    $http({
      method: 'POST',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      data: applicationData
    }).then(
      function successCallback(data) {
        applicationData.lastSaved = moment.utc();
        d.resolve(data);
      },
      function errorCallback(error) {
        applicationData.lastSaved = 0;
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export const getApplicationFactory = (_env, $q, $http) => {
  return function(access_token, ein) {
    let url = _env.api_url + '/api/save/' + ein;
    let d = $q.defer();

    $http({
      method: 'GET',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token
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

export const getSubmittedApplicationFactory = (_env, $q, $http) => {
  return function(access_token, appid) {
    let url = _env.api_url + '/api/application?id=' + appid;
    let d = $q.defer();

    $http({
      method: 'GET',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token
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

export const getSubmittedApplicationsFactory = (_env, $q, $http) => {
  return function(access_token) {
    let url = _env.api_url + '/api/application/summary';
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

export const submitApplicationFactory = (_env, $q, $http, submissionService) => {
  return function(access_token, ein, vm) {
    const url = _env.api_url + '/api/application/submit';
    const d = $q.defer();
    const submissionVm = submissionService.getSubmissionVM(ein, vm);

    $http({
      method: 'POST',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token
      },
      data: submissionVm
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

export const changeApplicationStatusFactory = (_env, $q, $http) => {
  return function(access_token, appId, newStatusId) {
    const url = `${_env.api_url}/api/application/status?id=${appId}&statusId=${newStatusId}`;
    const d = $q.defer();

    $http({
      method: 'POST',
      url: url,
      headers: {
        Authorization: 'bearer ' + access_token
      }
    }).then(
      function successCallback() {
        d.resolve();
      },
      function errorCallback(error) {
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export default (_env, $q, $http, moment, submissionService) => ({
  saveApplication: saveApplicationFactory(_env, $q, $http, moment),
  getApplication: getApplicationFactory(_env, $q, $http),
  getSubmittedApplication: getSubmittedApplicationFactory(_env, $q, $http),
  getSubmittedApplications: getSubmittedApplicationsFactory(_env, $q, $http),
  submitApplication: submitApplicationFactory(_env, $q, $http, submissionService),
  changeApplicationStatus: changeApplicationStatusFactory(_env, $q, $http, submissionService)
});
