import { AppError } from "./app-error";

export class BadRequestError extends AppError {
  constructor(public originalErr?: any) {
    super(originalErr);
  }
}