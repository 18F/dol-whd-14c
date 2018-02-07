describe('validationService', function() {
  beforeEach(module('14c'));

  var stateService;
  var validationService;

  beforeEach(
    inject(function($injector) {
      stateService = $injector.get('stateService');
      validationService = $injector.get('validationService');

      stateService.setFormData({
        WIOA: {
          WIOAWorkers: [
            {
              fullName: 'dgsdfg',
              WIOAWorkerVerified: {
                id: 41,
                questionKey: 'WIOAWorkerVerified',
                display: 'Not Required',
                subDisplay: null,
                otherValueKey: null,
                isActive: true,
                createdBy_Id: null,
                createdBy: null,
                createdAt: '0001-01-01T00:00:00',
                lastModifiedBy_Id: null,
                lastModifiedBy: null,
                lastModifiedAt: '0001-01-01T00:00:00'
              }
            }
          ],
          hasVerifiedDocumentation: false,
          hasWIOAWorkers: true
        },
        workSites: [
          {
            type: 27,
            name: 'Establishment name',
            address: {
              streetAddress: '1600 Pennsylvania Ave.',
              city: 'Washington',
              state: 'DC',
              zipCode: '2050x'
            },
            sca: false,
            federalContractWorkPerformed: true,
            numEmployees: 2,
            employees: [
              {
                name: 'Joe Plumber',
                primaryDisability: 35,
                workType: 'Work type',
                numJobs: 1,
                avgWeeklyHours: 40,
                avgHourlyEarnings: 5,
                prevailingWage: 5,
                hasProductivityMeasure: true,
                productivityMeasure: 50,
                commensurateWageRate: 102.1,
                totalHours: 40,
                workAtOtherSite: false,
                primaryDisabilityId: 35,
                primaryDisabilityOther: 'other disability'
              }
            ],
            workSiteTypeId: 27
          },
          {
            type: 27,
            name: 'Establishment name',
            address: {
              streetAddress: '1600 Pennsylvania Ave.',
              city: 'Washington',
              state: 'DC',
              zipCode: '20500'
            },
            sca: false,
            federalContractWorkPerformed: true,
            numEmployees: 1,
            employees: [
              {
                name: 'Joe Plumber',
                primaryDisability: 31,
                workType: 'Work type',
                numJobs: 1,
                avgWeeklyHours: 40,
                avgHourlyEarnings: 5,
                prevailingWage: 5,
                hasProductivityMeasure: true,
                productivityMeasure: 50,
                commensurateWageRate: 102.1,
                totalHours: 40,
                workAtOtherSite: false,
                primaryDisabilityId: 31
              }
            ],
            workSiteTypeId: 27
          }
        ],
        saved: '2016-11-07T21:21:05.736Z',
        establishmentType: [3, 4],
        lastSaved: '2016-11-07T21:05:08.780Z',
        applicationType: 2,
        hasPreviousApplication: false,
        hasPreviousCertificate: true,
        certificateNumber: '12-34567-H-89',
        contactName: 'Bob Roberts',
        contactPhone: '123-456-7890',
        contactFax: '987-654-310',
        contactEmail: 'email@email.com',
        employer: {
          numSubminimalWageWorkers: {
            total: 5,
            workCenter: 1,
            patientWorkers: 4,
            swep: 0,
            businessEstablishment: 0
          },
          providingFacilitiesDeductionTypeId: [17, 18, 20],
          providingFacilitiesDeductionTypeOther: 'other stuff',
          legalName: 'AIS',
          hasTradeName: true,
          tradeName: 'Applied Information Sciences',
          legalNameHasChanged: true,
          physicalAddress: {
            streetAddress: '2681 Commons Blvd.',
            city: 'Beavercreek',
            state: 'OH',
            zipCode: '45431',
            county: 'Greene'
          },
          hasParentOrg: true,
          parentLegalName: 'AIS Parent Legal',
          parentTradename: 'AIS Parent Trade',
          parentAddress: {
            streetAddress: '11400 Commerce Park Dr.',
            city: 'Reston',
            state: 'VA',
            zipCode: '2019x',
            county: 'Reston'
          },
          sendMailToParent: true,
          employerStatusId: 10,
          isEducationalAgency: false,
          fiscalQuarterEndDate: '2005-03-01T05:00:00.000Z',
          scaId: 11,
          scaCount: 1,
          eo13658Id: 16,
          representativePayee: true,
          totalDisabledWorkers: 3,
          takeCreditForCosts: true,
          temporaryAuthority: false,
          pca: false,
          employerStatusOther: 'status'
        },
        payType: 23,
        hourlyWageInfo: {
          numWorkers: 5,
          jobName: 'Contract Name',
          jobDescription: 'Job description',
          prevailingWageMethod: 25,
          mostRecentPrevailingWageSurvey: {
            prevailingWageDetermined: 5,
            attachmentId: '86c52860-0aae-4893-a482-80499217d10a',
            attachmentName: '14c.Form.Requirements.Logic.xlsx'
          },
          alternateWageData: {
            alternateWorkDescription: 'Job description',
            alternateDataSourceUsed: 'alternate data source',
            prevailingWageProvidedBySource: 5.11,
            dataRetrieved: '1999-11-11T05:00:00.000Z'
          },
          workMeasurementFrequency: 'asfsdfsdf',
          attachmentId: '7b1a2b0d-d669-4cfb-a316-9c0667861f51',
          attachmentName: '14c.Form.Requirements.Logic.xlsx',
          prevailingWageMethodId: 25
        },
        totalNumWorkSites: 3,
        applicationTypeId: 2,
        payTypeId: 23,
        establishmentTypeId: [4, 3],
        pieceRateWageInfo: {
          numWorkers: 1,
          jobName: '',
          jobDescription: '',
          prevailingWageMethod: 24,
          prevailingWageMethodId: 24,
          mostRecentPrevailingWageSurvey: {
            prevailingWageDetermined: 11,
            basedOnSurvey: '',
            sourceEmployers: [
              {
                employerName: 'Biz 1'
              }
            ]
          }
        }
      });
    })
  );

  it('should reset state', function() {
    validationService.resetState();
    expect(validationService.getValidationErrors()).toEqual({});
  });

  it('should set and clear validation error', function() {
    validationService.setValidationError('error.prop.path', 'ERROR');
    expect(validationService.getValidationError('error.prop.path')).toEqual(
      'ERROR'
    );

    validationService.clearValidationError('error.prop.path');
    expect(validationService.getValidationError('error.prop.path')).toBe(
      undefined
    );
  });

  it('should check required value', function() {
    // test value equals
    expect(
      validationService.checkRequiredValue(
        'employer.numSubminimalWageWorkers.total'
      )
    ).toEqual(5);

    // test invalid property
    expect(validationService.checkRequiredValue('bogus')).toBeUndefined();
    expect(validationService.getValidationError('bogus')).toBeDefined();
  });

  it('should check required string', function() {
    // test value exists
    expect(validationService.checkRequiredString('contactName')).toBeDefined();

    // test invalid property
    expect(validationService.checkRequiredString('bogus')).toBeUndefined();
    expect(validationService.getValidationError('bogus')).toBeDefined();
  });

  it('should check required number', function() {
    // test value equals
    expect(
      validationService.checkRequiredNumber(
        'employer.numSubminimalWageWorkers.total'
      )
    ).toEqual(5);

    // test failure when value is < min
    expect(
      validationService.checkRequiredNumber(
        'employer.numSubminimalWageWorkers.total',
        undefined,
        10
      )
    ).toBeUndefined();

    // test failure when value is > max
    expect(
      validationService.checkRequiredNumber(
        'employer.numSubminimalWageWorkers.total',
        undefined,
        0,
        2
      )
    ).toBeUndefined();

    // test invalid property
    expect(validationService.checkRequiredNumber('bogus')).toBeUndefined();
    expect(validationService.getValidationError('bogus')).toBeDefined();
  });

  it('should check required multiple choice', function() {
    // test value exists
    expect(
      validationService.checkRequiredMultipleChoice('applicationType')
    ).toBeDefined();

    // test invalid property
    expect(
      validationService.checkRequiredMultipleChoice('bogus')
    ).toBeUndefined();
    expect(validationService.getValidationError('bogus')).toBeDefined();
  });

  it('should validate a date', function() {
    expect(validationService.validateDate(new Date())).toBe(true);
  });

  it('should validate a zip code', function() {
    expect(validationService.validateZipCode('26501')).toBe(true);
  });

  it('should validate an ein number', function() {
    expect(validationService.validateEIN('26512301')).toBe(false);
    expect(validationService.validateEIN('26-1234567')).toBe(true);
  });

  it('should validate a certificate number', function() {
    expect(validationService.validateCertificateNumber('12-34567-H-890')).toBe(true);
    expect(validationService.validateCertificateNumber('12-34567-A-890')).toBe(false);
  });

  it('should validate a telephone number', function() {
    expect(validationService.validateTelephoneNumber('333-444-5555')).toBe(
      true
    );
  });

  it('should validate an email address', function() {
    expect(validationService.validateEmailAddress('email@email.com')).toBe(
      true
    );
  });

  it('should validate all form data', function() {
    // test validateForm call
    // test data is incomplete so expect it to be false
    expect(validationService.validateForm()).toBe(false);
  });
});
