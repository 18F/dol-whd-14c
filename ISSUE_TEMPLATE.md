As a [type of user] I would like [functionality, behavior, etc.] so that [a particular task or need that is satisfied by the functionality or behavior]

## Acceptance Criteria

> **NOTE**: Remove any accessibility criteria that don't apply to this user story.  For the full list of accessibility requirements, see [issue #198, Fully Accessible Experience](#198).

#### Functionality

- [ ] functionality criteria 1
- [ ] functionality criteria 2
- [ ] etc.

#### Accessibility

<<<<<<< HEAD
> **NOTE**: Remove any accessibility criteria that don't apply to this user story.  For the full list of accessibility requirements, see [issue #198, Fully Accessible Experience](#198).

=======
>>>>>>> add accessibility criteria
##### Keyboard Accessibility

- [ ] An interactive element or function can be accessed or activated by keyboard
- [ ] keyboard "trap" not found
- [ ] if non-standard keyboard commands are needed and they are documented
- [ ] TITLE provides information and equivalent information is found through text or visual context
- [ ] The visual focus can be determined at all times.
- [ ] focus always appears on the element it is programmatically on.
- [ ] if focus remains in the modal dialog box until the box is closed.
- [ ] if focus moves to revealed content OR a description of the content change is provided.
- [ ] the tab order is logical

##### Web: Forms

> Web forms include controls (checkboxes, radio buttons etc.), and editable content (text input, select options etc.).

- [ ] all form fields and their instructions and cues have HTML (‘Label for’ and ‘ID’ are used or TITLE is descriptive) or ARIA for association.
- [ ] All 'Label for' and 'ID' are valid code pairs.
- [ ] All form instructions that are provided by mouse over are available onscreen.
- [ ] All ARIA form fields have a NAME property that contains all of the instructions.
- [ ] All ARIA form fields have correct Role, State, and Value properties.
- [ ] All form controls identify their purpose. (Check if there are multiple form controls with the same visual label).

##### Web: Links and User Controls

> Links and/or user controls must have meaningful names that describe the unique destination, function, and/or purpose of the control for assistive technology.

- [ ] All links have a unique and meaningful description.
- [ ] All scripted elements have a unique and meaningful description.

##### Web: Images

> Web images include interactive images (links, buttons etc.), static images, charts, diagrams, text rendered as an image, etc. 

- [ ] All images have an ALT, TITLE, or ARIA attribute. 
- [ ] All meaningful images with ALT have an equivalent text description.
- [ ] All decorative images with ALT have ALT="". 
- [ ] All images that contain text with ALT have the same text in the ALT attribute.
- [ ] CAPTCHA images describe their purpose. (DNA, after the captcha is removed)
- [ ] All components that have multiple statuses provide their current status

##### Web: Image Maps (if no image map, then DNA)

> An image map is a single image that has designated regions or "hotspots" that contain links.Server-side image maps may not be used. Client-side image-maps must be used instead

- [ ] All image maps are client side

##### Color and Contrast

> Color dependence is using color as the sole means to convey information. There must be contrasting colors/shades at a ratio of 4.5:1 for discerning between background and foreground content.

- [ ] if color is used but is not the only method to provide information.
- [ ] if the contrast ratio is 4.5:1 or greater when comparing all background and foreground colors.
Page titles Programmatically identify Page Titles.
- [ ] There is a meaningful page title in plain language.

##### Time Outs

> Messages and/or instructions to the user requesting their response within a given time are typically associated with sites that require a secure login. This includes both server time outs and client side security time outs. If a time out is about to occur, an alert must be posted for at least 20 seconds and the user must have the option to request more time. The alert (often a pop up window) and option to request more time must be keyboard accessible.

- [ ] The application provides notification before timing out.
- [ ] The application's time out notification is displayed for at least **20** seconds before timing out.
- [ ] The application provides user an option to request more time before timing out.

##### Web: Language

> A default language must be programmatically identified for each page and for passages that use a language other than the default.

- [ ] if the correct default language for the page is programmatically set.
- [ ] if there is not a passage in a language that differs from the default language of the page.

##### Web: Section Headings

> Headings must be programmatically identified and must match the visual outline level.

- [ ] All visually apparent headings are programmatically identified with an <H>. (<H> levels do not need to be correct.) 
- [ ] Programmatic <H> levels on all visually apparent headings match the visual structure.

##### Web: Data Tables (If no table, then DNA)

> Data tables are those tables where the information in a cell requires a row or column header to adequately describe the cell's contents.

- [ ] HTML data tables' row and/or column headers are correctly identified programmatically.
- [ ] if there are data tables but none of them are images.
- [ ] if all HTML complex data tables' data cells are associated with their headers programmatically. 
- [ ] if there are complex data tables but none of them are images

##### Web: Style sheet Dependence

> style sheets are a means to provide visual formatting information to complement a web page's content.

- [ ] The order of the content did not change OR if the order of the content changed but is still logical.
- [ ] All relevant content and information from all meaningful images is available.
- [ ] All content does not overlap and stays legible.
- [ ] If no confusing elements are revealed on the page.

##### Web: Frames

> Frames are a means of separating out sections of a web page into different navigable regions

- [ ] if all frames' Title or Name is descriptive.

##### Web: Repetitive Content and Links

> A method must be provided to skip blocks of repeated content or links on Web pages allowing a user to move directly to page-specific content.

- [ ] There is a method to skip repetitive content.
- [ ] There is a target for all skip links.
- [ ] All skip functions work properly
- [ ] If the relative order of the repeated components is the same as other pages.

##### Web: Required Plug-ins (if no plug-In, then DNA)

- [ ] if on a public site, links to download all required plug-ins are provided.
- [ ] All plug-ins required to view content are compliant.
