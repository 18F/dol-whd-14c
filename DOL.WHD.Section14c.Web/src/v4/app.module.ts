// Modules
import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { UpgradeModule } from '@angular/upgrade/static';
import { Ng2PageScrollModule } from 'ng2-page-scroll';
import { HttpModule }    from '@angular/http';

// Injectables
import { GlobalErrorHandler } from './error-handler';

// Components
import { DolFooterComponent } from './dol-footer.component';
import { DolHeaderComponent } from './dol-header.component';
import { HelloWorldComponent } from './hello-world.component';
import { UiLibraryComponent } from './ui-library.component';
import { mainTopNavDirective } from '../modules/components/mainTopNavControl/mainTopNav.component';

// Services
import { LoggingService } from './services/logging.service';
import { WindowRef } from './services/window.service';

@NgModule({
  imports: [BrowserModule, UpgradeModule, Ng2PageScrollModule, HttpModule],
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
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    },
    LoggingService,
    WindowRef

  ]
})
export class AppModule {
  ngDoBootstrap() {}
}
