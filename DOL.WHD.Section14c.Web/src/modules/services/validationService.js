'use strict';

import set from 'lodash/set';
import get from 'lodash/get';
import isArray from 'lodash/isArray';
import isEmpty from 'lodash/isEmpty';
import isNumber from 'lodash/isNumber';
import includes from 'lodash/includes';
import forEach from 'lodash/forEach';

module.exports = function(ngModule) {
    ngModule.service('validationService', function(stateService, _constants) {
        'ngInject';
        'use strict';

        // months containing 31 days
        const months31 = [1, 3, 5, 7, 8, 10, 12];


        // validation error tree (mirrors data model tree)
        let state = {};

        this.resetState = function() {
            state = {};
        }


        // validation error accessor methods
        this.setValidationError = function(propPath, msg) {
            set(state, propPath, msg);
        }

        this.getValidationError = function(propPath, returnNested) {
            let error = get(state, propPath, undefined);
            if (isArray(error) && !returnNested) {
                return undefined;
            }

            return error;
        }

        this.clearValidationError = function(propPath) {
            set(state, propPath, undefined);
        }

        this.getValidationErrors = (function(propPaths, returnNested) {
            // if propPaths is left blank, return the entire state
            if (!propPaths) {
                return state;
            }

            let error;

            if (isArray(propPaths)) {
                // if an array of propPaths are passed in, return the first
                // error. This is a convenience for validating multi-value
                // fields.
                for (let i=0; i < propPaths.length; i++) {
                    error = this.getValidationError(propPaths[i], returnNested);
                    if (error) {
                        break;
                    }
                }
            }
            else {
                // single propPath, return error val
                error = this.getValidationError(propPaths, returnNested);
            }

            return error;
        }).bind(this);


        // internal utility methods for retreiving, checking, and validating values
        this.getFormValue = function(propPath) {
            let formData = stateService.formData;
            return get(formData, propPath, undefined);
        }

        this.checkRequiredValue = function(propPath, msg) {
            let val = this.getFormValue(propPath);
            if (val === undefined) {
                this.setValidationError(propPath, msg ? msg : "This field is required");
            }

            return val;
        }

        this.checkRequiredString = function(propPath, msg) {
            let val = this.getFormValue(propPath);
            if (!val || val.length === 0) {
                this.setValidationError(propPath, msg ? msg : "Please complete this field");
            }

            return val;
        }

        this.checkRequiredNumber = function(propPath, msg, min, max) {
            let val = this.checkRequiredValue(propPath);
            if (isNumber(val)) {
                if (min && val < min) {
                    this.setValidationError(propPath, msg ? msg : "Please enter a value no less than " + min);
                    return undefined;
                }
                else if (max && val > max) {
                    this.setValidationError(propPath, msg ? msg : "Please enter a value no greater than " + max);
                    return undefined;
                }

                return val;
            }

            this.setValidationError(propPath, msg ? msg : "Please enter a valid numerical value");
            return undefined;
        }

        this.checkRequiredMultipleChoice = function(propPath, msg) {
            return this.checkRequiredValue(propPath, msg ? msg : "Please select a value");
        }

        this.checkRequiredValueArray = function(propPath, msg, allowedEmpty) {
            let val = this.checkRequiredValue(propPath);
            if (isArray(val)) {
                if (!allowedEmpty && val.length === 0) {
                    this.setValidationError(propPath, msg ? msg : "Please provide the required information");
                }

                return val;
            }
            else if (val === undefined && !allowedEmpty) {
                this.setValidationError(propPath, msg ? msg : "Please provide the required information");
            }
            else {
                this.setValidationError(propPath, "VALUE ERROR");
            }

            return undefined;
        }

        this.checkRequiredDate = function(monthPath, dayPath, yearPath) {
            let month = this.checkRequiredNumber(monthPath, "Please enter a valid month", 1, 12);
            this.checkRequiredNumber(dayPath, "Please enter a valid day", 1, month && month === 2 ? 28 : month && !includes(months31, month) ? 30 : 31);
            this.checkRequiredNumber(yearPath, "Please enter a valid year", 1900);
        }

        this.checkRequiredDateComponent = function(propPath) {
            let val = this.checkRequiredValue(propPath);
            if(!this.validateDate(val)) {
                this.setValidationError(propPath, "Please enter a valid date");
            }
        }

        this.validateDate = function(date) {
            return Object.prototype.toString.call(date) === "[object Date]" && ! isNaN(date.getTime());
        }

        this.validateZipCode = function(zip) {
            let re = /^[0-9]{5}(?:-[0-9]{4})?$/i;
            let match = re.exec(zip);

            return match !== null;
        }

        this.validateTelephoneNumber = function(phoneno) {
            let re = /^[0-9]{3}-[0-9]{3}-[0-9]{4}$/i;
            let match = re.exec(phoneno);

            return match !== null;
        }

        this.validateEmailAddress = function(email) {
            let re = /^[-a-z0-9~!$%^&*_=+}{\'?]+(\.[-a-z0-9~!$%^&*_=+}{\'?]+)*@([a-z0-9_][-a-z0-9_]*(\.[-a-z0-9_]+)*\.(aero|arpa|biz|com|coop|edu|gov|info|int|mil|museum|name|net|org|pro|travel|mobi|[a-z][a-z])|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,5})?$/i;
            let match = re.exec(email);

            return match !== null;
        }


        // methods for validating each section (primarily used internally)
        this.validateAppInfo = function() {
            this.checkRequiredMultipleChoice("applicationType");
            this.checkRequiredMultipleChoice("hasPreviousApplication");

            let hasPreviousCert = this.checkRequiredMultipleChoice("hasPreviousCertificate");
            if (hasPreviousCert) {
                let certNo = this.getFormValue("certificateNumber");
                if (!certNo || certNo.length !== 14) {
                    //TODO: better test against actual cert number rules
                    this.setValidationError("certificateNumber", "Please enter a valid certificate number");
                }
            }

            this.checkRequiredValueArray("establishmentType", "Please select all that apply");

            this.checkRequiredString("contactName");

            if (!this.validateTelephoneNumber(this.getFormValue("contactPhone"))) {
                this.setValidationError("contactPhone", "Please enter a valid telephone number");
            }

            let fax = this.getFormValue("contactFax");
            if (fax && !this.validateTelephoneNumber(fax)) {
                this.setValidationError("contactFax", "Please enter a valid fax number or leave blank");
            }

            if (!this.validateEmailAddress(this.getFormValue("contactEmail"))) {
                this.setValidationError("contactEmail", "Please enter a valid email address");
            }
        }

        this.validateEmployer = function() {
            let hasTradeName = this.checkRequiredMultipleChoice("employer.hasTradeName");
            if (hasTradeName === true) {
                this.checkRequiredString("employer.tradeName");
            }

            let legalNameHasChanged = this.checkRequiredMultipleChoice("employer.legalNameHasChanged");
            if (legalNameHasChanged === true) {
                this.checkRequiredString("employer.priorLegalName");
            }

            this.checkRequiredString("employer.physicalAddress.streetAddress");
            this.checkRequiredString("employer.physicalAddress.city");
            this.checkRequiredValue("employer.physicalAddress.state", "Please select a state or territory");

            if (!this.validateZipCode(this.getFormValue("employer.physicalAddress.zipCode"))) {
                this.setValidationError("employer.physicalAddress.zipCode", "Please enter a valid zip code");
            }

            this.checkRequiredString("employer.physicalAddress.county", "Please enter a county");

            let hasParentOrg = this.checkRequiredMultipleChoice("employer.hasParentOrg");
            if (hasParentOrg === true) {
                this.checkRequiredString("employer.parentLegalName");
                this.checkRequiredString("employer.parentAddress.streetAddress");
                this.checkRequiredString("employer.parentAddress.city");
                this.checkRequiredValue("employer.parentAddress.state", "Please select a state or territory");

                if (!this.validateZipCode(this.getFormValue("employer.parentAddress.zipCode"))) {
                    this.setValidationError("employer.parentAddress.zipCode", "Please enter a valid zip code");
                }

                this.checkRequiredString("employer.parentAddress.county", "Please enter a county");
            }

            let employerStatus = this.checkRequiredMultipleChoice("employer.employerStatusId");
            if (employerStatus === _constants.responses.employerStatus.other) {
                this.checkRequiredString("employer.employerStatusOther");
            }

            this.checkRequiredMultipleChoice("employer.isEducationalAgency");
            this.checkRequiredDateComponent("employer.fiscalQuarterEndDate");
            this.checkRequiredNumber("employer.numSubminimalWageWorkers.total", undefined, 0);
            this.checkRequiredNumber("employer.numSubminimalWageWorkers.workCenter", undefined, 0);
            this.checkRequiredNumber("employer.numSubminimalWageWorkers.patientWorkers", undefined, 0);
            this.checkRequiredNumber("employer.numSubminimalWageWorkers.swep", undefined, 0);
            this.checkRequiredNumber("employer.numSubminimalWageWorkers.businessEstablishment", undefined, 0);

            this.checkRequiredMultipleChoice("employer.pca");

            let sca = this.checkRequiredMultipleChoice("employer.scaId");
            if (sca === _constants.responses.sca.yes) {
                let scaCount = this.checkRequiredNumber("employer.scaCount", undefined, 0);

                //TODO: validate number of uploads with scaCount ???
                this.checkRequiredValue("employer.SCAAttachment", "Please upload the required SCA Wage Determinations");
            }

            this.checkRequiredMultipleChoice("employer.eo13658Id");

            let representativePayee = this.checkRequiredMultipleChoice("employer.representativePayee");
            if (representativePayee === true) {
                this.checkRequiredNumber("employer.totalDisabledWorkers", undefined, 0);
            }

            let takeCreditForCosts = this.checkRequiredMultipleChoice("employer.takeCreditForCosts");
            if (takeCreditForCosts === true) {
                let deductions = this.checkRequiredValueArray("employer.providingFacilitiesDeductionTypeId", "Please select all that apply");
                if (isArray(deductions) && deductions.includes(_constants.responses.providingFacilitiesDeductionType.other)) {
                    this.checkRequiredString("employer.providingFacilitiesDeductionTypeOther", "Please specify");
                }
            }

            this.checkRequiredMultipleChoice("employer.temporaryAuthority");
        }

        this.validateWageDataPayType = function(prefix) {
            this.checkRequiredNumber(prefix + ".numWorkers", undefined, 0);
            this.checkRequiredString(prefix + ".jobName");
            this.checkRequiredString(prefix + ".jobDescription");

            let prevailingWageMethod = this.checkRequiredMultipleChoice(prefix + ".prevailingWageMethod");
            if (prevailingWageMethod === _constants.responses.prevailingWageMethod.survey) {
                this.checkRequiredNumber(prefix + ".mostRecentPrevailingWageSurvey.prevailingWageDetermined", undefined, 0);

                let sourceEmployers = this.checkRequiredValueArray(prefix + ".mostRecentPrevailingWageSurvey.sourceEmployers", "Please provide 3 source employers");
                if (sourceEmployers) {
                    for (let i=0; i < sourceEmployers.length; i++) {
                        let subprefix = prefix + ".sourceEmployers[" + i + "]";

                        this.checkRequiredString(subprefix + ".employerName");
                        this.checkRequiredString(subprefix + ".address.streetAddress");
                        this.checkRequiredString(subprefix + ".address.city");
                        this.checkRequiredValue(subprefix + ".address.state", "Please select a state or territory");

                        if (!this.validateZipCode(this.getFormValue(subprefix + ".address.zipCode"))) {
                            this.setValidationError(subprefix + ".zipCode", "Please enter a valid zip code");
                        }

                        if (!this.validateTelephoneNumber(this.getFormValue(subprefix + ".phone"))) {
                            this.setValidationError(subprefix + ".phone", "Please enter a valid telephone number");
                        }

                        this.checkRequiredString(subprefix + ".contactName");
                        this.checkRequiredString(subprefix + ".contactTitle");
                        this.checkRequiredDate(subprefix + ".contactDate.month", subprefix + ".contactDate.day", subprefix + ".contactDate.year");
                        this.checkRequiredString(subprefix + ".jobDescription");
                        this.checkRequiredNumber(subprefix + ".experiencedWorkerWageProvided", undefined, 0);
                        this.checkRequiredString(subprefix + ".conclusionWageRateNotBasedOnEntry");
                    }
                }
            }
            else if (prevailingWageMethod === _constants.responses.prevailingWageMethod.alternate) {
                this.checkRequiredString(prefix + ".alternateWageData.alternateWorkDescription");
                this.checkRequiredString(prefix + ".alternateWageData.alternateDataSourceUsed");
                this.checkRequiredNumber(prefix + ".alternateWageData.prevailingWageProvidedBySource");
                this.checkRequiredDate(prefix + ".alternateWageData.dateRetrieved.month", prefix + ".alternateWageData.dateRetrieved.day", prefix + ".alternateWageData.dateRetrieved.year");
            }
            else if (prevailingWageMethod === _constants.responses.prevailingWageMethod.sca) {
                this.checkRequiredValue(prefix + ".scaWageDeterminationAttachment", "Please upload the applicable documentation");
            }
        }

        this.validateWageData = function() {
            let payType = this.checkRequiredMultipleChoice("payType");
            let isHourly = payType === _constants.responses.payType.hourly || payType === _constants.responses.payType.both;
            let isPieceRate = payType === _constants.responses.payType.pieceRate || payType === _constants.responses.payType.both;

            if (isHourly) {
                let prefix = "hourlyWageInfo";

                this.validateWageDataPayType(prefix);

                this.checkRequiredValue(prefix + ".workMeasurementFrequency");
                this.checkRequiredValue(prefix + ".Attachment", "Please upload a work measurement or time study");
            }

            if (isPieceRate) {
                let prefix = "pieceRateWageInfo";

                this.validateWageDataPayType(prefix);

                this.checkRequiredString(prefix + ".pieceRateWorkDescription");
                this.checkRequiredNumber(prefix + ".prevailingWageDeterminedForJob", undefined, 0);
                this.checkRequiredNumber(prefix + ".standardProductivity", undefined, 0);
                this.checkRequiredNumber(prefix + ".pieceRatePaidToWorkers", undefined, 0);
                this.checkRequiredValue(prefix + "Attachment", "Pleas upload the required docments")
            }
        }

        this.validateWorkSites = function() {
            let totalNumWorkSites = this.checkRequiredNumber("totalNumWorkSites", undefined, 1);

            let worksites = this.checkRequiredValueArray("workSites", "Please provide information for each work site");
            if (worksites) {
                for (let i=0; i < worksites.length; i++) {
                    let prefix = "workSites[" + i + "]";
                    this.checkRequiredMultipleChoice(prefix + ".type");
                    this.checkRequiredString(prefix + ".name");

                    this.checkRequiredString(prefix + ".address.streetAddress");
                    this.checkRequiredString(prefix + ".address.city");
                    this.checkRequiredValue(prefix + ".address.state", "Please select a state or territory");

                    if (!this.validateZipCode(this.getFormValue(prefix + ".address.zipCode"))) {
                        this.setValidationError(prefix + ".address.zipCode", "Please enter a valid zip code");
                    }

                    this.checkRequiredMultipleChoice(prefix + ".sca");
                    this.checkRequiredMultipleChoice(prefix + ".federalContractWorkPerformed");

                    let numEmployees = this.checkRequiredNumber(prefix + ".numEmployees", undefined, 0);

                    let employees = this.checkRequiredValueArray(prefix + ".employees", "Please provide the required information for employees");
                    if (employees) {
                        for (let j=0; j < employees.length; j++) {
                            let subprefix = prefix + ".employees[" + j + "]";
                            this.checkRequiredString(subprefix + ".name");

                            let primaryDisability = this.checkRequiredMultipleChoice(subprefix + ".primaryDisability");
                            if (primaryDisability === _constants.responses.primaryDisability.other) {
                                this.checkRequiredString(subprefix + "." + _constants.primaryDisability.otherValueKey);
                            }

                            this.checkRequiredString(subprefix + ".workType");

                            this.checkRequiredNumber(subprefix + ".numJobs", undefined, 0);
                            this.checkRequiredNumber(subprefix + ".avgWeeklyHours", undefined, 0);
                            this.checkRequiredNumber(subprefix + ".avgHourlyEarnings", undefined, 0);
                            this.checkRequiredNumber(subprefix + ".prevailingWage", undefined, 0);
                            this.checkRequiredNumber(subprefix + ".productivityMeasure", undefined, 0);
                            this.checkRequiredNumber(subprefix + ".commensurateWageRate", undefined, 0);
                            this.checkRequiredNumber(subprefix + ".totalHours", undefined, 0);
                            this.checkRequiredMultipleChoice(subprefix + ".workAtOtherSite");
                        }
                    }

                    let totalEmployees = employees ? employees.length : 0;
                    if (numEmployees !== totalEmployees) {
                        this.setValidationError(prefix + ".employee_count", "The number of employees specified does not match the number entered for this worksite");
                    }
                }

                if (totalNumWorkSites !== worksites.length) {
                    this.setValidationError("workSites_count", "The number of work sites specified does not match the number entered");
                }
            }
        }

        this.validateWIOA = function() {
            this.checkRequiredMultipleChoice("WIOA.hasVerfiedDocumentation");

            let hasWIOAWorkers = this.checkRequiredMultipleChoice("WIOA.hasWIOAWorkers");
            if (hasWIOAWorkers === true) {
                let workers = this.checkRequiredValueArray("WIOA.WIOAWorkers", "Please list all applicable workers");
                if (workers) {
                    for (let i=0; i < workers.length; i++) {
                        this.checkRequiredString("WIOA.WIOAWorkers[" + i + "].fullName", "Please specify the worker's full name");
                        this.checkRequiredMultipleChoice("WIOA.WIOAWorkers[" + i + "].WIOAWorkerVerified");
                    }
                }
            }
        }


        // main method to be called for application validation
        this.validateForm = function() {
            this.resetState();
            this.validateAppInfo();
            this.validateEmployer();
            this.validateWageData();
            this.validateWorkSites();
            this.validateWIOA();

            return isEmpty(state);
        }
    });
}
