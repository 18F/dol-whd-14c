'use strict';

import set from 'lodash/set';
import get from 'lodash/get';
import isArray from 'lodash/isArray';
import isEmpty from 'lodash/isEmpty';
import isNumber from 'lodash/isNumber';

module.exports = function(ngModule) {
  ngModule.service('validationService', function(
    stateService,
    _constants,
    moment
  ) {
    'ngInject';
    'use strict';

    // validation error tree (mirrors data model tree)
    let state = {};

    let section;

    this.resetState = function() {
      state = {};
    };

    // validation error accessor methods
    this.setValidationError = function(propPath, msg) {
      set(state, propPath, msg);

      if (section) {
        set(state, section, true);
      }
    };

    this.getValidationError = function(propPath, returnNested) {
      let error = get(state, propPath, undefined);
      if (isArray(error) && !returnNested) {
        return undefined;
      }

      return error;
    };

    this.clearValidationError = function(propPath) {
      set(state, propPath, undefined);
    };

    this.getValidationErrors = function(propPaths, returnNested) {
      // if propPaths is left blank, return the entire state
      if (!propPaths) {
        return state;
      }

      let error;

      if (isArray(propPaths)) {
        // if an array of propPaths are passed in, return the first
        // error. This is a convenience for validating multi-value
        // fields.
        for (let i = 0; i < propPaths.length; i++) {
          error = this.getValidationError(propPaths[i], returnNested);
          if (error) {
            break;
          }
        }
      } else {
        // single propPath, return error val
        error = this.getValidationError(propPaths, returnNested);
      }

      return error;
    }.bind(this);

    // internal utility methods for retreiving, checking, and validating values
    this.getFormValue = function(propPath) {
      let formData = stateService.formData;
      return get(formData, propPath, undefined);
    };

    this.checkRequiredValue = function(propPath, msg) {
      let val = this.getFormValue(propPath);
      if (val === undefined) {
        this.setValidationError(propPath, msg ? msg : 'This field is required');
      }

      return val;
    };

    this.checkRequiredString = function(propPath, msg) {
      let val = this.getFormValue(propPath);
      if (!val || val.length === 0) {
        this.setValidationError(
          propPath,
          msg ? msg : 'Please complete this field'
        );
      }

      return val;
    };

    this.checkRequiredBoolean = function(propPath, msg) {
      let val = this.checkRequiredValue(propPath);
      if (val === false) {
        this.setValidationError(propPath, msg ? msg : 'Please check the box');
      }
    };

    /* eslint-disable complexity */
    this.checkRequiredNumber = function(propPath, msg, min, max) {
      let val = this.checkRequiredValue(propPath);
      if (isNumber(val)) {
        if (min && val < min) {
          this.setValidationError(
            propPath,
            msg ? msg : 'Please enter a value no less than ' + min
          );
          return undefined;
        } else if (max && val > max) {
          this.setValidationError(
            propPath,
            msg ? msg : 'Please enter a value no greater than ' + max
          );
          return undefined;
        }

        return val;
      }

      this.setValidationError(
        propPath,
        msg ? msg : 'Please enter a valid numerical value'
      );
      return undefined;
    };
    /* eslint-enable complexity */

    this.checkRequiredMultipleChoice = function(propPath, msg) {
      return this.checkRequiredValue(
        propPath,
        msg ? msg : 'Please select a value'
      );
    };

    this.checkRequiredValueArray = function(propPath, msg, allowedEmpty) {
      let val = this.checkRequiredValue(propPath);
      if (isArray(val)) {
        if (!allowedEmpty && val.length === 0) {
          this.setValidationError(
            propPath,
            msg ? msg : 'Please provide the required information'
          );
        }

        return val;
      } else if (val === undefined && !allowedEmpty) {
        this.setValidationError(
          propPath,
          msg ? msg : 'Please provide the required information'
        );
      } else {
        this.setValidationError(propPath, 'VALUE ERROR');
      }

      return undefined;
    };

    this.checkRequiredDateComponent = function(propPath) {
      let val = this.checkRequiredValue(propPath);
      if (!this.validateDate(val)) {
        this.setValidationError(propPath, 'Please enter a valid date');
      }
    };

    this.validateDate = function(date) {
      const dateValMoment = moment(date, moment.ISO_8601, true);
      return dateValMoment.isValid();
    };

    this.validateZipCode = function(zip) {
      let re = /^[0-9]{5}(?:-[0-9]{4})?$/i;
      let match = re.exec(zip);

      return match !== null;
    };

    this.validateTelephoneNumber = function(phoneno) {
      let re = /^[0-9]{3}-[0-9]{3}-[0-9]{4}$/i;
      let match = re.exec(phoneno);

      return match !== null;
    };

    this.validateEmailAddress = function(email) {
      let re = /^[-a-z0-9~!$%^&*_=+}{'?]+(\.[-a-z0-9~!$%^&*_=+}{'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i;
      let match = re.exec(email);

      return match !== null;
    };

    this.checkIsInitial = function() {
      return (
        this.getFormValue('applicationTypeId') ===
        _constants.responses.applicationType.initial
      );
    };

    // methods for validating each section (primarily used internally)

    this.validateAssurances = function() {
      section = '__assurances';

      this.checkRequiredBoolean(
        'signature.agreement',
        'An electronic signature must be provided in order to submit this application'
      );
      this.checkRequiredString(
        'signature.fullName',
        'Please provide the full name of the authorized representative'
      );
      this.checkRequiredString(
        'signature.title',
        'Please provide the title of the authorized representative'
      );
      this.checkRequiredDateComponent(
        'signature.date',
        'Please provide the date'
      );
    };

    /* eslint-disable complexity, max-statements */
    this.validateAppInfo = function() {
      section = '__appinfo';

      this.checkRequiredMultipleChoice(
        'applicationTypeId',
        'Please choose which type of application you are submitting'
      );
      this.checkRequiredMultipleChoice(
        'hasPreviousApplication',
        'Please indicate if the employer has previously applied a 14(c) certificate'
      );

      let hasPreviousCert = this.checkRequiredMultipleChoice(
        'hasPreviousCertificate'
      );
      if (hasPreviousCert) {
        let certNo = this.getFormValue('previousCertificateNumber');
        if (!certNo || certNo.length !== 14) {
          //TODO: better test against actual cert number rules
          this.setValidationError(
            'previousCertificateNumber',
            "Please provide the most recently held certificate number for the employer's main establishment"
          );
        }
      }

      this.checkRequiredValueArray(
        'establishmentTypeId',
        'Please select at least one establishment type. You can choose more than one if needed.'
      );

      this.checkRequiredString(
        'contactName',
        'Please provide the full name of the Applicant Contact person'
      );

      if (!this.validateTelephoneNumber(this.getFormValue('contactPhone'))) {
        this.setValidationError(
          'contactPhone',
          'Please provide a valid contact telephone number'
        );
      }

      let fax = this.getFormValue('contactFax');
      if (fax && !this.validateTelephoneNumber(fax)) {
        this.setValidationError(
          'contactFax',
          'If available, please provide a fax number'
        );
      }

      if (!this.validateEmailAddress(this.getFormValue('contactEmail'))) {
        this.setValidationError(
          'contactEmail',
          'Please provide a valid contact email address'
        );
      }

      section = undefined;
    };

    this.validateEmployer = function() {
      section = '__employer';

      this.checkRequiredString(
        'employer.legalName',
        'Please provide the full Legal Name of the employer'
      );

      let hasTradeName = this.checkRequiredMultipleChoice(
        'employer.hasTradeName',
        'Please indicate if the employer has a Trade Name'
      );
      if (hasTradeName === true) {
        this.checkRequiredString(
          'employer.tradeName',
          'You indicated the employer has a Trade Name. Please provide the Trade name here.'
        );
      }

      let legalNameHasChanged = this.checkRequiredMultipleChoice(
        'employer.legalNameHasChanged',
        "Please indicate if the employer's Legal Name has changed"
      );
      if (legalNameHasChanged === true) {
        this.checkRequiredString(
          'employer.priorLegalName',
          "You have indicated that the employer's legal name has changed since its last application. Please provide the prior legal name here."
        );
      }

      this.checkRequiredString(
        'employer.physicalAddress.streetAddress',
        "Please provide the street address for the employer's main establishment"
      );
      this.checkRequiredString(
        'employer.physicalAddress.city',
        "Please provide the city for the employer's main establishment"
      );
      this.checkRequiredValue(
        'employer.physicalAddress.state',
        "Please select a state or territory for the employer's main establishment"
      );

      if (
        !this.validateZipCode(
          this.getFormValue('employer.physicalAddress.zipCode')
        )
      ) {
        this.setValidationError(
          'employer.physicalAddress.zipCode',
          "Please provide a valid zip code for the employer's main establishment"
        );
      }

      this.checkRequiredString(
        'employer.physicalAddress.county',
        "Please provide the county for the employer's main establishment"
      );

      let hasMailingAddress = this.getFormValue('employer.hasMailingAddress');
      if (hasMailingAddress === true) {
        this.checkRequiredString(
          'employer.mailingAddress.streetAddress',
          "Please provide the street address for the employer's mailing address"
        );
        this.checkRequiredString(
          'employer.mailingAddress.city',
          "Please provide the city for the employer's mailing address"
        );
        this.checkRequiredValue(
          'employer.mailingAddress.state',
          "Please select a state or territory for the employer's mailing address"
        );

        if (
          !this.validateZipCode(
            this.getFormValue('employer.mailingAddress.zipCode')
          )
        ) {
          this.setValidationError(
            'employer.mailingAddress.zipCode',
            "Please provide a valid zip code for the employer's mailing address"
          );
        }

        this.checkRequiredString(
          'employer.mailingAddress.county',
          "Please provide the county for the employer's mailing address"
        );
      }

      let hasParentOrg = this.checkRequiredMultipleChoice(
        'employer.hasParentOrg',
        'Please indicate if the employer has a Parent Organization'
      );
      if (hasParentOrg === true) {
        this.checkRequiredString(
          'employer.parentLegalName',
          "Please provide the Parent Organization's legal name"
        );
        this.checkRequiredString(
          'employer.parentAddress.streetAddress',
          "Please provide the street for the employer's Parent Organization"
        );
        this.checkRequiredString(
          'employer.parentAddress.city',
          "Please provide the city for the employer's Parent Organization"
        );
        this.checkRequiredValue(
          'employer.parentAddress.state',
          "Please select a state or territory for the employer's Parent Organization"
        );

        if (
          !this.validateZipCode(
            this.getFormValue('employer.parentAddress.zipCode')
          )
        ) {
          this.setValidationError(
            'employer.parentAddress.zipCode',
            "Please enter a valid zip code for the employer's Parent Organization"
          );
        }

        this.checkRequiredString(
          'employer.parentAddress.county',
          "Please provide the county for the employer's Parent Organization"
        );
      }

      let employerStatus = this.checkRequiredMultipleChoice(
        'employer.employerStatusId',
        "Please provide the employer's status"
      );
      if (employerStatus === _constants.responses.employerStatus.other) {
        this.checkRequiredString(
          'employer.employerStatusOther',
          "Please provide a brief description of the employer's status or choose from the options above."
        );
      }

      this.checkRequiredMultipleChoice(
        'employer.isEducationalAgency',
        'Please indicate if the employer is a Local or State educational agency'
      );

      if (!this.checkIsInitial()) {
        this.checkRequiredDateComponent(
          'employer.fiscalQuarterEndDate',
          'Please provide the date for the most recently compelted fiscal quarter'
        );
        this.checkRequiredNumber(
          'employer.numSubminimalWageWorkers.total',
          'Please provide the total number of workers with disabilities employed at subminimum wages.',
          0
        );
        this.checkRequiredNumber(
          'employer.numSubminimalWageWorkers.workCenter',
          'Please indicate how many workers were employed at Work Center',
          0
        );
        this.checkRequiredNumber(
          'employer.numSubminimalWageWorkers.patientWorkers',
          'Please indicate how many workers were employed as Patient Workers',
          0
        );
        this.checkRequiredNumber(
          'employer.numSubminimalWageWorkers.swep',
          'Please indicate how many workers were employed by a School Work Experience Program',
          0
        );
        this.checkRequiredNumber(
          'employer.numSubminimalWageWorkers.businessEstablishment',
          'Please indicate how many workers were employed at a business establishment',
          0
        );
      }

      this.checkRequiredMultipleChoice(
        'employer.pca',
        'Please indicate if the employer manufactures items under the PCA'
      );

      let sca = this.checkRequiredMultipleChoice(
        'employer.scaId',
        'Please indicate if the employer holds any SCA-covered contracts'
      );
      if (sca === _constants.responses.sca.yes) {
        this.checkRequiredNumber(
          'employer.scaCount',
          'Please provide the total number of workers employed under SCA-covered contracts',
          0
        );

        //TODO: validate number of uploads with scaCount ???
        this.checkRequiredValue(
          'employer.fileUploadSCA',
          'Please upload the required SCA Wage Determinations'
        );
      }

      this.checkRequiredMultipleChoice(
        'employer.eo13658Id',
        'Please indicate if the employer is under contract for services with the Federal Government subject to Executive Order 13658'
      );

      let representativePayee = this.checkRequiredMultipleChoice(
        'employer.representativePayee',
        'Please indicate if the employer was a representative payee'
      );
      if (representativePayee === true) {
        this.checkRequiredNumber(
          'employer.totalDisabledWorkers',
          'You indicated the employer was a representative payee for Social Security Benefits. Please provide the total number of workers the facility was a representative payee for.',
          0
        );
      }

      let takeCreditForCosts = this.checkRequiredMultipleChoice(
        'employer.takeCreditForCosts',
        'Please indicate if the employer took credit for facility costs'
      );
      if (takeCreditForCosts === true) {
        this.checkRequiredValueArray(
          'employer.providingFacilitiesDeductionTypeId',
          'Please select at least one deduction'
        );
      }

      this.checkRequiredMultipleChoice(
        'employer.temporaryAuthority',
        'Please indicate if this is a request for temporary authority'
      );

      section = undefined;
    };

    this.validateWageDataPayType = function(prefix, value) {
      this.checkRequiredNumber(prefix + '.numWorkers', undefined, 0);
      this.checkRequiredString(
        prefix + '.jobName',
        'Please provide the name of the job or contract'
      );

      let prevailingWageMethod = this.checkRequiredMultipleChoice(
        prefix + '.prevailingWageMethodId',
        'Please indicate which method was used'
      );
      if (
        prevailingWageMethod ===
        _constants.responses.prevailingWageMethod.survey
      ) {
        this.checkRequiredNumber(
          prefix + '.mostRecentPrevailingWageSurvey.prevailingWageDetermined',
          'Please provide the Prevailing Wage calculated based on the survey.',
          0
        );

        let sourceEmployers = this.checkRequiredValueArray(
          prefix + '.mostRecentPrevailingWageSurvey.sourceEmployers',
          'Please provide 3 source employers'
        );
        if (sourceEmployers) {
          for (let i = 0; i < sourceEmployers.length; i++) {
            let subprefix =
              prefix +
              '.mostRecentPrevailingWageSurvey.sourceEmployers[' +
              i +
              ']';

            this.checkRequiredString(
              subprefix + '.employerName',
              'Please provide the name of the Source Employer'
            );
            this.checkRequiredString(
              subprefix + '.address.streetAddress',
              'Please provide the street address of the Source Employer'
            );
            this.checkRequiredString(
              subprefix + '.address.city',
              'Please provide the city for the Source Employer'
            );
            this.checkRequiredValue(
              subprefix + '.address.state',
              'Please select a state or territory for the Source Employer'
            );

            if (
              !this.validateZipCode(
                this.getFormValue(subprefix + '.address.zipCode')
              )
            ) {
              this.setValidationError(
                subprefix + '.address.zipCode',
                'Please enter a valid zip code for the Source Employer'
              );
            }

            if (
              !this.validateTelephoneNumber(
                this.getFormValue(subprefix + '.phone')
              )
            ) {
              this.setValidationError(
                subprefix + '.phone',
                'Please enter a valid telephone number for the Source Employer'
              );
            }

            this.checkRequiredString(
              subprefix + '.contactName',
              'Please provide the name of the Source Employer contact'
            );
            this.checkRequiredString(
              subprefix + '.contactTitle',
              'Please provide the title for the Source Employer contact'
            );
            this.checkRequiredDateComponent(
              subprefix + '.contactDate',
              'Please provide the date the Source Employer was contacted'
            );
            this.checkRequiredString(
              subprefix + '.jobDescription',
              'Please provide a brief description of the job'
            );
            this.checkRequiredNumber(
              subprefix + '.experiencedWorkerWageProvided',
              'Please provide the experienced worker wage provided',
              0
            );
            this.checkRequiredString(
              subprefix + '.conclusionWageRateNotBasedOnEntry',
              'Please provide a bassi for conclusion that the wage rate is not based on entry'
            );
          }

          if (sourceEmployers.length < 3) {
            this.setValidationError(
              prefix + '.sourceEmployers_count',
              'Only ' +
                sourceEmployers.length +
                ' of 3 Source Employers provided.'
            );
          }
        }
      } else if (
        prevailingWageMethod ===
        _constants.responses.prevailingWageMethod.alternate
      ) {
        this.checkRequiredString(
          prefix + '.alternateWageData.alternateWorkDescription',
          'Please provide a description of the work'
        );
        this.checkRequiredString(
          prefix + '.alternateWageData.alternateDataSourceUsed',
          'Please provide the source used for alternate data'
        );
        this.checkRequiredNumber(
          prefix + '.alternateWageData.prevailingWageProvidedBySource',
          'Please provide the prevailing wage provided by source'
        );
        this.checkRequiredDateComponent(
          prefix + '.alternateWageData.dataRetrieved',
          'Please provide the date the alternate wage data was retrieved'
        );
      } else if (
        prevailingWageMethod === _constants.responses.prevailingWageMethod.sca
      ) {
        this.checkRequiredValueArray(
          prefix + '.' + value + 'ScaWageDeterminationAttachments',
          'Please provide the SCA Wage Determination survey',
          false
        );
      }
    };

    this.validateWageData = function() {
      section = '__wagedata';

      let payType = this.checkRequiredMultipleChoice('payTypeId');
      let isHourly =
        payType === _constants.responses.payType.hourly ||
        payType === _constants.responses.payType.both;
      let isPieceRate =
        payType === _constants.responses.payType.pieceRate ||
        payType === _constants.responses.payType.both;

      if (isHourly) {
        let prefix = 'hourlyWageInfo';
        let value = 'h';
        this.validateWageDataPayType(prefix, value);

        this.checkRequiredString(
          prefix + '.jobDescription',
          'Please provide a brief description of the work performed. For example: Kitchen cleaning—sink, counters, stove, refrigerator, microwave cleaning duties.'
        );
        this.checkRequiredValue(
          prefix + '.workMeasurementFrequency',
          'Please indicate how frequently the employer conducts work measurements or time studies'
        );
        this.checkRequiredValue(
          prefix + '.attachmentId',
          'Please upload at one piece of documentation'
        );
      }

      if (isPieceRate) {
        let prefix = 'pieceRateWageInfo';
        let value = 'pr';
        this.validateWageDataPayType(prefix, value);

        this.checkRequiredString(
          prefix + '.jobDescription',
          'Please provide a brief description of the work performed. For example: Gadget dissasembly, or Contract No. 000-111 with Widgets Inc.--Hand Assebmly of Boxes (28" x 12").'
        );
        this.checkRequiredString(
          prefix + '.pieceRateWorkDescription',
          'Please provide a description of the job tasks'
        );
        this.checkRequiredNumber(
          prefix + '.prevailingWageDeterminedForJob',
          'Please provide the hourly prevailing wage for the job',
          0
        );
        this.checkRequiredNumber(
          prefix + '.standardProductivity',
          'Please provide the standard productivity',
          0
        );
        this.checkRequiredNumber(
          prefix + '.pieceRatePaidToWorkers',
          'Please provide the piece rate paid to workers',
          0
        );
        this.checkRequiredValue(
          prefix + '.attachmentId',
          'Please upload one piece of documentation'
        );
      }

      section = undefined;
    };

    this.validateWorkSites = function() {
      section = '__worksites';

      let totalNumWorkSites = this.checkRequiredNumber(
        'totalNumWorkSites',
        'You must provide the total number of establishments and/or work sites to be covered by the certificate',
        1
      );

      let worksites = this.checkRequiredValueArray(
        'workSites',
        'Please provide information for each work site'
      );
      if (worksites) {
        let mainWorksite = -1;

        for (let i = 0; i < worksites.length; i++) {
          let prefix = 'workSites[' + i + ']';

          let worksiteType = this.checkRequiredMultipleChoice(
            prefix + '.workSiteTypeId'
          );
          if (worksiteType === _constants.responses.workSiteType.main) {
            if (mainWorksite !== -1) {
              this.setValidationError(
                prefix + '.workSiteTypeId',
                'There can only be one Main Establishment. You indicated a work site already added was the Main Establishment.'
              );
              this.setValidationError(
                'workSites[' + mainWorksite + '].workSiteTypeId',
                'There can only be one Main Establishment. You indicated a work site already added was the Main Establishment.'
              );
            } else {
              mainWorksite = i;
            }
          }

          this.checkRequiredString(
            prefix + '.name',
            'Please provide the name of this establishment/work site'
          );

          this.checkRequiredString(
            prefix + '.address.streetAddress',
            'Please provide the street address for the establishment/work site'
          );
          this.checkRequiredString(
            prefix + '.address.city',
            'Please provide the city for the establishment/work site'
          );
          this.checkRequiredValue(
            prefix + '.address.state',
            'Please select a state or territory for the establishment/work site'
          );

          if (
            !this.validateZipCode(
              this.getFormValue(prefix + '.address.zipCode')
            )
          ) {
            this.setValidationError(
              prefix + '.address.zipCode',
              'Please enter a valid zip code for the establishment/work site'
            );
          }

          this.checkRequiredMultipleChoice(
            prefix + '.sca',
            'Please indicated if SCA-covered work is performed at this establishment/work site'
          );
          this.checkRequiredMultipleChoice(
            prefix + '.federalContractWorkPerformed',
            'Please indicate if work performed at this establishment/work site is pursuant to a Federal contract'
          );

          if (!this.checkIsInitial()) {
            let numEmployees = this.checkRequiredNumber(
              prefix + '.numEmployees',
              'Please provide the number of workers employed at this establishment/work site',
              0
            );

            let employees = this.checkRequiredValueArray(
              prefix + '.employees',
              'Please provide the required information for employees'
            );
            if (employees) {
              for (let j = 0; j < employees.length; j++) {
                let subprefix = prefix + '.employees[' + j + ']';
                this.checkRequiredString(subprefix + '.name');

                let primaryDisability = this.checkRequiredMultipleChoice(
                  subprefix + '.primaryDisabilityId'
                );
                if (
                  primaryDisability ===
                  _constants.responses.primaryDisability.other
                ) {
                  this.checkRequiredString(
                    subprefix +
                      '.' +
                      _constants.responses.primaryDisability.otherValueKey
                  );
                }

                this.checkRequiredString(
                  subprefix + '.workType',
                  'Please describe the type of work performed by this worker'
                );

                this.checkRequiredNumber(
                  subprefix + '.numJobs',
                  'Please indicated the number of jobs the employee performed',
                  0
                );
                this.checkRequiredNumber(
                  subprefix + '.avgWeeklyHours',
                  'Please provide the average number of hours per week the employee worked on all jobs',
                  0
                );
                this.checkRequiredNumber(
                  subprefix + '.avgHourlyEarnings',
                  'Please provide the average earings per hour for this employee',
                  0
                );
                this.checkRequiredNumber(
                  subprefix + '.prevailingWage',
                  'Please provide the prevailing wage rate for the job identified',
                  0
                );

                if (this.getFormValue(subprefix + '.hasProductivityMeasure')) {
                  this.checkRequiredNumber(
                    subprefix + '.productivityMeasure',
                    'Please provide the productivity measure for the job identified',
                    0
                  );
                }

                this.checkRequiredNumber(
                  subprefix + '.commensurateWageRate',
                  'Please provide the commensurage wage rate for the job identified',
                  0
                );
                this.checkRequiredNumber(
                  subprefix + '.totalHours',
                  "Please provide the employee's total hours worked on the job identified",
                  0
                );
                this.checkRequiredMultipleChoice(
                  subprefix + '.workAtOtherSite',
                  'Please indicate if the employee also performed work at another site included with this application'
                );
              }
            }

            let totalEmployees = employees ? employees.length : 0;
            if (numEmployees !== totalEmployees) {
              this.setValidationError(
                prefix + '.employee_count',
                'The number of employees reported (' +
                  (numEmployees === undefined ? '0' : numEmployees) +
                  ') does not match the number of employees entered (' +
                  totalEmployees +
                  ') for this worksite'
              );
            }
          }
        }

        if (totalNumWorkSites !== worksites.length) {
          this.setValidationError(
            'workSites_count',
            'The number of work sites reported (' +
              (totalNumWorkSites === undefined ? '0' : totalNumWorkSites) +
              ') does not match the number entered (' +
              worksites.length +
              ')'
          );
        }
      }

      section = undefined;
    };

    /* eslint-enable complexity */

    this.validateWIOA = function() {
      section = '__wioa';

      this.checkRequiredMultipleChoice(
        'WIOA.hasVerifiedDocumentation',
        'Please indicate if the employer reviewed & verified documentation as required by WIOA'
      );

      let hasWIOAWorkers = this.checkRequiredMultipleChoice(
        'WIOA.hasWIOAWorkers'
      );
      if (hasWIOAWorkers === true) {
        let workers = this.checkRequiredValueArray(
          'WIOA.WIOAWorkers',
          'Please list all applicable workers'
        );
        if (workers) {
          for (let i = 0; i < workers.length; i++) {
            this.checkRequiredString(
              'WIOA.WIOAWorkers[' + i + '].fullName',
              'Please provide the full name of the worker'
            );
            this.checkRequiredMultipleChoice(
              'WIOA.WIOAWorkers[' + i + '].WIOAWorkerVerified',
              'Please choose a response for this worker'
            );
          }
        }
      }

      section = undefined;
    };

    // main method to be called for application validation
    this.validateForm = function() {
      this.resetState();
      this.validateAssurances();
      this.validateAppInfo();
      this.validateEmployer();

      if (!this.checkIsInitial()) {
        this.validateWageData();
      }
      this.validateWorkSites();
      this.validateWIOA();

      return isEmpty(state);
    };
  });
};
