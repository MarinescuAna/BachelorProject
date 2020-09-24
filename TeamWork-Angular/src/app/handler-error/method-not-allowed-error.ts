import { AppError } from "./app-error";

export class MethodNotAllowedError extends AppError {
    constructor(public originalErr?: any) {
      super(originalErr);
    }
  }