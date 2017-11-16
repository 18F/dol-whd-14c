'use strict';

module.exports = function(ngModule) {
  ngModule.controller('sectionWageDataController', function(
    $scope,
    $location,
    $document,
    stateService,
    navService,
    responsesService,
    validationService,
    _constants
  ) {
    'ngInject';
    'use strict';

    $scope.formData = stateService.formData;
    $scope.validate = validationService.getValidationErrors;
    $scope.showAllHelp = false;

    // the Wage Data section should not be completed for Initial applications,
    // so redirect if necessary.
    if (
      $scope.formData.applicationTypeId ===
      _constants.responses.applicationType.initial
    ) {
      navService.clearNextQuery();
      navService.goNext();
    }

    // multiple choice responses
    let questionKeys = ['PayType'];
    responsesService.getQuestionResponses(questionKeys).then(responses => {
      $scope.responses = responses;
    });

    let query = $location.search();

    var vm = this;
    vm.activeTab = query.t ? query.t : 1;
    vm.showLinks = false;

    vm.onTabClick = function(id) {
      vm.activeTab = id;

      vm.setNextTabQuery(id);
    };

    vm.setNextTabQuery = function(id) {
      if (id === 1) {
        navService.setNextQuery(
          { t: 2 },
          'Next: Add Piece Rate',
          'wagedata_tab_box'
        );
      } else {
        navService.clearNextQuery();
      }
    };

    vm.tabPanelFocus = function(id) {
      if (id === 1) {
          $document[0].getElementById('hourlyTabPanel').focus();
      } else {
          $document[0].getElementById('pieceRateTabPanel').focus();
      }
    };

    $scope.$on('$routeUpdate', function() {
      query = $location.search();
      vm.activeTab = query.t ? query.t : 1;
      vm.setNextTabQuery(vm.activeTab);
    });

    $scope.$watch('formData.payTypeId', function(value) {
      if (value === 23 && vm.activeTab === 1) {
        vm.setNextTabQuery(1);
      } else {
        vm.setNextTabQuery();
      }
    });

    this.toggleAllHelpText = function() {
      $scope.showAllHelp = !$scope.showAllHelp;
    }    

    // Sliding Panel (Helpful Links)
    $('.dol-btn-helpful-links').on('click', function(event){     
      $('.cd-panel').addClass('is-visible');
      $('#helpfulLinksHeader').focus();
      event.preventDefault();
    });

    // close the panel
    function closeSlidingPanel(event) {
      $('.cd-panel').removeClass('is-visible');
      $('.dol-btn-helpful-links').focus();
    }
    $(document).keydown(function(event) {
        // escape key
        if ($('.cd-panel').hasClass('is-visible') && event.keyCode === 27) {
          closeSlidingPanel();
          event.preventDefault();
        }
    });
    $('.cd-panel-close').on('click', function(event){
        closeSlidingPanel()
        event.preventDefault();
    });

    // trap keyboard access inside the panel
    $(".dol-helpful-links-panel .dol-last-focus").keydown(function(event){
      if (event.which === 9 && !event.shiftKey) {
        $(".dol-helpful-links-panel .dol-first-focus").focus();
        event.preventDefault(); 
      }
    });    
    $(".dol-helpful-links-panel .dol-first-focus").keydown(function(event){
      if (event.shiftKey && event.which === 9) {
        $(".dol-helpful-links-panel .dol-last-focus").focus();
        event.preventDefault(); 
      }
    });

  });
};
