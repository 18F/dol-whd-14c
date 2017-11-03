
import { ErrorHandler, Injectable, Injector} from '@angular/core';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { LoggingService } from './services/logging.service';
import { customError } from './../models/customError';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private injector: Injector, private loggingService: LoggingService) { }
  handleError(error: any) {
    var exception = new customError(error, "Error");
    this.loggingService.addLog(error);
    throw error;
  }
}
