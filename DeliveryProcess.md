C2 Team Practices
This document outlines best practices for the C2 team, and anyone wanting to contribute to the project.

The Lifecycle of a Story
A story is a definable piece of work to be completed, represented in the team tracker as a card. Bug fixes, engineering tasks, and new user features are all examples of stories. Stories are represented with a short, 1-2 sentence description. Stories that involve user-related changes are written in the perspective of the end user. All stories should contain details on how the feature can be evaluated for acceptance.

By the time a story makes it to the C2 Trello Board, it has gone through a collaborative prioritization process that includes stakeholders, product owners, and the delivery team. Additionally, stories are "groomed" on a weekly basis. This involves team discussion for adding necessary details into stories.

Team members are encouraged to ask for clarification for stories at any time.

A story's life cycle will typically begin at the left of the tracker, progressing through the stages of the tracker from left to right. This is accomplished through team members. A member starts work on a Trello card by dragging it into the In Process list and putting their face on it. Card status is updated by moving the card to the next Trello list. For example, moving a story to the Code Review signifies that the story is in code review.


Lifecycle States
Design tasks are listed separately but on the same board as the development tasks. Some stories may skip the design tasks if there are no visual needs or if the user research has already been completed.
Most stories will enter the tracker here after being prioritized by stakeholders and product owners. Stories that are visually higher in a task list indicate a higher priority than the stories lower in the list. When a card is in this list, a designer can start working on it.


Ready
Committed to 

These are stories that are ready to be worked on by developers, including frontend and backend work. Stories that are visually higher in a task list indicate a higher priority than the stories lower in the list. Although there are often task dependencies before moving, some stories may move directly into Ready to Start if the design/research needs have already been met.

In Trello, the developer will self-assign the story by putting their face on the Trello card.

A developer can self-assign a story while it is in Ready to Start if they intend on working on that card, but developers are encouraged to avoid selecting stories until they are In Progress to avoid preventing others from attempting work on a feature.



In Progress
When a developer starts a story, they move the card to this list and self-assign. Each developer should aim to have no more than 2-3 stories in process at a time.

Blocked
If a story in backlog does not have enough detail to be actionable, add a comment asking for the required information, tag the story with the blocked tag and assign the product manager as the owner to determine next actions.

Q+A

What if I discover a bug or need to create a story that is relevant to the work that we are currently doing?
If the bug is actionable, a story can be created for it and prioritized in the Ready to start. If the story needs more discussion, it can be moved into Needs Grooming for the next grooming session.



___

# Story lifecycle

**Stories** represent tactical increments of individually-valuable work deliverable by a squad within a single iteration... often an isolated change in functionality aimed at achieving a goal for a particular kind of stakeholder, whether customer, user, or operator/admin. Stories are tracked on the collections of the squad that has taken responsibility for them, or in an icebox or under a future Feature as warranted.

Stories progress through these columns (although there is some variation from squad to squad):

- Triage (where reported issues, and newly-identified stories appear)
- Backlog (when they are sequenced alongside other stories prioritized for attention)
- Icebox (where they go when they are not planned to get attention any time soon)

*Note that Triage and Icebox lists are typically at the left in Favro. This is because those lists tend to get loooooong. By separating them from the board, it remains easy to scroll around when you have multiple boards in your collection.* 
- Grooming (when they're being refined for implementation)
- Ready (when they're in a shovel-worthy state, just waiting for team capacity to do the work)
- In Progress (when someone is actively working on the issue)
- Blocked/waiting (when there's a dependency on someone responding or something happening outside the team)
- Awaiting Acceptance (when work is considered complete and awaiting review/merging/feedback)
- Done (when work warrants demonstration to stakeholders and is awaiting the next sprint review)
- Done, but archived (when work has been demoed, released, announced, and is no longer worth looking at)

## Definition of "Done"

An agile "Definition of Done" (DoD) captures the team's agreed-upon standards for how we get work done at a consistent level of quality. Having a DoD ensures that non-functional requirements (NFRs) don't have to be re-litigated for every piece of work taken on, cards can be focused on just the relevant details, and new team members aren't surprised by assumed expectations of their colleagues.

At our [sprint reviews](https://docs.google.com/presentation/d/192PxdXMrCS__QcG6-px5x7n4Mp860ZSb_gqCDlrQwiE/edit), we demo work that has reached the "Done" column and is of interest to our users, teammates, or other people apart from the squad that built it. (As an example of finished work that may not be necessary to demo, fixing internal tech debt may not be of interest outside the squad that fixed it.)

### Column exit criteria
For cloud.gov, our DoD is broken up into a set of statements that should be true for each card before it moves to the next column on the board. 

Before advancing a card from one column to the next on the board, it should meet the "exit criteria" for the current column, which are listed below.

#### Triage

- Relevant points from any discussion in the comments is captured in the initial post.
- Decision is made to move to the Backlog or Icebox columns, or close.

#### Backlog

- Indicate the intended benefit and who the story is for in one of these forms:
 - Story: We prefer the ["In order"](http://blog.crisp.se/2014/09/25/david-evans/as-a-i-want-so-that-considered-harmful) form (rather than the common "As a/I want/so that" form).
 - Hypothesis: What's the lean hypothesis being tested, and how it will be validated?

#### Grooming

- Has testable/demoable Acceptance Criteria that we expect to be able to check off to help us understand when the work is done. (Try wording them as [Gherkin](https://en.wikipedia.org/wiki/Behavior-driven_development#Behavioural_specifications), and use [GFM checklists](https://github.com/blog/1375-task-lists-in-gfm-issues-pulls-comments) for them).
- Discussed by the team and implementation sketched (use more checklists here).
- We believe the benefit of the story is easily deliverable within in a few days of concentrated work (otherwise, split it into smaller stories!)
- Any authentication/authorization and data persistence points are discussed and requirements are addressed via Acceptance Criteria (e.g., "Authenticate using [login.cloud.gov]" or "Any ephemeral data resulting from usage is backed up/recoverable").
- There's a communication plan for any user-visible changes which require their attention/action.

Required for compliance: 
<!-- Security impact analysis is due to CM-3 part b, CM-4 -->
<!-- New software integration check is due to CM-7 (5) part a -->
<!-- Noah said we need to check with him before integrating new services -->

- _[Pending initial threat-modeling of some key components]_ The team has used the [threat modeling guide](https://docs.google.com/document/d/14QOvOqWuLtlnJptB5Lw-qA7ClERiMiA8yfxRTD__lbg/edit) to document and analyze any potential security impact of the changes proposed by the story. 
- If the story includes integrating new software (such as a new open source component) into the core platform, the cloud.gov System Owner has approved the plan.
- If the story includes integrating a new external service (a service outside the cloud.gov FedRAMP authorization boundary) or changing an external service configuration/usage in a way that may have a security/compliance impact, the cloud.gov Authorizing Official has approved the plan.

#### Ready

- No info or assistance is needed from outside the team to start work and likely finish it.
- There's capacity available to work on the story (e.g., this column is a buffer of shovel-ready work).

#### In Progress

- Acceptance criteria are demonstrably met.
- Relevant tasks complete, irrelevant checklists removed or captured on a new story.
- Follows documented coding conventions.
- Pair-programmed or peer-reviewed (e.g., use pull-requests).
- Test coverage exists and overall coverage hasn't been reduced.
- User-facing and internal operation docs have been updated.
- Demoable to other people in their own time (e.g., staging environment, published branch).
- Any deployment is repeatable (e.g., at least documented to increase [bus factor](https://en.wikipedia.org/wiki/Bus_factor) beyond one) and if possible automated via CI/CD.
 - If the deployment is difficult to automate, then a story for making it automated is created at the top of `Triage`.
- All UAA accounts are provisioned using the Cloud Foundry secrets or the cloud.gov [service account](https://cloud.gov/docs/services/cloud-gov-service-account/) or [identity provider](https://cloud.gov/docs/services/cloud-gov-identity-provider/) services.

Required for compliance:

- The deployment must follow our [Configuration Management plan](https://docs.cloud.gov/ops/configuration-management/).  If not possible, contact the Program Management team to modify the story or discuss how to update the Configuration Management plan.  
- If the deployment includes 18F-developed code, ensure the repository is configured to run [Code Climate static analysis on each PR](https://docs.codeclimate.com/docs/github#pull-requests).
  - Code Climate scan results are reviewed, and any false positives are flagged / scanning rules are updated to exclude irrelevant files (vendored code, tests, etc).

#### Awaiting Acceptance

- A team-local proxy for the people the story affects reviews and approves the work as meeting acceptance criteria.
- If the work is suitable to demo at our biweekly sprint review, add a description of it to the [sprint review slide deck](https://docs.google.com/presentation/d/192PxdXMrCS__QcG6-px5x7n4Mp860ZSb_gqCDlrQwiE/edit), ideally including a link to the live work or a screenshot (especially for visual work).
- If the work as completed is obviously unscalable and will cause problems if we try, then a story for making it scalable is created at the top of `Triage`.

Required for compliance:

- The work completed adheres to all of [cloud.gov's policies and procedures](https://github.com/18F/compliance-docs).
- If the work changes an aspect of our system or operating environment that is (or should be) documented in our System Security Plan (SSP), [note necessary changes in our pending SSP change document](https://docs.google.com/a/gsa.gov/document/d/1CWi8efCQKi6TS5oxm76YpSvx3pyQMiW-F0i7lIsdBXk/edit?usp=drive_web) to note the change.

#### Done

- The work is user-visible and announceable at any time.
