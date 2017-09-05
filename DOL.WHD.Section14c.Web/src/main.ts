import './polyfills';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { setAngularLib, UpgradeModule } from '@angular/upgrade/static';
import * as angular from 'angular';

import { app } from './modules/app';
import { AppModule } from './v4/app.module';

setAngularLib(angular);

platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .then(platformRef => {
    const upgrade = platformRef.injector.get(UpgradeModule);
    upgrade.bootstrap(document.documentElement, [app.name]);
  });
