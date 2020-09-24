import { AppError } from "./app-error";

export class ConflictError extends AppError {
    constructor(public originalErr?: any) {
      super(originalErr);
    }
  }