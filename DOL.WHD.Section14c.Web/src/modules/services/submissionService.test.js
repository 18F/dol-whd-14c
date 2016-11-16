describe('submissionService', function() {
    beforeEach(module('14c'));

    var submissionService;

    beforeEach(inject(function(_submissionService_) {
        submissionService = _submissionService_;
    }));

    it('should collapse id property', function() {
        const obj = {
            wioa: {
                wioaWorkers: [
                    {
                        wioaWorkerVerified : {
                            id: 41,
                            display: "Yes"
                        }
                    }
                ]
            }
        };
        
        const submissionVM = submissionService.getSubmissionVM('30-1234567', obj);

        expect(submissionVM.wioa.wioaWorkers[0].wioaWorkerVerified).not.toBeDefined();
        expect(submissionVM.wioa.wioaWorkers[0].wioaWorkerVerifiedId).toBe(41);
    });

    it('should populate ein property', function() {
        const submissionVM = submissionService.getSubmissionVM('30-1234567', {});

        expect(submissionVM.ein).toBe('30-1234567');
    });
    
});
