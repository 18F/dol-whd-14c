import { Component } from '@angular/core';

@Component({
  selector: 'dol-header',
  styleUrls: ['src/v4/dol-header.component.css'],
  template: `
    <header class="dol-header">
      <a class="dol-brand" href="/">
        <img
          class="dol-brand--img"
          src="https://www.savingmatters.dol.gov/homepage/img/logo.png"
          alt="DOL Logo"
          width="70"
          height="70"
        />
        <div class="dol-brand--text">
          UNITED STATES<br>DEPARTMENT OF LABOR
        </div>
      </a>
    </header>
  `
})
export class DolHeaderComponent {}
