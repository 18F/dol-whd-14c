This document outlines environments and review practices for the DOL WHD 14(c) development team and anyone wanting to contribute to the project.


## Environments 

### Production
This environment does not currently exist. In the future, this will exist within DOL.

### Staging (renamed from QA)

This is a stable environment where the Product Owner (and other team members, as needed) will typically be able to review user stories, assess their acceptance criteria, and confirm that completed stories meet our Definition of Done. 

Additionally, the environment is used for general testing and review by contributors or stakeholders outside of the core development team and should be accessible via a web browser without any local configuration.

### Test
This environment does not currently exist but could be created for reviewing work-in-progress or experimental features during user testing session.

### Integration
This environment is designed to be more of an "in progress" environment for developer work and testing during a sprint. The environment is tied (via CI) to the master branch which is updated as features are completed.

This environment is built from the head of the master branch. So, everything that is accepted within a given sprint is deployed here. From a development persepctive, this environment represents the first time that all local development branches are merged into one place.


## Branches

### Master 

When the development team completes code review, design review, and their QA, their work is merged into the *master* branch. The aggregate changes on this branch are periodically bundled and deployed to staging.

### Sprint 

[tk]

### Feature 

By default, developers perform their testing on their local machine during most of the sprint. When needed, developers will create feature branches from the sprint branch to perform their work. For example, if two developers need to collaborate on their stories, they will work on the same feature branch created for this purpose.

All feature branches get merged back to master branch when their work has been completed to allow for testing by the development team.

Recommendations:
 - When working on any feature branch, forward merges from the master branch sometimes might cause a merge conflict or a blocking error. These need to be resolved at the highest priority. These kinds of errors are minimized if developers who need to work on dependent stories have a common feature branch.
  - Before merging into the master branch from a feature branch, forward merge from the master branch to a feature branch and retest to ensure that your code still works. Only then, merge from the feature branch to the master branch.
