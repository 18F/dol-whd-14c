
import { ErrorHandler, Injectable, Injector} from '@angular/core';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { LoggingService } from './services/logging.service';
import { customError } from './../models/customError';
@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private injector: Injector, private loggingService: LoggingService) { }
  handleError(error: any) {
    // console.log('v4')
    console.log(error);
        this.loggingService.addLog("here");
        throw error;
    }
}
