import { Component } from '@angular/core';

@Component({
  selector: 'dol-header',
  styleUrls: ['./dol-header.component.css'],
  template: `
    <a pageScroll href="#mainContent" class="dol-skip-to-main">skip to main content</a>
    <header class="dol-header" role="header">
      <a class="brand" href="/">
        <img
          class="brand--img"
          src="images/dol-logo.png"
          alt="Seal of the United States Department of Labor"
          width="70"
          height="70"
        />
        <div class="brand--text">
          UNITED STATES<br>DEPARTMENT OF LABOR
        </div>
      </a>
    </header>
  `
})
export class DolHeaderComponent {}
