'use strict';

import * as _ from 'lodash';

module.exports = function(ngModule) {
  ngModule.service('responsesService', function($http, _env, $q) {
    'ngInject';
    'useStrict';

    var responses;
    var url = `${_env.api_url}/api/Response`;

    var getData = function() {
      let d = $q.defer();

      if (responses) {
        // load cached data
        d.resolve(responses);
      } else {
        // get data from server
        $http({
          method: 'GET',
          url: url
        }).then(
          function successCallback(data) {
            // cache data
            responses = data.data;
            d.resolve(data.data);
          },
          function errorCallback(error) {
            d.reject(error);
          }
        );
      }

      return d.promise;
    };

    this.getQuestionResponses = function(questionKeys) {
      var d = $q.defer();

      getData().then(
        function successCallback(data) {
          var obj = {};
          if (!_.isArray(questionKeys)) {
            questionKeys = [questionKeys];
          }
          _.forEach(questionKeys, function(questionKey) {
            obj[questionKey] = _.sortBy(
              _.filter(data, { questionKey: questionKey, isActive: true }),
              'id'
            );
          });

          d.resolve(obj);
        },
        function errorCallback(error) {
          d.reject(error);
        }
      );

      return d.promise;
    };
  });
};
