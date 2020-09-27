export class AppError {
    originalError: string;
    constructor(public originalErr?: any) {
      this.originalError = originalErr != null ? originalErr : null;
    }
  }