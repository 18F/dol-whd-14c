'use strict';

import findIndex from 'lodash/findIndex';

module.exports = function(ngModule) {
    ngModule.service('navService', function($location, $route, autoSaveService, stateService) {
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
            get: function() { return state.nextQuery ? state.nextQuery.label : undefined }
        });

        Object.defineProperty(this, 'backLabel', {
            get: function() { return state.backQuery ? state.backQuery.label : undefined }
        });

        this.getSections = function() {
            return stateService.isAdmin ? adminSectionArray : userSectionArray;
        }

        this.hasNext = function() {
            return state.nextQuery || this.getNextSection();
        }

        this.hasBack = function() {
            return state.backStack.length > 0 || this.getPreviousSection();
        }

        this.goNext = function() {
            this.clearBackQuery();

            let current = $route.current.params.section_id;
            let next = this.getNextSection(current);

            if (state.nextQuery) {
                this.pushToBack(current, $location.search());
                $location.search(state.nextQuery.query);
            }
            else if (next) {
                state.backStack.length = 0;
                $location.path("/section/" + next).search({});
            }

            autoSaveService.save();

            this.clearNextQuery();
        }

        this.goBack = function() {
            let previous = this.getPreviousSection();

            this.clearNextQuery();

            if (state.backQuery) {
                $location.search(state.backQuery.query);
            }
            else {
                if (state.backStack.length > 0) {
                    let back = state.backStack.pop();

                    if (back && back.section) {
                        if (back.query) {
                            $location.path("/section/" + back.section).search(back.query);
                        }
                        else {
                            $location.path("/section/" + back.section).search({});
                        }
                    }
                }
                else if (previous) {
                    $location.path("/section/" + previous).search({});
                }
            }

            autoSaveService.save();

            this.clearBackQuery();
        }

        this.gotoSection = function(section) {
            var sectionArray = this.getSections();

            if (findIndex(sectionArray, { 'id': section }) === -1) {
                return;
            }

            this.clearBackQuery();
            this.clearNextQuery();
            state.backStack.length = 0;
            if (stateService.isAdmin) {
                if (stateService.appData.id) {
                    $location.path("/admin/" + stateService.appData.id + "/section/" + section).search({});
                }
                else {
                    // no application loaded so redirect back to the dashboard
                    $location.path("/admin").search({});
                }
            }
            else {
                $location.path("/section/" + section).search({});
            }

            autoSaveService.save();
        }

        this.clearQuery = function() {
            $location.search({});
        }

        this.pushToBack = function(section, query) {
            state.backStack.push({ section: section, query: query });
        }

        this.setNextQuery = function(query, label) {
            state.nextQuery = { query: query, label: label };
        }

        this.clearNextQuery = function() {
            state.nextQuery = undefined;
        }

        this.setBackQuery = function(query, label) {
            state.backQuery = { query: query, label: label };
        }

        this.clearBackQuery = function() {
            state.backQuery = undefined;
        }

        this.getNextSection = function(current) {
            if (!current) {
                current = $route.current.params.section_id;
            }

            var sectionArray = this.getSections();

            let index = findIndex(sectionArray, { 'id': current }) + 1;
            if (index < sectionArray.length) {
                return sectionArray[index].id;
            }

            return undefined;
        }

        this.getPreviousSection = function(current) {
            if (!current) {
                current = $route.current.params.section_id;
            }

            var sectionArray = this.getSections();

            let index = findIndex(sectionArray, { 'id': current }) - 1;
            if (index >= 0) {
                return sectionArray[index].id;
            }

            return undefined;
        }
    });
}
