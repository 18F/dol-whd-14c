import { Directive, ElementRef, Injector } from '@angular/core';
import { UpgradeComponent } from '@angular/upgrade/static';

@Directive({
  selector: 'main-top-nav'
})
export class mainTopNavDirective extends UpgradeComponent {
  constructor(elementRef: ElementRef, injector: Injector) {
    super('mainTopNavControl', elementRef, injector);
  }
}
