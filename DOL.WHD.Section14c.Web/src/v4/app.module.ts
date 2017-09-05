import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { UpgradeModule } from '@angular/upgrade/static';

import { HelloWorldComponent } from './hello-world.component';

@NgModule({
  imports: [BrowserModule, UpgradeModule],
  declarations: [HelloWorldComponent],
  entryComponents: [HelloWorldComponent]
})
export class AppModule {
  ngDoBootstrap() {}
}
