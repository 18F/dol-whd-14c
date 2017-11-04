WIP, lots I'm cleaning up/writing here - AS:

please go ahead. There is some environment info sprinkled in there as well. My document did not verbalize the ZenHub custom swim lanes/labels which I assume your document will.
I believe "Integration" environment is also being used to feature test during the sprint as Kyle mentioned. Integration Env = Current Sprint; QA Env = Previous Sprint + potential fixes applied in Current Sprint
And if fixes applied in Current Sprint to QA Env, fix originates in Master branch and also get applied to the Sprint branch

A bug is the result of a coding error
A defect is a deviation from the requirements
That is: A defect does not necessarily mean there is a bug in the code, it could be a function that was not implemented but defined in the requirements of the software.
I would generally say that bugs or defects should be noted and then the card sent back to `In Progress` for it to be fixed by the team member who was working on it

The integration environment is designed to be more of an "in progress" environment. This environment is tied (via CI) to the sprint branch which is updated as features are completed throughout the sprint (and approved/merged by @mgwalker). The QA environment is more of a "stable" environment. This environment is tied (via CI) to the master branch which gets is updated at the end of each sprint when we merge the sprint branch into the master branch. The thinking behind this was that @sgray and @Leslee would have the QA environment that they could use for user acceptance testing (sorry, there's probably a more accurate term for this) that wouldn't be constantly changing as we integrate new features and we'd have the Integration environment to be able to show off new features as they are ready.

The idea was that the QA environment could be used for usability testing, because it is more stable. However so far we have been doing user testing with UXPin prototypes instead, since we can make design changes quickly in prototypes. @sgray could probably elaborate more -- I'm not sure what our plans for QA environment.

Reconfigured Jenkins
Functional tests

Thanks.  it is very helpful. I am reviewing the process now.  I am also curious if we need to create any label  for the user story for defect we find in INT/QA testing ?

All feature branches get merged back to Sprint branch ideally a few days before end of sprint to allow for integration testing on the integration environment.

Dev QA Sprint Workflow Tenets
Who reviews the work at what stage?
Why do we need both QA and integration environment?
Functional tests (#16)



[] CONTRIBUTING.md/Collaboration Workflow

Can someone help me understand why we have both Integration and QA environments? What is the difference between them?

This document outlines environments and review practices for the DOL WHD 14(c) development team and anyone wanting to contribute to the project.


## Environments 

### Production
#### This environment does not currently exist. In the future, this will exist within DOL.

### Integration
#### This environment is built from the head of the sprint branch. So, everything that is accepted within a given sprint is deployed here.
[Integrating what is needed from dev perspective, first time that all local dev is merged]

WHD, Cert Team will look at “integration” environment for reviewing issues in the current sprint.

As things stand, Sprint branch deploys to the integration (Dev) environment through Jenkins CI. This necessitates reconfiguring Jenkins at the beginning of each sprint. Alternative implementations are possible but not required by WHD. These would require an extra “ais” branch but can be discussed only if AIS needs to.

Defects:
 - If defects are found here, defect must be recorded as a comment in the appropriate issue (am assuming that communication with developer would have occurred). Fix for the defect will be in the feature branch off Sprint X branch. If feature branch was merged in to the Sprint X branch prior to the fix, forward merge from the Sprint X branch in to the feature branch, fix code in the feature branch, and after review (not Sprint review) merge feature branch back to the Sprint X branch.


### QA
#### This environment exists to []
Built from master branch
[Why do we have this?]
WHD, Cert Team will look at “QA” environment for reviewing issues from previous sprints (more stable).
At the end of Sprint X, assuming sprint deliverable is accepted (after demo, code review, QA, etc.) Sprint X code is merged to “master” branch which will trigger a build on the “QA” environment.

Defects: 
 - If defects are found here, fixes will have to be made on a child branch of “master” and merged to “main” after it is reviewed (by a peer/stakeholder) on the developer laptop. Once fix is approved by stakeholder fix branch must be merged to “master”. In addition, “fix” must be forward merged to Sprint X branch. All Developers must be notified of this forward merge. (how?)


## Branches

### Master 

[tk]

### Sprint 

A new Sprint branch is made available by 18F at the beginning of each sprint. This will be a forward merge from master.


### Feature 

Devs can create more branches as needed

Developers by default perform their testing on their local machine during most of the sprint. When needed, developers will create feature branches from this Sprint branch to perform their work. For example, if two developers need to collaborate on their stories, they will work on the same feature branch created for this purpose.

All feature branches get merged back to sprint branch ideally a few days before end of sprint to allow for integration testing on the integration environment.

Recommendations:
 - When working on any feature branch, forward merge from a SprintX branch sometimes might cause a merge conflict or a blocking error. These need to be resolved at the highest priority. These kinds of errors are minimized if developers who need to work on dependent stories have a common feature branch.
  - Before merging in to a SprintX branch from a feature branch, forward merge from the Sprint X branch to feature branch, retest to ensure that your code still works. And only then merge from the feature branch to the SprintX branch. 
   - Ideally, these are done a few days prior to the end of the Sprint X to stabilize the Sprint deliverable.


## Functional testing

In a few, how could a person test that this works....
If not automated functional testing in this engagement...
[could be a separate buy] 

At the beginning of a Sprint, from a QA perspective, QA/manual tester/Developer assigned to the story will record happy test paths, boundary conditions, and unhappy test paths. During the Sprint manual/Dev testing is likely to result in additional test cases. These need to be also recorded as well and associated with the story. Nothing onerous. By the end of period of performance these test cases can be collected to form a test suite, can be used to create a set of validation tests that need to be performed as a part of each Sprint deliverable.
