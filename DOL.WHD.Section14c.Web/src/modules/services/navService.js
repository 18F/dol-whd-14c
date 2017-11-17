'use strict';

import findIndex from 'lodash/findIndex';

module.exports = function(ngModule) {
  ngModule.service('navService', function(
    $location,
    $route,
    $window,
    $anchorScroll,
    autoSaveService,
    stateService,
    _constants
  ) {
    'ngInject';
    'use strict';

    const userSectionArray = [
      {
        id: 'assurances',
        display: 'Assurances'
      },
      {
        id: 'app-info',
        display: 'Application Info'
      },
      {
        id: 'employer',
        display: 'Employer'
      },
      {
        id: 'wage-data',
        display: 'Wage Data'
      },
      {
        id: 'work-sites',
        display: 'Work Sites & Employees'
      },
      {
        id: 'wioa',
        display: 'WIOA'
      },
      {
        id: 'review',
        display: 'Review & Submit'
      }
    ];

    const adminSectionArray = [
      {
        id: 'summary',
        display: 'Summary'
      },
      {
        id: 'assurances',
        display: 'Assurances'
      },
      {
        id: 'app-info',
        display: 'Application Info'
      },
      {
        id: 'employer',
        display: 'Employer'
      },
      {
        id: 'wage-data',
        display: 'Wage Data'
      },
      {
        id: 'work-sites',
        display: 'Work Sites & Employees'
      },
      {
        id: 'wioa',
        display: 'WIOA'
      }
    ];

    let state = {
      backStack: []
    };

    Object.defineProperty(this, 'nextLabel', {
      get: function() {
        return state.nextQuery ? state.nextQuery.label : undefined;
      }
    });

    Object.defineProperty(this, 'backLabel', {
      get: function() {
        return state.backQuery ? state.backQuery.label : undefined;
      }
    });

    this.getSections = function() {
      if (stateService.isAdmin) {
        return adminSectionArray;
      } else {
        if (
          stateService.formData.applicationTypeId ===
          _constants.responses.applicationType.initial
        ) {
          let index = findIndex(userSectionArray, { id: 'wage-data' });
          if (index >= 0) {
            return userSectionArray
              .slice(0, index)
              .concat(userSectionArray.slice(index + 1));
          }
        }

        return userSectionArray;
      }
    };

    this.hasNext = function() {
      return state.nextQuery || this.getNextSection();
    };

    this.hasBack = function() {
      return state.backStack.length > 0 || this.getPreviousSection();
    };

    this.scrollPage = function(anchor) {
      if (anchor) {
        $location.hash(anchor);
        $anchorScroll();
      } else {
        $window.scrollTo(0, 0);
      }

      $location.hash('');
    };

    this.goNext = function() {
      this.clearBackQuery();

      let current = $location.$$path.split('/section/')[1];
      let next = this.getNextSection(current);

      if (state.nextQuery) {
        this.pushToBack(current, $location.search());
        $location.search(state.nextQuery.query);
        this.scrollPage(state.nextQuery.scrollAnchor);
      } else if (next) {
        state.backStack.length = 0;
        $location.path('/section/' + next).search({});
        this.scrollPage();
      }

      autoSaveService.save();

      this.clearNextQuery();
    };

    this.goBack = function() {
      let previous = this.getPreviousSection();

      this.clearNextQuery();

      if (state.backQuery) {
        $location.search(state.backQuery.query);
        this.scrollPage(back.query.scrollAnchor);
      } else {
        if (state.backStack.length > 0) {
          let back = state.backStack.pop();

          if (back && back.section) {
            if (back.query) {
              $location.path('/section/' + back.section).search(back.query);
              this.scrollPage(back.query.scrollAnchor);
            } else {
              $location.path('/section/' + back.section).search({});
              this.scrollPage();
            }
          }
        } else if (previous) {
          $location.path('/section/' + previous).search({});
          this.scrollPage();
        }
      }

      autoSaveService.save();

      this.clearBackQuery();
    };

    this.gotoSection = function(section) {
      var sectionArray = this.getSections();

      if (findIndex(sectionArray, { id: section }) === -1) {
        return;
      }

      this.clearBackQuery();
      this.clearNextQuery();
      state.backStack.length = 0;
      if (stateService.isAdmin) {
        if (stateService.appData.id) {
          $location
            .path('/admin/' + stateService.appData.id + '/section/' + section)
            .search({});
          this.scrollPage();
        } else {
          // no application loaded so redirect back to the dashboard
          $location.path('/admin').search({});
          this.scrollPage();
        }
      } else {
        $location.path('/section/' + section).search({});
        this.scrollPage();
      }

      autoSaveService.save();
    };

    this.clearQuery = function() {
      $location.search({});
    };

    this.pushToBack = function(section, query) {
      state.backStack.push({ section: section, query: query });
    };

    this.setNextQuery = function(query, label, scrollAnchor) {
      state.nextQuery = {
        query: query,
        label: label,
        scrollAnchor: scrollAnchor
      };
    };

    this.clearNextQuery = function() {
      state.nextQuery = undefined;
    };

    this.setBackQuery = function(query, label, scrollAnchor) {
      state.backQuery = {
        query: query,
        label: label,
        scrollAnchor: scrollAnchor
      };
    };

    this.clearBackQuery = function() {
      state.backQuery = undefined;
    };

    this.getNextSection = function(current) {
      if (!current) {
        current = $route.current.params.section_id;
      }

      var sectionArray = this.getSections();

      let index = findIndex(sectionArray, { id: current }) + 1;
      if (index < sectionArray.length) {
        return sectionArray[index].id;
      }

      return undefined;
    };

    this.getPreviousSection = function(current) {
      if (!current) {
        current = $location.$$path.split('/section/')[1];
      }

      var sectionArray = this.getSections();

      let index = findIndex(sectionArray, { id: current }) - 1;
      if (index >= 0) {
        return sectionArray[index].id;
      }

      return undefined;
    };
  });
};
