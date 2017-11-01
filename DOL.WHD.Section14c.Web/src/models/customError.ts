
export class customError extends Error {
  message: string;
  ein: string;
  userId: string;
  user: string;
  level: string;
  exception: string;

  constructor(message:string, ein?: string, userId?: string, user?: string, level?: string, exception?: string) {
    super(message);
    this.message = message;
    this.ein = ein;
    this.userId = userId;
    this.user = user;
    this.level = level;
    this.exception = exception;
  }
}
