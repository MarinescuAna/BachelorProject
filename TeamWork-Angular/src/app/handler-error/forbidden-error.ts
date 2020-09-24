import { AppError } from "./app-error";

export class ForbiddenError extends AppError {
    constructor(public originalErr?: any) {
      super(originalErr);
    }
  }