import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { UpgradeModule } from '@angular/upgrade/static';

import { HelloWorldComponent } from './hello-world.component';
import { UiLibraryComponent } from './ui-library.component';

@NgModule({
  imports: [BrowserModule, UpgradeModule],
  declarations: [HelloWorldComponent, UiLibraryComponent],
  entryComponents: [HelloWorldComponent, UiLibraryComponent]
})
export class AppModule {
  ngDoBootstrap() {}
}
