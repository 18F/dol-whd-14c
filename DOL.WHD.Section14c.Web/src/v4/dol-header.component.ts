import { Component } from '@angular/core';

@Component({
  selector: 'dol-header',
  styleUrls: ['src/v4/dol-header.component.css'],
  template: `
    <header class="dol-header">
      <a class="brand" href="/">
        <img
          class="brand--img"
          src="/images/dol-logo.png"
          alt="DOL Logo"
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
