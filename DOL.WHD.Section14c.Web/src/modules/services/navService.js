'use strict';

module.exports = function(ngModule) {
    ngModule.service('navService', function($location, $route) {
        'use strict';

        const sectionArray = ['assurances', 'app-info', 'employer', 'wage-data', 'work-sites', 'wioa'];

        let state = {
            backStack: []
        };

        Object.defineProperty(this, 'nextLabel', {
            get: function() { return state.nextQuery ? state.nextQuery.label : undefined }
        });

        this.hasNext = function() {
            return state.nextQuery || this.getNextSection();
        }

        this.hasBack = function() {
            return state.backStack.length > 0 || this.getPreviousSection();
        }

        this.goNext = function() {
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

            this.clearNextQuery();
        }

        this.goBack = function() {
            let previous = this.getPreviousSection();

            this.clearNextQuery();

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

        this.pushToBack = function(section, query) {
            state.backStack.push({ section: section, query: query });
        }

        this.setNextQuery = function(query, label) {
            state.nextQuery = { query: query, label: label };
        }

        this.clearNextQuery = function() {
            state.nextQuery = undefined;
        }

        this.getNextSection = function(current) {
            if (!current) {
                current = $route.current.params.section_id;
            }

            let index = sectionArray.indexOf(current) + 1;
            if (index < sectionArray.length) {
                return sectionArray[index];
            }

            return undefined;
        }

        this.getPreviousSection = function(current) {
            if (!current) {
                current = $route.current.params.section_id;
            }

            let index = sectionArray.indexOf(current) - 1;
            if (index >= 0) {
                return sectionArray[index];
            }

            return undefined;
        }
    });
}
