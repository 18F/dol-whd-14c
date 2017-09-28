import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { UpgradeModule } from '@angular/upgrade/static';

import { DolFooterComponent } from './dol-footer.component';
import { DolHeaderComponent } from './dol-header.component';
import { HelloWorldComponent } from './hello-world.component';
import { UiLibraryComponent } from './ui-library.component';

import { mainTopNavDirective } from '../modules/components/mainTopNavControl/mainTopNav.component';

@NgModule({
  imports: [BrowserModule, UpgradeModule],
  declarations: [
    DolFooterComponent,
    DolHeaderComponent,
    HelloWorldComponent,
    UiLibraryComponent,
    mainTopNavDirective
  ],
  entryComponents: [
    DolFooterComponent,
    DolHeaderComponent,
    HelloWorldComponent,
    UiLibraryComponent
  ]
})
export class AppModule {
  ngDoBootstrap() {}
}
