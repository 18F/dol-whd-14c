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

## Collaboration Workflow

Our team strives to deliver the highest value and quality product possible by implementing fully tested, incremental updates as frequently as possible.


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
The ***master*** branch of this repository contains production ready code that has gone through proper testing and review. There is only 1 master branch. Direct commits to this branch should be avoided.

***Sprint*** (aka release) branches are cut from the master branch. Pull requests from complete feature branches (see below) are merged into the sprint branch after approval. After all feature branches have been merged to complete a sprint and after the final sprint branch has been reviewed and approved, it will be merged into the master branch. A new branch will be cut from the master branch (at its current state) for every sprint - there is one branch for each sprint. Direct commits to this branch should be avoided.

***Feature*** branches are development branches that are cut from the current sprint branch. Local commits are then made to a feature branch. When a feature is complete, a developer submits a pull request to the respective sprint branch. The pull request is merged after approval into the respective sprint branch.

#### Workflow

1. If you are contributing from outside of our organization, you will need to [create your own fork](https://help.github.com/articles/fork-a-repo/) of this repository before continuing.  If are within our organization and have write access to the repository, you can skip this step.

2. After cloning your copy of this repository to your local machine, checkout an existing branch that you want to create a new branch from.

  ```
  $ git checkout [branch name you want to cut a new branch from]
  ```

3. Cut a new namespaced feature branch from current sprint branch.

  - sprint1/feature1...
  - sprintA/test1...
  - sprint2/doc...

  ```
  $ git checkout -b [new branch name]

  ```
  - Branches should not be prefixed with the name master.
  - Sprint branches should be prefixed with "sprint" (i.e sprint1, sprintA)
  - Feature branch names should feature its parent sprint branch name along with a name for the feature (i.e. sprint1/feature1, sprint2/test1, sprint2/fix1)
  
4. Make commits to feature branch. Commit early and often. Write concise and clear commit messages. [Read more on writing commit messages](#commit-message-guidelines)...

    Prefix each commit like so:
    - (feat) Added a new feature
    - (fix) Fixed inconsistent tests [Fixes #0]
    - (refactor) ...
    - (cleanup) ...
    - (test) ...
    - (doc) ...

    \* Make changes and commits on your branch, and make sure that you only make changes that are relevant to this branch. If you find yourself making unrelated changes, make a new branch for those changes.

5. Once a feature is finished, [rebase upstream changes](#rebase-upstream-changes-into-your-branch) into your feature branch.
6. Create a [pull request](#make-a-pull-request) from your feature branch to the respective sprint branch it came from. For example, if you cut your feature branch from sprint A, create a pull request to the sprint A branch.
7. Pull requests to a sprint branch are merged after review.
8. When sprint is complete, the respective sprint branch will be merged into the master branch.

\*\* After a sprint is merged into master, a new branch should be created for the next sprint.

##### Commit Message Guidelines
  - Commit messages should be written in the present tense; e.g. "Fix continuous integration script".
  - The first line of your commit message should be a ***brief summary*** of what the commit changes. Aim for about 70 characters max.
  - If you want to explain the commit in more depth, following the first line should be a blank line and then a more detailed description of the commit. This can be as detailed as you want, so dig into details here and keep the first line short.
##### Rebase "upstream" changes into your branch

Once you are done committing changes to your branch, you can begin the process of getting your code merged into the branch of the main repository that your feature branch was cut from. First, you must rebase "upstream" changes to your branch. Rebasing upstream changes will grab any commits made by other developers and place your local commits on top of them -- this ensures that you are always applying your local changes to the latest version of the code base. If there are merge conflicts, you will need to resolve them locally. "Upstream" refers to the git remote pointer of the repository where you cut your feature branch from. If you forked this repository, and want to make contributions to this 18F/dol-whd-14c repo, you can consider the 18F/dol-whd-14c repo as your upstream remote. 
```
UPSTREAM
18F/dol-whd-14c (18F repo that contains sprint A branch)
                   |     
                   |       fork          DOWNSTREAM 
                   -------------------> user/dol-whd-14c (local dev repo that will contain sprint A branch from 18F repo)
```

If you 

To see what remotes your repository can push to, run the following command:

```
git remote -v
```

Your local repository will have one remote by default (origin). This will point to the url of the repository that you cloned your local repository from. If you forked your repository, you will need to add an upstream remote that points this repo (18F/dol-whd-14c). If you are a part of the 18F organization and did not fork this repo, you do not need to add a remote because your upstream remote and origin/downstream remote are the same). To add a remote, run the following command:

```
git remote add <new name of remote e.g. upstream> <url of git repo for new remote>
```

Now that you have an upstream remote set up, you can rebase upstream changes by running the following command:

```
git pull --rebase <remote name> <branch name>
```

This will start the rebase process. You must commit all of your changes before doing this. If there are no conflicts, this should just roll all of your changes back on top of the changes from upstream, leading to a nice, clean, linear commit history.

If there are conflicting changes, git will start yelling at you part way through the rebasing process. Git will pause rebasing to allow you to sort out the conflicts. You do this the same way you solve merge conflicts, by checking all of the files git says have been changed in both histories and picking the versions you want. Be aware that these changes will show up in your pull request, so try and incorporate upstream changes as much as possible.

You pick a file by `git add`ing it - you do not make commits during a rebase.

Once you are done fixing conflicts for a specific commit, run:

```bash
git rebase --continue
```

This will continue the rebasing process. Once you are done fixing all conflicts you should run the existing tests to make sure you didn’t break anything, then run your new tests (there are new tests, right?) and make sure they work also.

If rebasing broke anything, fix it, then repeat the above process until you get here again and nothing is broken and all the tests pass.

##### Make a pull request

Make a clear pull request from your fork and branch to the respective sprint branch on our organization's repository, detailing exactly what changes you made and what feature this should add. The clearer your pull request is the faster you can get your changes incorporated into this repo.

At least one other person MUST give your changes a code review, and once they are satisfied they will merge your changes into proper branch. Alternatively, they may have some requested changes. You should make more commits to your branch to fix these, then follow this process again from rebasing onwards.

Once you get back here, make a comment requesting further review and someone will look at your code again. If they like it, it will get merged, else, just repeat again.
