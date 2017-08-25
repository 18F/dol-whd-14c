'use strict';

module.exports = function(ngModule) {

    let constants =
        {
            responses: {
                applicationType: {
                    initial: 1,
                    renewal: 2
                },
                establishmentType: {
                    workCenter: 3,
                    careFacility: 4,
                    swep: 5,
                    business: 6
                },
                employerStatus: {
                    public: 7,
                    privateForProfit: 8,
                    privateNotForProfit: 9,
                    other: 10
                },
                sca: {
                    yes: 11,
                    no: 12,
                    intending: 13
                },
                eO13658: {
                    yes: 14,
                    no: 15,
                    intending: 16
                },
                providingFacilitiesDeductionType: {
                    transportation: 17,
                    rent: 18,
                    meals: 19,
                    other: 20
                },
                payType: {
                    hourly: 21,
                    pieceRate: 22,
                    both: 23
                },
                prevailingWageMethod: {
                    survey: 24,
                    alternate: 25,
                    sca: 26
                },
                workSiteType: {
                    main: 27,
                    branch: 28,
                    offsite: 29,
                    swep: 30
                },
                primaryDisability: {
                    idd: 31,
                    pd: 32,
                    vi: 33,
                    hi: 34,
                    sa: 35,
                    nm: 36,
                    ar: 37,
                    other: 38,
                    otherValueKey: "primaryDisabilityOther"
                },
                wioaWorkerVerified: {
                    yes: 39,
                    no: 40,
                    notRequired: 41
                }
            },
            applicationClaimTypes: {
                viewAdminUI: "Application.ViewAdminUI",
                viewAllApplications: "Application.ViewAll",
                changeApplicationStatus: "Application.ChangeStatus"
            }
        };

    ngModule.constant('_constants', constants);

}
