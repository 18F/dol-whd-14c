import { Component } from '@angular/core';

@Component({
  selector: 'dol-footer',
  styleUrls: ['./dol-footer.component.css'],
  template: `
    <footer class="dol-footer">
      <div class="dol-footer--brand">
        <a class="brand" href="/">
          <img
            class="brand--img"
            src="images/dol-logo-white.png"
            alt="Seal of the United States Department of Labor"
            width="70"
            height="70"
          />
          <div class="brand--text">
            UNITED STATES<br>DEPARTMENT OF LABOR
          </div>
        </a>
      </div>
      <div class="dol-footer--content">
        <div class="usa-grid-full">
          <div class="usa-width-one-fourth content-group">
            200 Constitution Ave. NW<br>
            Washington DC 20210<br>
            1-866-4-USA-DOL<br>
            1-866-487-2365<br>
            <a href="https://www.dol.gov/">www.dol.gov</a>
          </div>
          <ul class="usa-unstyled-list usa-width-one-fourth content-group">
            <li class="mb1"><strong>ABOUT THE SITE</strong></li>
            <li><a href="#">Link 1</a></li>
            <li><a href="#">Link 2</a></li>
            <li><a href="#">Link 3</a></li>
            <li><a href="#">Link 4</a></li>
          </ul>
          <ul class="usa-unstyled-list usa-width-one-fourth content-group">
            <li class="mb1"><strong>LABOR DEPARTMENT</strong></li>
            <li><a href="#">Link 1</a></li>
            <li><a href="#">Link 2</a></li>
            <li><a href="#">Link 3</a></li>
            <li><a href="#">Link 4</a></li>
          </ul>
          <ul class="usa-unstyled-list usa-width-one-fourth content-group">
            <li class="mb1"><strong>FEDERAL GOVERNMENT</strong></li>
            <li><a href="#">Link 1</a></li>
            <li><a href="#">Link 2</a></li>
            <li><a href="#">Link 3</a></li>
            <li><a href="#">Link 4</a></li>
          </ul>
        </div>
      </div>
    </footer>
  `
})
export class DolFooterComponent {}
