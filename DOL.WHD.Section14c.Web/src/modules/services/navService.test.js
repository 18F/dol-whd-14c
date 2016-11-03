describe('navService', function() {
    var $location;
    var $route;
    var state;

    const sectionArray = ['assurances', 'app-info', 'employer', 'wage-data', 'work-sites', 'wioa'];
    beforeEach(module('14c'));

    var nav;

    beforeEach(inject(function($injector, _$location_, _$route_) {
        $location = _$location_;
        $route = _$route_;
        state = {backStack: []};
        nav = $injector.get('navService');
    }));

    it('should set the nextLabel property', function() {
        expect(nav.nextLabel).toEqual('value');
     });

    it('should set the backLabel property', function() {
        expect(nav.backLabel).toEqual('value');
    });

    it('should set the hasNext property', function() {
        nav.hasNext();
        expect(nav.backLabel).toEqual('value');
    });

    it('should set the hasBack property', function() {
        nav.hasBack();
        expect(nav.backLabel).toEqual('value');
    });

    it('should go the Next Section on goNext call', function() {
        spyOn(nav, 'getNextSection').and.returnValue('wioa');
        $route.current = {
            params: {
                section_id: 'wioa'
            }
            };
        nav.goNext();
        expect(state.nextQuery).toEqual(undefined);
    });

    it('should call the goBack method and clear backQuery', function() {
        spyOn(nav, 'getPreviousSection').and.returnValue('wioa');
        nav.goBack();
        expect(state.backQuery).toEqual(undefined);
    });

    it('should call the gotoSection method', function() {
        nav.gotoSection('wioa');
        expect(nav.backLabel).toEqual('value');
    });

    it('should call the clearQuery method', function() {
        nav.clearQuery();
        expect($location).toEqual('');
    });

    it('should call the pushToBack method', function() {
        nav.pushToBack(section,query);
        expect(nav.backLabel).toEqual('value');
    });

    it('should call the setNextQuery method', function() {
        nav.setNextQuery(query, label);
        expect(nav.backLabel).toEqual('value');
    });

    it('should call the clearNextQuery method', function() {
        nav.clearNextQuery();
        expect(nav.backLabel).toEqual('value');
    });

    it('should call the setBackQuery method', function() {
        nav.setBackQuery(query, label);
        expect(nav.backLabel).toEqual('value');
    });

    it('should call the clearBackQuery method', function() {
        nav.clearBackQuery();
        expect(nav.backLabel).toEqual('value');
    });

    it('should return the next section if not at the end', function() {
        var current = 'assurances';
        expect(nav.getNextSection(current)).toEqual('app-info');
    });

    it('should return undefined if it is at the end', function() {
        var current = 'wioa';
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