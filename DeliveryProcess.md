This document outlines delivery practices for the DOL WHD 14(c) development team and anyone wanting to contribute to the project.


## Story lifecycle

Stories represent tactical increments of work, valuable on their own and deliverable by a team within a single iteration. Often, these represent an isolated change in functionality aimed at achieving a goal for a particular kind of stakeholder, whether customer, user, or operator/administrator. 

Stories are tracked in the repository and/or board of the team that has taken responsibility for them. Stories that are visually higher in a task list indicate a higher relative priority than the stories lower in the list. 

## Definition of Done

Our Definition of Done ("DoD") captures the team's agreed-upon standards for how we get work done at a consistent level of quality. Having a DoD ensures that non-functional requirements (NFRs) don't have to be re-litigated for every piece of work taken on; cards can be focused on just the relevant details, and new team members aren't surprised by assumed expectations of their colleagues.

### Column definitions and exit criteria
Our DoD is broken up into a set of statements that should be true for each card before it moves to the next column on the board. Generally, stories progress through these columns from left to right. To advance a card from one column to the next, it must meet the "exit criteria" for the current column, which are listed below. 


### Icebox 
#### Not prioritized to get attention any time soon

### Backlog
#### Sequenced with other stories, prioritized for attention

Exit criteria:
 - Indicate the intended benefit and who the story is for in one of these forms:
 - Consider the ["In order"](http://blog.crisp.se/2014/09/25/david-evans/as-a-i-want-so-that-considered-harmful) form (rather than the "As a/I want/so that" form).
 - When appropriate: indicate the hypothesis being tested and how it will/could be validated


### Ready to Start
#### Selected and committed to by the team for the current sprint, are in a "shovel-worthy" state, and await only team capacity to do the work

Exit criteria:
- Has a value statement oriented towards end users with clear, testable Acceptance Criteria that can be checked off so we know precisely when the work is done
- Discussed by the team with implementation discussed/sketched as they see appropriate
- Benefit is easily deliverable within in a few days of concentrated work (if not, split it into smaller stories!)
- No information, assistance, or unsecured resources are needed from outside the team to start work and likely finish it
- There is capacity available to work on the story (e.g. this column is a buffer of shovel-ready work)

### In Progress
#### A team member is actively working on the story

Exit criteria: 
- Acceptance criteria are demonstrably met
- Relevant tasks complete, and any irrelevant checklists are removed (captured in a new story)
- Follows documented coding conventions
- Test coverage exists and overall coverage meets our standards

### Blocked/waiting 
#### There is a dependency on someone responding or something happening outside the team

Exit criteria:
 - Any external blocking issues (e.g. feedback, resources) have been resolved

### Review/QA 
#### A PR is open and waiting on code review, or the story is waiting on QA/design/other internal review

*Note: over time, this column's criteria could potentially folded into In Progress*

Exit criteria:
- Code is pair-programmed or peer-reviewed (e.g. use pull-requests)
- Any QA, accessibility, and functional tests have been completed
- Demoable to other team members in their own time without special configuration (e.g. in a staging environment on a published branch)
- Any deployment is repeatable (or at least documented) and, if possible, automated via CI/CD

### Awaiting Acceptance
#### Story is considered complete and awaiting acceptance by PO or a delegate

*Note: acceptance can, and should, happen at any time, not just at the end of a sprint. If needed, the team can meet at the end of the sprint to review any lingering issues in this column, but this can be the exception rather than the norm to save review time for demonstrating features to the broader team.*

Exit criteria:
- A team-local proxy for the people the story affects (typically the Product Owner, or members the PO designates, including development team members) has reviewed and approved the work as meeting acceptance criteria
- If the work is suitable to demo at sprint review, prepare to demonstrate it. Ideally, including a link to the live work or a screenshot (especially for visual work)

### Done

#### Work warrants demonstration to stakeholders at the next sprint review or is otherwise considered complete

Exit criteria:
 - The work is deployed according to our deployment protocol and is visible to users and able to be announced at any time

### Closed
#### Work has been demonstrated, released, announced, so the story is no longer worth looking at

## Grooming

The PO is responsible for working with the team to make sure stories are appropriately groomed, including a value proposition and acceptance criteria. Team members can add implementation details as is useful for them, though these details are not required. Team members are encouraged to ask for clarification for stories at any time and should not pick up stories that are not acceptably groomed.

## Bugs, spikes, chores, and tasks

As these do not necessarily (but can) represent value for users, we relax our grooming criteria somewhat. Formal acceptance criteria is not required, though bugs should include steps to reproduce using [behavior-driven development](https://en.wikipedia.org/wiki/Behavior-driven_development) criteria, including a scenario with these elements: 
 - Given [set of conditions]
 - When [input]
 - Then [expected state]
 - And [additional expected state information, as needed]
 - But [description of unexpected behavior/condition]

## Estimating
We currently estimate using story points. If a story takes more than three days for a dedicated team member to complete it, we break down the story into more manageable chunks of work.

## Picking up stories

The Scrum Master is responsible for the overall state of the board, and the team is responsible for making sure their current work state is represented. For example, when a contributing member starts a story, they move the card to the appropriate column and self-assigns so the team knows the state of the work, who is responsible for it, and what additional attention, if any, is required.


## White noise threshold

During the course of work, the team will encounter new requirements, details, tasks, or chores related to the main work of the sprint. If this work takes two hours or less, the team should just do the work. If the work would take more, they should write a story and ask the Scrum Master and PO to direct them how to prioritize this. 

If significant new work is brought into the sprint, other work should be removed to keep a reasonable level of work for the team at all times.

## WIP limits
We do not currently use WIP ("Work in Progress") limits but may choose to incorporate them in the future to our In Progress, Review/QA, and Awaiting Acceptance columns to focus the team's efforts, avoid spreading ourselves too thin, and delay delivering work.

## Sprint planning

During sprint planning, the PO brings the development team a backlog of stories; typically, these stories are discussed, groomed, and estimated in advance, but these activities can occur during sprint planning, as needed. 

With the Scrum Master's guidance, the team asks the PO any questions they have about the stories and then, given capacity, commit to a number of stories they feel is reasonably possible to complete during the sprint given what they know about the stories and their capacity. At the end of planning, the Scrum Master takes a level of confidence measure from the contributing team members at the end – if the average overall confidence is 60% or higher, the sprint is considered committed, and the team begins work.


## Running the sprint

During the sprint, if business context changes, *only* the PO (or a delegate) can change the priority or bring new work into the sprint. Again, if significant new work is brought into the sprint, other work should be removed to keep a reasonable level of work for the team.


## Sprint review/system demo

At sprint reviews, the team demonstrates work that has been completed (reaching the Done column) and is of interest to users, other teammates, or people apart from the team members that built it. To focus our efforts on finishing work, we *do not* demo work that is almost/nearly done; we may mention briefly work that is in progress, but we don't demo the work and "take the credit" until the work meets our DoD.

Additionally, finished work that may not always be necessary to demo. For example, fixing internal tech debt may not be of interest outside the team that fixed it, so reviews should typically focus on work that delivers value to users.

## How we test

Our team ships bug-free and defect-free code, discovers problems early, and addresses these promptly. Rather than going through "User Acceptance Testing" cycles that can be quite large and cumbersome, we take the responsibilty of regularly shipping quality software with the resources contained within the team. This can take some time and practice initially but, over the long term, helps us move faster. 

Definitions: 
 - A bug is the result of a coding error
 - A defect is a deviation from the requirements (that is: a defect does not necessarily mean there is a bug in the code, it could be a function that was not implemented but defined in the requirements of the software.)

_Citation: https://softwareengineering.stackexchange.com/questions/37029/difference-between-a-defect-and-a-bug-in-testing_

#### When we find bugs or defects

Though the Product Owner has main responsibility for quality control, the team's job is also keeping a vigilent eye out for quality and calling out any issues. 

If a bug or defect is found in work that is in progress, comment on the related issue (and, if appropriate, also move it back to the appropriate column on the board) so the team member who was working on it can fix it. 

If a bug or defect is found outside of work in progress (by the development team or other parties), a new issue should be filed so it can be triaged.

Establishing testing practices are key to enabling these needs. You will be working with your team to ensure that a testing strategy is in place covering the following:

### Unit tests

[tk]


### Functional testing
Our product need to be continually tested from end to end. That is, unit test coverage cannot replace functional testing. Since we currently do not have (and project teams seldom) a separate QA team, we take the lead on establishing and running these testing practices and test suites. 

Though there are many ways to perform end-to-end functional testing, the product manager ensures that these are done.

#### Automated functional tests

Automated testing protects against a lot. Though we do not currently have automated functional tests, we can start down the path in implementing these by:
 - When a team member picks up the story, document happy test paths, boundary conditions, and unhappy test paths
 - During develeopment, document any additional test cases
 - Collect these and form test suites which can be used for ongoing automated testing

#### Manual functional tests

In the least, the product manager must spend time testing the product to make sure our stories result in functional components. Generally, when accepting stories, the PM should review them in a stable environment, make sure the components work, actively try to break the system (trying common user errors), and monitor for regressions, 

In the future, we may leverage a manual QA team (e.g. through the bug bounty program) to dedicate staff, but for now, manually testing is the team's job, led by the PO.

### Nonfunctional testing

#### Accessibility testing

We test using Web Content Accessibility Guidelines 2.0 AA (WCAG 2.0 AA) as our accessibility standards. For automatic scans, we use [pa11y](https://github.com/pa11y/pa11y) or [AXE](https://www.deque.com/blog/google-selects-deques-axe-chrome-devtools/). For manual scans, we use the [DHS Trusted Tester Program (TT)](https://www.dhs.gov/trusted-tester) process. For each, our acceptable quality level is zero errors reported. If errors are found, they should be addressed before a story is marked as complete.

#### Performance testing
Validating non-functional aspects such as performance compliance should also be accounted for by the product owner or project manager in project plans.


### Usability testing
In the vein of feedback cycles and iterative development, usability testing provides key insight as to a product’s effectiveness.

At this stage, code is complete and deployed, so users are not necessarily trying to break it, but they do review it, give feedback to the development team, and highlight any bugs they happen to find. Generally, this code can be reviewed on staging (until a production environment is deployed).
