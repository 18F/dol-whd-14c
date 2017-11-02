# Delivery Process

This document outlines delivery practices for the DOL WHD 14(c) development team and anyone wanting to contribute to the project.


## Story lifecycle

Stories represent tactical increments of work, valuable on their own and deliverable by a team within a single iteration. Often, these represent an isolated change in functionality aimed at achieving a goal for a particular kind of stakeholder, whether customer, user, or operator/admin. 

Stories are tracked in the repository and/or board of the team that has taken responsibility for them. Stories that are visually higher in a task list indicate a higher relative priority than the stories lower in the list. 

Stories progress through these columns (typically from left to right):
- **Icebox** (not planned to get attention any time soon)
- **Backlog** (sequenced alongside other stories, prioritized for attention)
- **Ready to Start** (selected and commited to by the team for the current sprint, are in a "shovel-worthy" state, and await only team capacity to do the work)
- **In Progress** (a team member is actively working on the story)
- **Blocked/waiting** (there is a dependency on someone responding or something happening outside the team)
- **Review/QA** (a PR is open and waiting on code review, or the story is waiting on QA/design/other internal review)
- **Awaiting Acceptance** (considered complete and awaiting acceptance by PO or a delegate)
- **Done** (work warrants demonstration to stakeholders at the next sprint review)
- **Closed** (work has been demoed, released, announced, so the story is no longer worth looking at)

## Definition of Done

Our Definition of Done captures the team's agreed-upon standards for how we get work done at a consistent level of quality. Having a DoD ensures that non-functional requirements (NFRs) don't have to be re-litigated for every piece of work taken on; cards can be focused on just the relevant details, and new team members aren't surprised by assumed expectations of their colleagues.

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


#### Ready to Start
- Has a value statement oriented towards end users with clear, testable/demoable Acceptance Criteria that can be checked off so we know precisely when the work is done. (Try wording them as [Gherkin](https://en.wikipedia.org/wiki/Behavior-driven_development#Behavioural_specifications), and use [GFM checklists](https://github.com/blog/1375-task-lists-in-gfm-issues-pulls-comments) for them)
- Discussed by the team with implmentation discussed/sketched as they see appropriate
- Benefit is easily deliverable within in a few days of concentrated work (if not, split it into smaller stories!)
- No information or assistance is needed from outside the team to start work and likely finish it
- There is capacity available to work on the story (e.g., this column is a buffer of shovel-ready work)

#### In Progress
- Acceptance criteria are demonstrably met
- Relevant tasks complete and irrelevant checklists removed or captured on a new story
- Follows documented coding conventions
- Test coverage exists and overall coverage hasn't been reduced

#### Blocked/waiting 
 - Any external blocking issues (e.g. feedback, resources) have been resolved

#### Review/QA 
- Pair-programmed or peer-reviewed (e.g., use pull-requests)
- Any QA, accessibility, and functional tests have been completed
- Demoable to other team members in their own time without special configuration (e.g. staging environment, published branch)
- Any deployment is repeatable (or at least documented to increase [bus factor](https://en.wikipedia.org/wiki/Bus_factor) beyond one) and if possible automated via CI/CD

#### Awaiting Acceptance
- A team-local proxy for the people the story affects (typically the Product Owner, or members the PO designates, including development team members) has reviewed and approved the work as meeting acceptance criteria
- If the work is suitable to demo at our biweekly sprint review, prepare to demonstrate it. Ideally, including a link to the live work or a screenshot (especially for visual work).

#### Done
- Typically, the work is deployed according to our deployment protocol and is visible to users and able to be annoucned at any time.

## Picking up stories

The Scrum Master is responsibile for the overall state of the board, and the team is responsible for making sure their work is represented. When a contributing member starts a story, they move the card to the appropriate column and self-assigns so the team knows the state of the work and who is responsible for it. 

## Grooming

The PO is responsible for working with the team to make sure stories are appropriately groomed, including a value proposition and acceptance criteria. Team members can add implementation details as is useful for them, though these are not required. Team members are encouraged to ask for clarification for stories at any time and should not pick up stories that are not acceptably groomed.

## Bugs, spikes, chores, and tasks

As these items do not represent direct value for users, we relax our grooming criteria. Formal acceptance criteria is not required, though bugs should include steps to reproduce using [behavior-driven development](https://en.wikipedia.org/wiki/Behavior-driven_development) criteria, including a scenario with these elements: 
 - Given [set of conditions]
 - When [input]
 - Then [expected state]
 - And [additional expected state information, as needed]
 - But [description of unexpected behavior/condition]

## WIP limits
We do not currently use WIP ("Work in Progress") limits but may choose to incorporate them in the future to our In Progress, Review/QA, and Awaiting Acceptance columns to focus the team's efforts, avoid spreading ourselves too thin, and delay delivering work.

## Sprint reviews

At our sprint reviews, we demo work that has reached the Done column and is of interest to our users, teammates, or other people apart from the team members that built it. To focus our efforts on finishing work, we *do not* demo work that is almost/nearly done; we may mention briefly work that is in progress, but we don't demo the work and "take the credit" until the work meets our DoD.

Additionally, finished work that may not always be necessary to demo. For example, fixing internal tech debt may not be of interest outside the team that fixed it, so reivews should typically focus on work that delivers value to users.

