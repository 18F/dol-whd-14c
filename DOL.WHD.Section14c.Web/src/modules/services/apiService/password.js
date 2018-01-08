export const changePasswordFactory = (_env, $q, $http) => {
  return function(
    access_token,
    email,
    oldPassword,
    newPassword,
    confirmPassword
  ) {
    let url = _env.api_url + '/api/Account/ChangePassword';
    let d = $q.defer();
    let headerVal;

    if (access_token !== undefined) {
      headerVal = {
        Authorization: 'bearer ' + access_token,
        'Content-Type': 'application/x-www-form-urlencoded'
      };
    } else {
      headerVal = { 'Content-Type': 'application/x-www-form-urlencoded' };
    }

    $http({
      method: 'POST',
      url: url,
      headers: headerVal,
      data: $.param({
        Email: email,
        OldPassword: oldPassword,
        NewPassword: newPassword,
        ConfirmPassword: confirmPassword
      })
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        //console.log(error);
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export const resetPasswordFactory = (_env, $q, $http) => {
  return function(email, passwordResetUrl) {
    let url = _env.api_url + '/api/Account/ResetPassword';
    let d = $q.defer();

    $http({
      method: 'POST',
      url: url,
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
      data: $.param({ Email: email, PasswordResetUrl: passwordResetUrl })
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

export const verifyResetPasswordFactory = (_env, $q, $http) => {
  return function(
    userId,
    newPassword,
    confirmPassword,
    code
  ) {
    let url = _env.api_url + '/api/Account/VerifyResetPassword';
    let d = $q.defer();

    $http({
      method: 'POST',
      url: url,
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
      data: $.param({
        NewPassword: newPassword,
        ConfirmPassword: confirmPassword,
        UserId: userId,
        Nounce: code
      })
    }).then(
      function successCallback(data) {
        d.resolve(data);
      },
      function errorCallback(error) {
        //console.log(error);
        d.reject(error);
      }
    );

    return d.promise;
  };
};

export default (_env, $q, $http) => ({
  changePassword: changePasswordFactory(_env, $q, $http),
  resetPassword: resetPasswordFactory(_env, $q, $http),
  verifyResetPassword: verifyResetPasswordFactory(_env, $q, $http)
});
