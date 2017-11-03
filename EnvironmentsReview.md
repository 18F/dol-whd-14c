[] CONTRIBUTING.md/Collaboration Workflow

This document outlines environments and review practices for the DOL WHD 14(c) development team and anyone wanting to contribute to the project.


## Environments 

### Production
#### This environment exists to []

### Integration
#### This environment exists to []
[Integrating what is needed from dev perspective, first time that all local dev is merged]

WHD, Cert Team will look at “integration” environment for reviewing issues in the current sprint.

As things stand, Sprint branch deploys to the integration (Dev) environment through Jenkins CI. This necessitates reconfiguring Jenkins at the beginning of each sprint. Alternative implementations are possible but not required by WHD. These would require an extra “ais” branch but can be discussed only if AIS needs to.

Defects:
 - If defects are found here, defect must be recorded as a comment in the appropriate issue (am assuming that communication with developer would have occurred). Fix for the defect will be in the feature branch off Sprint X branch. If feature branch was merged in to the Sprint X branch prior to the fix, forward merge from the Sprint X branch in to the feature branch, fix code in the feature branch, and after review (not Sprint review) merge feature branch back to the Sprint X branch.


### QA
#### This environment exists to []
WHD, Cert Team will look at “QA” environment for reviewing issues from previous sprints (more stable).
At the end of Sprint X, assuming sprint deliverable is accepted (after demo, code review, QA, etc.) Sprint X code is merged to “master” branch which will trigger a build on the “QA” environment.

Defects: 
 - If defects are found here, fixes will have to be made on a child branch of “master” and merged to “main” after it is reviewed (by a peer/stakeholder) on the developer laptop. Once fix is approved by stakeholder fix branch must be merged to “master”. In addition, “fix” must be forward merged to Sprint X branch. All Developers must be notified of this forward merge. (how?)


## Branches

### Sprint branch

A new Sprint branch is made available by 18F at the beginning of each sprint. This will be a forward merge from master.



### Feature branches

Developers by default perform their testing on their local machine during most of the sprint. When needed, developers will create feature branches from this Sprint branch to perform their work. For example, if two developers need to collaborate on their stories, they will work on the same feature branch created for this purpose.

All feature branches get merged back to sprint branch ideally a few days before end of sprint to allow for integration testing on the integration environment.

Recommendations:
 - When working on any feature branch, forward merge from a SprintX branch sometimes might cause a merge conflict or a blocking error. These need to be resolved at the highest priority. These kinds of errors are minimized if developers who need to work on dependent stories have a common feature branch.
  - Before merging in to a SprintX branch from a feature branch, forward merge from the Sprint X branch to feature branch, retest to ensure that your code still works. And only then merge from the feature branch to the SprintX branch. 
   - Ideally, these are done a few days prior to the end of the Sprint X to stabilize the Sprint deliverable.


## Functional testing

At the beginning of a Sprint, from a QA perspective, QA/manual tester/Developer assigned to the story will record happy test paths, boundary conditions, and unhappy test paths. During the Sprint manual/Dev testing is likely to result in additional test cases. These need to be also recorded as well and associated with the story. Nothing onerous. By the end of period of performance these test cases can be collected to form a test suite, can be used to create a set of validation tests that need to be performed as a part of each Sprint deliverable.




