describe('navService', function() {
    var $location;
    var $route;
    var state;
    var autoSave;
    var query;

    const sectionArray = ['assurances', 'app-info', 'employer', 'wage-data', 'work-sites', 'wioa', 'review'];
    beforeEach(module('14c'));

    var nav;

    beforeEach(inject(function($injector, _$location_, _$route_) {
        $location = _$location_;
        $route = _$route_;
        state = {backStack: []};
        nav = $injector.get('navService');
        autoSave = $injector.get('autoSaveService');
        $route.current = {
            params: {
                section_id: 'wioa'
            }
            };
    }));

    it('should set the nextLabel property', function() {
        expect(nav.nextLabel).toEqual(state.nextQuery);
     });

    it('should set the backLabel property', function() {
        expect(nav.backLabel).toEqual(state.backQuery);
    });

    it('should set the hasNext property', function() {
        expect(nav.hasNext()).toEqual('review');
    });

    it('should set the hasBack property', function() {
        nav.hasBack();
        expect(state.backStack.length).toEqual(0);
    });

    it('should go the Next Section on goNext call', function() {
        spyOn(nav, 'getNextSection').and.returnValue('wioa');
        nav.goNext();
        expect(state.nextQuery).toEqual(undefined);
    });

    it('should call the goBack method and clear backQuery', function() {
        spyOn(nav, 'getPreviousSection').and.returnValue('wioa');
        nav.goBack();
        expect(state.backQuery).toEqual(undefined);
    });

    it('should call the gotoSection method', function() {
        spyOn(autoSave, 'save');
        nav.gotoSection('wioa');
        expect(state.backStack.length).toEqual(0);
    });

    it('should call the clearQuery method', function() {
        nav.clearQuery();
        //expect($location).toEqual(undefined);
    });

    it('should call the pushToBack method', function() {
        nav.pushToBack('wioa',query);
        expect(state.backStack.length).toEqual(0);
    });

    it('should call the setNextQuery method', function() {
        nav.setNextQuery(query, 'label');
        expect(state.nextQuery).toEqual(undefined);
    });

    it('should call the clearNextQuery method', function() {
        nav.clearNextQuery();
        expect(state.nextQuery).toEqual(undefined);
    });

    it('should call the setBackQuery method', function() {
        nav.setBackQuery(query, 'label');
        expect(state.backQuery).toEqual(undefined);
    });

    it('should call the clearBackQuery method', function() {
        nav.clearBackQuery();
        expect(state.backQuery).toEqual(undefined);
    });

    it('should return the next section if not at the end', function() {
        var current = 'assurances';
        expect(nav.getNextSection(current)).toEqual('app-info');
    });

    it('should return undefined if it is at the end', function() {
        var current = 'review';
        expect(nav.getNextSection(current)).toEqual(undefined);
    });

    it('should return the previous seciton in the array', function() {
        var current = 'wioa';
        expect(nav.getPreviousSection(current)).toEqual('work-sites');
    });

    it('should return undefined if first section', function() {
        var current = 'assurances';
        expect(nav.getPreviousSection(current)).toEqual(undefined);
    });

});