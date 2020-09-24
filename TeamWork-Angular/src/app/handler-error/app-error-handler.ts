import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { AlertService } from "../services/alert.service";
import { AppError } from "./app-error";
import { BadRequestError } from "./bad-request-error";
import { ConflictError } from "./conflict-error";
import { ForbiddenError } from "./forbidden-error";
import { MethodNotAllowedError } from "./method-not-allowed-error";
import { NotFoundError } from "./not-found-error";
import { UnauthorizedError } from "./unauthorized-error";

@Injectable()
export class AppErrorHandler implements ErrorHandler{
    constructor(private injector:Injector){
    }

    handleError(error: any): void{
        const as=this.injector.get(AlertService);

        if(error instanceof  NotFoundError){
            as.showWarning(error.originalError || 'Record not found!');
            return;
          }
          if (error instanceof ConflictError) {
            as.showWarning(error.originalError || 'The request could not be completed due to a conflict!');
            return;
          }
          if (error instanceof ForbiddenError) {
            as.showWarning(error.originalError || 'You are trying to reach a page which is absolutely forbidden for some reason!');
            return;
          }
          if (error instanceof UnauthorizedError) {
            as.showWarning(error.originalError || 'You are not authorized!');
            return;
          }
          if (error instanceof BadRequestError) {
            as.showWarning(error.originalError || 'Incorrect input!');
            return;
          }
          if (error instanceof MethodNotAllowedError) {
            as.showWarning(error.originalErr || 'Method Not Allowed!');
            return;
          }
          if (error instanceof AppError) {
            as.showWarning(error.originalErr || 'Internal Server Error!');
            return;
          }
          as.showError(error.message);
    }
    
}