## Welcome!

We're so glad you're thinking about contributing to an 18F open source project! If you're unsure about anything, just ask -- or submit the issue or pull request anyway. The worst that can happen is you'll be politely asked to change something. We love all friendly contributions.

We want to ensure a welcoming environment for all of our projects. Our staff follow the [18F Code of Conduct](https://github.com/18F/code-of-conduct/blob/master/code-of-conduct.md) and all contributors should do the same.

We encourage you to read this project's CONTRIBUTING policy (you are here), its [LICENSE](LICENSE.md), and its [README](README.md).

If you have any questions or want to read more, check out the [18F Open Source Policy GitHub repository]( https://github.com/18f/open-source-policy), or just [shoot us an email](mailto:18f@gsa.gov).

## Public domain

This project is in the public domain within the United States, and
copyright and related rights in the work worldwide are waived through
the [CC0 1.0 Universal public domain dedication](https://creativecommons.org/publicdomain/zero/1.0/).

All contributions to this project will be released under the CC0
dedication. By submitting a pull request, you are agreeing to comply
with this waiver of copyright interest.

## Internal Collaboration Workflow

Our team uses agile methodologies to deliver the highest value and quality product possible by implementing fully tested and reviewed incremental updates as frequently as possible.

Features of the application are is done 

#### Overview

```
            new branch               new branch
master 0.0 ------------> sprint 1.0 -----------> feature A         
                                                      |
                                                      * commit #1
                                                      |
                                         merge        |
                         sprint 1.1 <---------------  * commit #2
                              |
                              |      new branch        
                               ----------------> feature B              
                                                      |
                                                      |
                                                      * commit #1
               merge                     merge        |
master 1.0 <------------ sprint 1.2 <---------------  * commit #2
    |
    |
    |
    |       new branch                new branch
     ------------------> sprint 2.0 ------------> feature C
                                                      |  
                                                      |  
                                                      * commit #1
                                         merge        |
master 2.0 <------------ sprint 2.1 <---------------- * commit #2

```
The ***master*** branch of this repository contains production ready code that has gone through proper testing and review from our QA and sprint planning teams. There is only 1 master branch. Direct commits to this branch should be avoided.

***Sprint*** (aka release) branches are cut from the master branch. Pull requests from feature branches are then merged into the sprint branch after approval. When the sprint branch has been reviewed and approved, it will be merged into master branch. A new branch will be cut from the master branch for every sprint - there is one branch for each sprint. Direct commits to this branch should be avoided.

***Feature*** branches are development branches that are cut from the current sprint branch. Local commits are then made to a feature branch. When a feature is complete, a developer should submit a pull request to the respective sprint branch. That pull request will be merged after approval into the respective sprint branch

#### Workflow

1. Checkout an existing/current sprint branch or an existing branch that you want to create a new branch from

  ```
  $ git checkout [branch name you want to cut a new branch from]
  ```

2. Cut a namespaced feature branch from current sprint branch
  - sprint1/feature1...
  - sprintA/test1...
  - sprint2/doc...

  ```
  # Cut new branch
  $ git checkout -b [new branch name]

  ```
3. Make commits to feature branch
4. Once feature is finished, create a pull request to respective sprint branch. For example, if you cut your feature branch from sprint A, create a pull request to the sprint A branch.
5. Pull requests to sprint branch should be merged after review
6. When sprint is complete, a merge will be made into the master branch.

\*\* After a sprint is merged into master, a new branch should be created for the next sprint.
#### Branch naming

- No branches should be prefixed with the name master.
- Sprint branches should be prefixed with "sprint" (i.e sprint1, sprintA)
- Feature branch names should feature its parent sprint branch name along with a name for the feature (i.e. sprint1/feature1, sprint2/test1, sprint2/fix1)
